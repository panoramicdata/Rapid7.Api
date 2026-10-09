using Microsoft.Extensions.Configuration;
using Rapid7.Api.Models.Assets;

namespace Rapid7.Api.IntegrationTest;

/// <summary>
/// Clients for a live InsightVM Security Console and Insight platform organisation, configured from user secrets or
/// environment variables.
/// </summary>
/// <remarks>
/// <para>
/// The console tests need <c>Rapid7:BaseUrl</c>, <c>Rapid7:Username</c> and <c>Rapid7:Password</c> (and, for a self-signed
/// certificate, <c>Rapid7:TrustedServerCertificateThumbprint</c>; for a 2FA account, <c>Rapid7:TwoFactorToken</c>). The
/// Cloud Integrations and Bulk Export tests need <c>Rapid7Platform:Region</c> and <c>Rapid7Platform:ApiKey</c>. Set them
/// with <c>dotnet user-secrets set &lt;key&gt; &lt;value&gt; --project Rapid7.Api.IntegrationTest</c>, or as
/// <c>Rapid7__BaseUrl</c> etc. in the environment (see docs/TESTING.md).
/// </para>
/// <para>A missing setting fails the tests that need it, with a message naming it; nothing is skipped.</para>
/// </remarks>
public sealed class Rapid7Fixture : IDisposable
{
	/// <summary>The prefix of every object the integration tests create, so leftovers are recognisable.</summary>
	internal const string Prefix = "rapid7api-test-";

	private readonly Lazy<Rapid7Client> _client;
	private readonly Lazy<Rapid7CloudClient> _cloudClient;
	private readonly Lazy<Rapid7BulkExportClient> _bulkExportClient;

	public Rapid7Fixture()
	{
		Configuration = new ConfigurationBuilder()
			.AddUserSecrets<Rapid7Fixture>()
			.AddEnvironmentVariables()
			.Build();
		_client = new(() => new Rapid7Client(CreateOptions()));
		_cloudClient = new(() => new Rapid7CloudClient(CreatePlatformOptions()));
		_bulkExportClient = new(() => new Rapid7BulkExportClient(CreatePlatformOptions()));
	}

	public IConfiguration Configuration { get; }

	/// <summary>A Security Console client.</summary>
	public Rapid7Client Client => _client.Value;

	/// <summary>A Cloud Integrations (v4) client.</summary>
	public Rapid7CloudClient CloudClient => _cloudClient.Value;

	/// <summary>A Bulk Export client.</summary>
	public Rapid7BulkExportClient BulkExportClient => _bulkExportClient.Value;

	/// <summary>Console options from configuration, failing loudly when a required setting is missing.</summary>
	public Rapid7ClientOptions CreateOptions() => new()
	{
		BaseUrl = Required("Rapid7:BaseUrl"),
		Username = Required("Rapid7:Username"),
		Password = Required("Rapid7:Password"),
		TwoFactorToken = Configuration["Rapid7:TwoFactorToken"],
		TrustedServerCertificateThumbprint = Configuration["Rapid7:TrustedServerCertificateThumbprint"],
	};

	/// <summary>Platform options from configuration, failing loudly when a required setting is missing.</summary>
	public Rapid7PlatformOptions CreatePlatformOptions() => new()
	{
		Region = Required("Rapid7Platform:Region"),
		ApiKey = Required("Rapid7Platform:ApiKey"),
	};

	/// <summary>A unique, recognisable name for an object a test creates.</summary>
	public static string UniqueName(string what) => $"{Prefix}{what}-{Guid.NewGuid():N}";

	/// <summary>Search criteria that match no asset (a unique host name), so a dynamic group or tag using them stays empty.</summary>
	public static SearchCriteria NoAssets() => new()
	{
		Match = SearchMatch.All,
		Filters = [new SearchFilter(SearchField.HostName, SearchOperator.Is) { Value = UniqueName("no-such-host") }],
	};

	private string Required(string key)
		=> Configuration[key] is { Length: > 0 } value
			? value
			: throw new InvalidOperationException(
				$"Integration tests need '{key}'. Set it with 'dotnet user-secrets set {key} <value> --project Rapid7.Api.IntegrationTest' (see docs/TESTING.md).");

	public void Dispose()
	{
		if (_client.IsValueCreated)
		{
			_client.Value.Dispose();
		}

		if (_cloudClient.IsValueCreated)
		{
			_cloudClient.Value.Dispose();
		}

		if (_bulkExportClient.IsValueCreated)
		{
			_bulkExportClient.Value.Dispose();
		}
	}
}
