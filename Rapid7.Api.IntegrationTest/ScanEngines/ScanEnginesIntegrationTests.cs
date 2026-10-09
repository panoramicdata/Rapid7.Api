using Rapid7.Api.Models;
using Rapid7.Api.Models.ScanEngines;
using System.Net;

namespace Rapid7.Api.IntegrationTest.ScanEngines;

/// <summary>
/// Reads scan engines, engine pools and the shared secret's state, and round-trips an empty engine pool the test creates.
/// No engine is paired, changed or removed, no engine joins or leaves a pool, and the shared secret is never generated or
/// revoked.
/// </summary>
[Collection(Rapid7TestGroup.Name)]
public class ScanEnginesIntegrationTests(Rapid7Fixture fixture)
{
	private static CancellationToken Ct => TestContext.Current.CancellationToken;

	private Rapid7Client Client => fixture.Client;

	[Fact]
	public async Task ExistingEngine_ReadsDetailsPoolsScansAndSites()
	{
		var engines = await Client.ScanEngines.ListAsync(Ct);
		var engineId = engines.Resources.Should().NotBeEmpty("every console has at least its local scan engine").And.Subject.First().Id!.Value;

		var engine = await Client.ScanEngines.GetAsync(engineId, Ct);
		var pools = await Client.ScanEngines.ListPoolsAsync(engineId, Ct);
		var scans = await Client.ScanEngines.ListScansAsync(engineId, new PageOptions { Size = 5 }, Ct);
		var sites = await Client.ScanEngines.ListSitesAsync(engineId, new PageOptions { Size = 5 }, Ct);

		engine.Id.Should().Be(engineId);
		engine.Name.Should().NotBeEmpty();
		engine.Status.Should().NotBeNull();
		pools.Resources.Should().OnlyContain(p => p.Engines.Contains(engineId));
		scans.Resources.Should().OnlyContain(s => s.Id > 0);
		sites.Resources.Should().OnlyContain(s => s.ScanEngine == engineId);
	}

	[Fact]
	public async Task Pools_ReadEveryPoolsEnginesAndSites()
	{
		var pools = await Client.ScanEnginePools.ListAsync(Ct);

		foreach (var listed in pools.Resources.Take(3))
		{
			var pool = await Client.ScanEnginePools.GetAsync(listed.Id, Ct);
			var engines = await Client.ScanEnginePools.ListEnginesAsync(listed.Id, Ct);
			var sites = await Client.ScanEnginePools.ListSitesAsync(listed.Id, Ct);

			pool.Name.Should().Be(listed.Name);
			engines.Resources.Should().BeEquivalentTo(pool.Engines);
			sites.Resources.Should().OnlyContain(id => id > 0);
		}
	}

	[Fact]
	public async Task EmptyPool_RoundTrip()
	{
		var name = Rapid7Fixture.UniqueName("pool");
		var created = await Client.ScanEnginePools.CreateAsync(new EnginePoolRequest { Name = name, Engines = [] }, Ct);
		var poolId = created.Id;
		try
		{
			(await Client.ScanEnginePools.GetAsync(poolId, Ct)).Name.Should().Be(name);

			var renamed = name + "-renamed";
			await Client.ScanEnginePools.UpdateAsync(poolId, new EnginePoolRequest { Name = renamed, Engines = [] }, Ct);
			await Client.ScanEnginePools.SetEnginesAsync(poolId, [], Ct);

			(await Client.ScanEnginePools.GetAsync(poolId, Ct)).Name.Should().Be(renamed);
			(await Client.ScanEnginePools.ListEnginesAsync(poolId, Ct)).Resources.Should().BeEmpty();
			(await Client.ScanEnginePools.ListSitesAsync(poolId, Ct)).Resources.Should().BeEmpty();
		}
		finally
		{
			await Client.ScanEnginePools.DeleteAsync(poolId, CancellationToken.None);
		}
	}

	[Fact]
	public async Task SharedSecret_ReadsItsStateWithoutGeneratingOne()
	{
		// Both reads answer 404 when no valid secret exists; neither creates one.
		try
		{
			var secondsLeft = await Client.ScanEngineSharedSecret.GetTimeToLiveAsync(Ct);
			var secret = await Client.ScanEngineSharedSecret.GetAsync(Ct);

			secondsLeft.Should().BePositive();
			secret.Should().NotBeNullOrWhiteSpace();
		}
		catch (Rapid7ApiException exception) when (exception.StatusCode == HttpStatusCode.NotFound)
		{
			exception.Message.Should().NotBeNullOrWhiteSpace();
		}
	}
}
