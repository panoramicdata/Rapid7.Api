using Rapid7.Api.Models.Sites;

namespace Rapid7.Api.IntegrationTest.Sites;

/// <summary>
/// A throwaway static site for the site membership round trips: created with a unique <c>rapid7api-test-</c> name and no
/// targets, and deleted on disposal. Nothing is ever scanned.
/// </summary>
internal sealed class TestSite : IAsyncDisposable
{
	private readonly Rapid7Client _client;

	private TestSite(Rapid7Client client, int id, string name)
	{
		_client = client;
		Id = id;
		Name = name;
	}

	/// <summary>The identifier of the site.</summary>
	public int Id { get; }

	/// <summary>The unique name of the site.</summary>
	public string Name { get; }

	/// <summary>Creates an empty static site.</summary>
	public static async Task<TestSite> CreateAsync(Rapid7Client client, CancellationToken cancellationToken)
	{
		var name = Rapid7Fixture.UniqueName("site");
		var created = await client.Sites.CreateAsync(
			new SiteCreateRequest { Name = name, Description = "Created by Rapid7.Api integration tests." },
			cancellationToken);
		return new TestSite(client, created.Id, name);
	}

	/// <inheritdoc />
	public async ValueTask DisposeAsync() => await _client.Sites.DeleteAsync(Id, CancellationToken.None);
}
