using Rapid7.Api.Models;
using Rapid7.Api.Models.AssetGroups;
using Rapid7.Api.Models.Assets;

namespace Rapid7.Api.IntegrationTest.AssetGroups;

/// <summary>
/// Reads asset groups and agents, and round-trips static and dynamic asset groups the test creates. Only the test's own
/// groups change: existing assets are added to and removed from them (the assets themselves are untouched), and no tag or
/// user is granted to anything.
/// </summary>
[Collection(Rapid7TestGroup.Name)]
public class AssetGroupsIntegrationTests(Rapid7Fixture fixture)
{
	private static CancellationToken Ct => TestContext.Current.CancellationToken;

	private Rapid7Client Client => fixture.Client;

	[Fact]
	public async Task ListAsync_FiltersByNameAndType()
	{
		var page = await Client.AssetGroups.ListAsync(new AssetGroupListOptions { Type = AssetGroupType.Dynamic, Size = 10 }, Ct);

		page.Resources.Should().OnlyContain(g => g.Type == AssetGroupType.Dynamic);
	}

	[Fact]
	public async Task ExistingGroup_ReadsDetailsMembersTagsAndUsers()
	{
		var page = await Client.AssetGroups.ListAsync(new AssetGroupListOptions { Size = 1 }, Ct);
		var groupId = page.Resources.Should().ContainSingle("the console must hold at least one asset group").Subject.Id!.Value;

		var group = await Client.AssetGroups.GetAsync(groupId, Ct);
		var assets = await Client.AssetGroupMembers.ListAssetsAsync(groupId, Ct);
		var tags = await Client.AssetGroupTags.ListAsync(groupId, Ct);
		var users = await Client.AssetGroupMembers.ListUsersAsync(groupId, Ct);

		group.Id.Should().Be(groupId);
		assets.Resources.Should().OnlyContain(id => id > 0);
		tags.Resources.Should().OnlyContain(id => id > 0);
		users.Resources.Should().OnlyContain(id => id > 0);
	}

	[Fact]
	public async Task StaticGroup_RoundTripWithMembers()
	{
		var name = Rapid7Fixture.UniqueName("static-group");
		var created = await Client.AssetGroups.CreateAsync(new AssetGroupRequest { Name = name, Type = AssetGroupType.Static, Description = "Created by Rapid7.Api integration tests" }, Ct);
		var groupId = created.Id;
		try
		{
			var read = await Client.AssetGroups.GetAsync(groupId, Ct);
			read.Name.Should().Be(name);
			read.Type.Should().Be(AssetGroupType.Static);

			await Client.AssetGroups.UpdateAsync(groupId, new AssetGroupRequest { Name = name, Type = AssetGroupType.Static, Description = "Updated" }, Ct);
			(await Client.AssetGroups.GetAsync(groupId, Ct)).Description.Should().Be("Updated");

			await ExerciseMembersAsync(groupId);

			(await Client.AssetGroupTags.ListAsync(groupId, Ct)).Resources.Should().BeEmpty();
			await Client.AssetGroupTags.RemoveAllAsync(groupId, Ct);

			var users = await Client.AssetGroupMembers.ListUsersAsync(groupId, Ct);
			await Client.AssetGroupMembers.SetUsersAsync(groupId, users.Resources, Ct);
		}
		finally
		{
			await Client.AssetGroups.DeleteAsync(groupId, CancellationToken.None);
		}
	}

	[Fact]
	public async Task DynamicGroup_RoundTripWithSearchCriteria()
	{
		var name = Rapid7Fixture.UniqueName("dynamic-group");
		var created = await Client.AssetGroups.CreateAsync(new AssetGroupRequest { Name = name, Type = AssetGroupType.Dynamic, SearchCriteria = Rapid7Fixture.NoAssets() }, Ct);
		var groupId = created.Id;
		try
		{
			var criteria = await Client.AssetGroups.GetSearchCriteriaAsync(groupId, Ct);
			criteria.Filters.Should().ContainSingle().Which.Field.Should().Be(SearchField.HostName);

			var replacement = new SearchCriteria
			{
				Match = SearchMatch.Any,
				Filters = [new SearchFilter(SearchField.RiskScore, SearchOperator.IsGreaterThan) { Value = 1e15 }],
			};
			await Client.AssetGroups.SetSearchCriteriaAsync(groupId, replacement, Ct);

			var updated = await Client.AssetGroups.GetSearchCriteriaAsync(groupId, Ct);
			updated.Match.Should().Be(SearchMatch.Any);
			updated.Filters.Should().ContainSingle().Which.Field.Should().Be(SearchField.RiskScore);
		}
		finally
		{
			await Client.AssetGroups.DeleteAsync(groupId, CancellationToken.None);
		}
	}

	[Fact]
	public async Task Agents_ListAPage()
	{
		var page = await Client.Agents.ListAsync(new PageOptions { Size = 5 }, Ct);

		page.Resources.Should().OnlyContain(a => a.Id > 0);
	}

	private async Task ExerciseMembersAsync(int groupId)
	{
		var assets = await Client.Assets.ListAsync(new PageOptions { Size = 2 }, Ct);
		var assetIds = assets.Resources.Select(a => a.Id!.Value).ToList();
		if (assetIds.Count == 0)
		{
			return;
		}

		await Client.AssetGroupMembers.SetAssetsAsync(groupId, assetIds, Ct);
		(await Client.AssetGroupMembers.ListAssetsAsync(groupId, Ct)).Resources.Should().BeEquivalentTo(assetIds);

		await Client.AssetGroupMembers.RemoveAssetAsync(groupId, assetIds[0], Ct);
		(await Client.AssetGroupMembers.ListAssetsAsync(groupId, Ct)).Resources.Should().NotContain(assetIds[0]);

		await Client.AssetGroupMembers.AddAssetAsync(groupId, assetIds[0], Ct);
		(await Client.AssetGroupMembers.ListAssetsAsync(groupId, Ct)).Resources.Should().Contain(assetIds[0]);

		await Client.AssetGroupMembers.RemoveAllAssetsAsync(groupId, Ct);
		(await Client.AssetGroupMembers.ListAssetsAsync(groupId, Ct)).Resources.Should().BeEmpty();
	}
}
