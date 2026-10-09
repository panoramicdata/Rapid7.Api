using Rapid7.Api.Test.Support;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Rapid7.Api.Test.Core;

/// <summary>Server certificate validation on the transport each Rapid7 client creates for itself.</summary>
public sealed class TransportTests : IDisposable
{
	private readonly X509Certificate2 _pinned = TestCertificates.Create();
	private readonly X509Certificate2 _other = TestCertificates.Create("CN=other.test");
	// SocketsHttpHandler passes the SslStream as the sender; the callback must not depend on it.
	private readonly object _sender = new();

	public void Dispose()
	{
		_pinned.Dispose();
		_other.Dispose();
	}

	/// <summary>The transport's TLS validation, as SocketsHttpHandler invokes it.</summary>
	private static Func<object, X509Certificate?, X509Chain?, SslPolicyErrors, bool> ValidationOf(Rapid7ClientOptions options)
	{
		using var handler = Rapid7Transport.Create(options);
		var validate = handler.SslOptions.RemoteCertificateValidationCallback!;
		return (sender, certificate, chain, errors) => validate(sender, certificate, chain, errors);
	}

	private static Func<object, X509Certificate?, X509Chain?, SslPolicyErrors, bool> PinnedCallback(string thumbprint)
		=> ValidationOf(new Rapid7ClientOptions { TrustedServerCertificateThumbprint = thumbprint });

	[Fact]
	public void NoThumbprintOrCallback_LeavesDefaultValidation()
	{
		using var handler = Rapid7Transport.Create(new Rapid7ClientOptions());

		handler.SslOptions.RemoteCertificateValidationCallback.Should().BeNull();
	}

	[Fact]
	public void PooledConnections_AreDroppedBeforeServersCloseThem()
	{
		using var handler = Rapid7Transport.Create(new Rapid7ClientOptions());

		handler.PooledConnectionIdleTimeout.Should().Be(TimeSpan.FromSeconds(5));
		handler.PooledConnectionLifetime.Should().Be(TimeSpan.FromMinutes(5));
	}

	[Fact]
	public void CustomCallback_TakesPrecedenceOverThumbprint_AndReceivesTheCertificate()
	{
		X509Certificate2? seen = null;
		var validate = ValidationOf(new Rapid7ClientOptions
		{
			ServerCertificateValidationCallback = (certificate, _, _) =>
			{
				seen = certificate;
				return false;
			},
			TrustedServerCertificateThumbprint = TestCertificates.Sha256(_pinned)
		});

		validate(_sender, _pinned, null, SslPolicyErrors.RemoteCertificateChainErrors).Should().BeFalse("the custom callback rejects, although the thumbprint would trust it");
		seen.Should().BeSameAs(_pinned);
	}

	[Fact]
	public void Pinned_TrustsThePinnedCertificate_HandedOverAsTheBaseType()
	{
		using var baseType = new X509Certificate(_pinned);

		PinnedCallback(TestCertificates.Sha256(_pinned))(_sender, baseType, null, SslPolicyErrors.RemoteCertificateChainErrors).Should().BeTrue();
	}

	[Theory]
	[InlineData(SslPolicyErrors.RemoteCertificateChainErrors)]
	[InlineData(SslPolicyErrors.RemoteCertificateNameMismatch | SslPolicyErrors.RemoteCertificateChainErrors)]
	public void Pinned_TrustsThePinnedCertificateDespiteErrors(SslPolicyErrors errors)
		=> PinnedCallback(TestCertificates.Sha256(_pinned))(_sender, _pinned, null, errors).Should().BeTrue();

	[Fact]
	public void Pinned_MatchesIgnoringCaseAndSeparators()
	{
		var hex = TestCertificates.Sha256(_pinned).ToLowerInvariant();
		var separated = string.Join(':', Enumerable.Range(0, 32).Select(i => hex.Substring(i * 2, 2)));

		PinnedCallback(separated)(_sender, _pinned, null, SslPolicyErrors.RemoteCertificateChainErrors).Should().BeTrue();
	}

	[Fact]
	public void Pinned_RejectsAnotherCertificateWithErrors()
		=> PinnedCallback(TestCertificates.Sha256(_pinned))(_sender, _other, null, SslPolicyErrors.RemoteCertificateChainErrors).Should().BeFalse();

	[Fact]
	public void Pinned_RejectsAMissingCertificateWithErrors()
		=> PinnedCallback(TestCertificates.Sha256(_pinned))(_sender, null, null, SslPolicyErrors.RemoteCertificateNotAvailable).Should().BeFalse();

	[Fact]
	public void Pinned_StillTrustsOtherCertificatesThatPassNormalValidation()
		=> PinnedCallback(TestCertificates.Sha256(_pinned))(_sender, _other, null, SslPolicyErrors.None).Should().BeTrue();

	[Fact]
	public void CreateTransport_RequiresOptions()
	{
		var act = () => Rapid7Transport.Create(null!);

		act.Should().Throw<ArgumentNullException>();
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData("  ")]
	public void NormalizeThumbprint_NoneIsNull(string? thumbprint)
		=> Rapid7Transport.NormalizeThumbprint(thumbprint).Should().BeNull();

	[Theory]
	[InlineData("ab cd")]
	[InlineData("0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef00")]
	[InlineData("DA39A3EE5E6B4B0D3255BFEF95601890AFD80709")]
	public void NormalizeThumbprint_RejectsWrongLength(string thumbprint)
	{
		var act = () => Rapid7Transport.NormalizeThumbprint(thumbprint);

		act.Should().Throw<ArgumentException>().WithMessage("*SHA-256*");
	}

	[Fact]
	public void NormalizeThumbprint_KeepsOnlyHexDigits()
		=> Rapid7Transport.NormalizeThumbprint(" 01-23:45 67" + new string('a', 56)).Should().Be("01234567" + new string('a', 56));

	[Fact]
	public void CustomCallback_ReceivesNoCertificate_WhenTlsHandsOverNone()
	{
		var called = false;
		var validate = ValidationOf(new Rapid7ClientOptions
		{
			ServerCertificateValidationCallback = (certificate, _, errors) =>
			{
				called = certificate is null && errors == SslPolicyErrors.RemoteCertificateNotAvailable;
				return true;
			}
		});

		validate(_sender, null, null, SslPolicyErrors.RemoteCertificateNotAvailable).Should().BeTrue();
		called.Should().BeTrue();
	}

	[Fact]
	public void PlatformOptions_PinTheSameWay()
	{
		using var handler = Rapid7Transport.Create(new Rapid7PlatformOptions { TrustedServerCertificateThumbprint = TestCertificates.Sha256(_pinned) });

		var callback = handler.SslOptions.RemoteCertificateValidationCallback!;
		callback(_sender, _pinned, null, SslPolicyErrors.RemoteCertificateChainErrors).Should().BeTrue();
		callback(_sender, _other, null, SslPolicyErrors.RemoteCertificateChainErrors).Should().BeFalse();
	}
}
