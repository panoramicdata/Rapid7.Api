using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Rapid7.Api;

/// <summary>Creates the network handler every Rapid7 client sends through, with its certificate trust and pooling.</summary>
internal static class Rapid7Transport
{
	/// <summary>
	/// How long an idle pooled connection is kept. Servers and load balancers close idle keep-alive connections, often
	/// after a few seconds; reusing one already closed fails with "connection forcibly closed" mid-request, so
	/// connections are dropped well before that.
	/// </summary>
	internal static readonly TimeSpan PooledConnectionIdleTimeout = TimeSpan.FromSeconds(5);

	/// <summary>How long any pooled connection is kept, so DNS changes (for example a console or region move) are picked up.</summary>
	internal static readonly TimeSpan PooledConnectionLifetime = TimeSpan.FromMinutes(5);

	/// <summary>Creates the handler for <paramref name="options"/>.</summary>
	internal static SocketsHttpHandler Create(Rapid7ConnectionOptions options)
	{
		ArgumentNullException.ThrowIfNull(options);
		return Create(options.ServerCertificateValidationCallback, options.TrustedServerCertificateThumbprint);
	}

	/// <summary>
	/// Creates the handler: <paramref name="validationCallback"/> when set, else trust for the certificate with
	/// <paramref name="trustedThumbprint"/> (SHA-256) on top of normal validation.
	/// </summary>
	internal static SocketsHttpHandler Create(
		Func<X509Certificate2?, X509Chain?, SslPolicyErrors, bool>? validationCallback,
		string? trustedThumbprint)
	{
		var handler = new SocketsHttpHandler
		{
			PooledConnectionIdleTimeout = PooledConnectionIdleTimeout,
			PooledConnectionLifetime = PooledConnectionLifetime
		};
		var callback = validationCallback ?? PinnedCallback(NormalizeThumbprint(trustedThumbprint));
		if (callback is not null)
		{
			// The sender is the SslStream, not the request, so the callback is not given one.
			handler.SslOptions.RemoteCertificateValidationCallback = (_, certificate, chain, errors)
				=> callback(AsCertificate2(certificate), chain, errors);
		}

		return handler;
	}

	/// <summary>TLS hands over an <see cref="X509Certificate2"/>; anything else is loaded as one.</summary>
	internal static X509Certificate2? AsCertificate2(X509Certificate? certificate)
		=> certificate switch
		{
			null => null,
			X509Certificate2 certificate2 => certificate2,
			_ => X509CertificateLoader.LoadCertificate(certificate.GetRawCertData())
		};

	private static Func<X509Certificate2?, X509Chain?, SslPolicyErrors, bool>? PinnedCallback(string? pinned)
		=> pinned is null
			? null
			: (certificate, _, errors) => errors == SslPolicyErrors.None
				|| (certificate is not null
					&& string.Equals(certificate.GetCertHashString(HashAlgorithmName.SHA256), pinned, StringComparison.OrdinalIgnoreCase));

	/// <summary>Normalises a SHA-256 thumbprint to 64 hex digits, or returns <see langword="null"/> when none is set.</summary>
	/// <exception cref="ArgumentException">The thumbprint is not 64 hex digits once separators are removed.</exception>
	internal static string? NormalizeThumbprint(string? thumbprint)
	{
		if (string.IsNullOrWhiteSpace(thumbprint))
		{
			return null;
		}

		var hex = new string([.. thumbprint.Where(Uri.IsHexDigit)]);
		return hex.Length == 64
			? hex
			: throw new ArgumentException("TrustedServerCertificateThumbprint must be a SHA-256 thumbprint (64 hex digits).", nameof(thumbprint));
	}
}
