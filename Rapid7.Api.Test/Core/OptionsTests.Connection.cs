using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Core;

public partial class OptionsTests
{
	private static readonly TimeSpan MaxTimer = TimeSpan.FromMilliseconds(int.MaxValue);

	public static TheoryData<string, Action<Rapid7ConnectionOptions>> InvalidConnectionSettings => new()
	{
		{ "MaxRetries", o => o.MaxRetries = -1 },
		{ "Timeout", o => o.Timeout = TimeSpan.Zero },
		{ "Timeout", o => o.Timeout = Timeout.InfiniteTimeSpan },
		{ "Timeout", o => o.Timeout = MaxTimer + TimeSpan.FromMilliseconds(1) },
		{ "RetryBaseDelay", o => o.RetryBaseDelay = TimeSpan.FromTicks(-1) },
		{ "MaxRetryDelay", o => o.MaxRetryDelay = TimeSpan.Zero },
		{ "MaxRetryDelay", o => o.MaxRetryDelay = MaxTimer + TimeSpan.FromMilliseconds(1) }
	};

	[Theory]
	[MemberData(nameof(InvalidConnectionSettings))]
	public void ConnectionSettings_OutOfRange_AreRejected_ByEveryClient(string setting, Action<Rapid7ConnectionOptions> tweak)
	{
		_ = setting;
		var console = () => Construct(TestClient.ConsoleOptions(tweak));
		var platform = () => Construct(TestClient.PlatformOptions(tweak));

		console.Should().Throw<ArgumentOutOfRangeException>();
		platform.Should().Throw<ArgumentOutOfRangeException>();
	}

	[Fact]
	public void ConnectionSettings_AtTheirLimits_AreAccepted()
	{
		static void Limits(Rapid7ConnectionOptions o) => (o.MaxRetries, o.Timeout, o.RetryBaseDelay, o.MaxRetryDelay) = (0, MaxTimer, TimeSpan.Zero, MaxTimer);

		Construct(TestClient.ConsoleOptions(Limits));
		Construct(TestClient.PlatformOptions(Limits));
	}

	[Theory]
	[InlineData("abc")]
	[InlineData("DA39A3EE5E6B4B0D3255BFEF95601890AFD80709")]
	public void AThumbprintThatIsNotSha256_IsRejected(string thumbprint)
	{
		var act = () => Construct(TestClient.ConsoleOptions(o => o.TrustedServerCertificateThumbprint = thumbprint));

		act.Should().Throw<ArgumentException>().WithMessage("*SHA-256*");
	}

	[Fact]
	public void NullOptions_AreRejected_ByEveryConstructor()
	{
		Action[] constructors =
		[
			() => _ = new Rapid7Client(null!),
			() => _ = new Rapid7Client(null!, new StubHandler()),
			() => _ = new Rapid7CloudClient(null!),
			() => _ = new Rapid7CloudClient(null!, new StubHandler()),
			() => _ = new Rapid7BulkExportClient(null!),
			() => _ = new Rapid7BulkExportClient(null!, new StubHandler())
		];

		constructors.Should().AllSatisfy(c => c.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("options"));
	}

	[Fact]
	public void NullInnerHandler_IsRejected_ByEveryClient()
	{
		Action[] constructors =
		[
			() => _ = new Rapid7Client(TestClient.ConsoleOptions(), null!),
			() => _ = new Rapid7CloudClient(TestClient.PlatformOptions(), null!),
			() => _ = new Rapid7BulkExportClient(TestClient.PlatformOptions(), null!)
		];

		constructors.Should().AllSatisfy(c => c.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("innerHandler"));
	}

	[Fact]
	public void InvalidOptions_AreRejected_ByThePublicConstructors_BeforeATransportIsCreated()
	{
		var console = () => new Rapid7Client(new Rapid7ClientOptions());
		var cloud = () => new Rapid7CloudClient(new Rapid7PlatformOptions());
		var export = () => new Rapid7BulkExportClient(new Rapid7PlatformOptions());

		console.Should().Throw<ArgumentException>();
		cloud.Should().Throw<ArgumentException>();
		export.Should().Throw<ArgumentException>();
	}
}
