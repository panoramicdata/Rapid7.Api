using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Rapid7.Api;

/// <summary>
/// The connection settings shared by <see cref="Rapid7ClientOptions"/> and <see cref="Rapid7PlatformOptions"/>: server
/// certificate trust, timeouts, retries and logging.
/// </summary>
public abstract class Rapid7ConnectionOptions
{
	private static readonly TimeSpan MaxTimerDuration = TimeSpan.FromMilliseconds(int.MaxValue);

	/// <summary>
	/// The SHA-256 thumbprint (hex, case and separators ignored) of a server certificate to trust even when it fails
	/// normal validation, such as a Security Console's default self-signed certificate. Other certificates are still validated
	/// normally. Ignored when <see cref="ServerCertificateValidationCallback"/> is set, and when an inner
	/// <see cref="HttpMessageHandler"/> is supplied to the client.
	/// </summary>
	public string? TrustedServerCertificateThumbprint { get; set; }

	/// <summary>
	/// Full control over server certificate validation. Takes precedence over <see cref="TrustedServerCertificateThumbprint"/>.
	/// Ignored when an inner <see cref="HttpMessageHandler"/> is supplied to the client.
	/// </summary>
	public Func<X509Certificate2?, X509Chain?, SslPolicyErrors, bool>? ServerCertificateValidationCallback { get; set; }

	/// <summary>
	/// HTTP timeout per attempt, covering sending the request and receiving the response headers. It does not include
	/// retry back-off, nor reading a streamed body after the headers arrive. An attempt that exceeds it raises a
	/// <see cref="TimeoutException"/>; caller cancellation still raises <see cref="OperationCanceledException"/>. Must be
	/// greater than zero and at most <see cref="int.MaxValue"/> milliseconds (about 24.8 days).
	/// </summary>
	public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);

	/// <summary>
	/// Maximum retries of a transient failure. Any verb is retried on 429 and 503;
	/// other 5xx responses are retried only for idempotent verbs (GET, HEAD, PUT, DELETE), never POST. A connection
	/// that could not be established (refused, reset during the TLS handshake, or a name that did not resolve) is retried
	/// for any verb, since nothing was sent. Requests with a stream body are never retried.
	/// </summary>
	public int MaxRetries { get; set; } = 3;

	/// <summary>Initial back-off, doubled on each retry (up to <see cref="MaxRetryDelay"/>). Must not be negative.</summary>
	public TimeSpan RetryBaseDelay { get; set; } = TimeSpan.FromSeconds(1);

	/// <summary>
	/// The longest single wait before a retry, also capping a server-supplied <c>Retry-After</c>. Must be greater than zero
	/// and at most <see cref="int.MaxValue"/> milliseconds.
	/// </summary>
	public TimeSpan MaxRetryDelay { get; set; } = TimeSpan.FromSeconds(30);

	/// <summary>
	/// Optional logger. Credentials, API keys, 2FA tokens and query strings are never
	/// logged; only the method and path are.
	/// </summary>
	public ILogger? Logger { get; set; }

	/// <summary>Parses a base URL, which must be an absolute http or https URL.</summary>
	private protected static bool TryParseBaseUrl(string baseUrl, [NotNullWhen(true)] out Uri? baseUri)
		// Absolute alone is not enough: on Linux a rooted path such as "/relative/path" parses as a file:// URI.
		=> Uri.TryCreate(baseUrl, UriKind.Absolute, out baseUri)
			&& (baseUri.Scheme == Uri.UriSchemeHttp || baseUri.Scheme == Uri.UriSchemeHttps);

	/// <summary>
	/// Checks a base URL: absolute http or https, with no credentials (they would leak into logs and exception messages),
	/// no query and no fragment (they would corrupt every request URL).
	/// </summary>
	private protected static void ValidateBaseUrl(string baseUrl, string parameterName)
	{
		if (!TryParseBaseUrl(baseUrl, out var baseUri))
		{
			throw new ArgumentException("The base URL must be an absolute http or https URL.", parameterName);
		}

		if (baseUri.UserInfo.Length > 0)
		{
			throw new ArgumentException("The base URL must not contain credentials; set them in the options instead.", parameterName);
		}

		if (baseUri.Query.Length > 0 || baseUri.Fragment.Length > 0)
		{
			throw new ArgumentException("The base URL must not contain a query string or fragment.", parameterName);
		}
	}

	/// <summary>Masks a secret for display: <c>***</c> when set, <c>(none)</c> otherwise.</summary>
	private protected static string Mask(string? secret) => string.IsNullOrEmpty(secret) ? "(none)" : "***";

	/// <summary>
	/// Masks any <c>user:password@</c> in a URL. Done on the text rather than a parsed <see cref="Uri"/>, since this must
	/// also redact a URL that fails to parse (such as one whose password contains an unescaped <c>@</c>).
	/// </summary>
	private protected static string MaskBaseUrl(string? url)
	{
		if (string.IsNullOrEmpty(url))
		{
			return "(none)";
		}

		var start = url.IndexOf("://", StringComparison.Ordinal);
		if (start < 0)
		{
			return url;
		}

		start += 3;
		var end = url.IndexOfAny(['/', '?', '#'], start);
		var authority = end < 0 ? url[start..] : url[start..end];
		var at = authority.LastIndexOf('@');
		return at < 0 ? url : $"{url[..start]}***{url[(start + at)..]}";
	}

	/// <summary>Checks the certificate, timeout and retry settings.</summary>
	private protected void ValidateConnection()
	{
		_ = Rapid7Transport.NormalizeThumbprint(TrustedServerCertificateThumbprint);
		ArgumentOutOfRangeException.ThrowIfNegative(MaxRetries);
		// The upper bounds are those of CancellationTokenSource.CancelAfter and Task.Delay, which would otherwise throw on
		// the first request rather than here.
		ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(Timeout, TimeSpan.Zero);
		ArgumentOutOfRangeException.ThrowIfGreaterThan(Timeout, MaxTimerDuration);
		ArgumentOutOfRangeException.ThrowIfLessThan(RetryBaseDelay, TimeSpan.Zero);
		ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(MaxRetryDelay, TimeSpan.Zero);
		ArgumentOutOfRangeException.ThrowIfGreaterThan(MaxRetryDelay, MaxTimerDuration);
	}
}
