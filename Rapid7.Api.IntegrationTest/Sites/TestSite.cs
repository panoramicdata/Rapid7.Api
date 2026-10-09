using Rapid7.Api.Models;
using Refit;
using System.Text.Json.Serialization;

namespace Rapid7.Api.IntegrationTest.Sites;

/// <summary>
/// A throwaway static site for the site membership round trips: created with a unique <c>rapid7api-test-</c> name and no
/// targets, and deleted on disposal. Nothing is ever scanned.
/// </summary>
/// <remarks>
/// Site create and delete belong to the site CRUD interface, which another branch adds; until it is merged this helper
/// reaches the two operations through a private Refit interface over the same client pipeline.
/// </remarks>
internal sealed class TestSite : IAsyncDisposable
{
	private readonly ILifecycle _lifecycle;

	private TestSite(ILifecycle lifecycle, int id, string name)
	{
		_lifecycle = lifecycle;
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
		var lifecycle = client.For<ILifecycle>();
		var name = Rapid7Fixture.UniqueName("site");
		var created = await lifecycle.CreateAsync(new CreateRequest { Name = name, Description = "Created by Rapid7.Api integration tests." }, cancellationToken);
		return new TestSite(lifecycle, created.Id, name);
	}

	/// <inheritdoc />
	public async ValueTask DisposeAsync() => await _lifecycle.DeleteAsync(Id, CancellationToken.None);

	public interface ILifecycle
	{
		[Post("api/3/sites")]
		Task<CreatedReference<int>> CreateAsync([Body] CreateRequest request, CancellationToken cancellationToken);

		[Delete("api/3/sites/{siteId}")]
		Task<Links> DeleteAsync(int siteId, CancellationToken cancellationToken);
	}

	public sealed class CreateRequest
	{
		[JsonPropertyName("name")]
		public required string Name { get; init; }

		[JsonPropertyName("description")]
		public string? Description { get; init; }
	}
}
