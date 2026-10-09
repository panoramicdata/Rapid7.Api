using Rapid7.Api.Models;

namespace Rapid7.Api.IntegrationTest.PolicyOverrides;

/// <summary>
/// Reads policy overrides from a live console. Nothing is created: an override changes the compliance results of a rule
/// the test does not own, so the create, status, expiry and delete operations are covered by unit tests only.
/// </summary>
[Collection(Rapid7TestGroup.Name)]
public class PolicyOverrideIntegrationTests(Rapid7Fixture fixture)
{
	private static CancellationToken Ct => TestContext.Current.CancellationToken;

	[Fact]
	public async Task GetPolicyOverridesAsync_ThenReadsTheFirstInEveryWay()
	{
		var client = fixture.Client;
		var page = await client.PolicyOverrides.GetPolicyOverridesAsync(new PageOptions { Size = 1 }, Ct);
		if (page.Resources.Count == 0 || page.Resources[0].Id is not long id)
		{
			return;
		}

		var item = await client.PolicyOverrides.GetAsync(id, Ct);

		item.Id.Should().Be(id);
		item.Scope.Rule.Should().BePositive();
		if (item.Expires is not null)
		{
			(await client.PolicyOverrides.GetExpirationAsync(id, Ct)).Should().Be(item.Expires);
		}

		if (item.Scope.Asset is long assetId)
		{
			var forAsset = await client.PolicyOverrides.GetForAssetAsync(assetId, Ct);
			forAsset.Resources.Should().Contain(o => o.Id == id);
		}
	}
}
