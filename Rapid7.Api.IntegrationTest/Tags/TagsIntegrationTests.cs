using Rapid7.Api.Models.Assets;
using Rapid7.Api.Models.Tags;

namespace Rapid7.Api.IntegrationTest.Tags;

/// <summary>
/// Reads tags and their members, and round-trips a custom tag the test creates. The test's tag is never applied to an
/// existing asset, asset group or site: its criteria match no asset.
/// </summary>
[Collection(Rapid7TestGroup.Name)]
public class TagsIntegrationTests(Rapid7Fixture fixture)
{
	private static CancellationToken Ct => TestContext.Current.CancellationToken;

	private Rapid7Client Client => fixture.Client;

	[Fact]
	public async Task ListAsync_FiltersByType()
	{
		var page = await Client.Tags.ListAsync(new TagListOptions { Type = TagType.Criticality }, Ct);

		page.Resources.Should().NotBeEmpty("every console has the built-in criticality tags")
			.And.OnlyContain(t => t.Type == TagType.Criticality);
	}

	[Fact]
	public async Task ExistingTag_ReadsDetailsAndMembers()
	{
		var page = await Client.Tags.ListAsync(new TagListOptions { Size = 1 }, Ct);
		var tagId = page.Resources.Should().ContainSingle().Subject.Id!.Value;

		var tag = await Client.Tags.GetAsync(tagId, Ct);
		var assets = await Client.TagMembers.ListAssetsAsync(tagId, Ct);
		var groups = await Client.TagMembers.ListAssetGroupsAsync(tagId, Ct);
		var sites = await Client.TagMembers.ListSitesAsync(tagId, Ct);

		tag.Id.Should().Be(tagId);
		assets.Resources.Should().OnlyContain(a => a.Id > 0 && a.Sources.Count > 0);
		groups.Resources.Should().OnlyContain(id => id > 0);
		sites.Resources.Should().OnlyContain(id => id > 0);
	}

	[Fact]
	public async Task CustomTag_RoundTripWithCriteria()
	{
		var name = Rapid7Fixture.UniqueName("tag");
		var created = await Client.Tags.CreateAsync(new TagRequest { Name = name, Type = TagType.Custom, Color = TagColor.Green }, Ct);
		var tagId = created.Id;
		try
		{
			var read = await Client.Tags.GetAsync(tagId, Ct);
			read.Name.Should().Be(name);
			read.Source.Should().Be(TagSource.Custom);

			await Client.Tags.UpdateAsync(tagId, new TagRequest { Name = name, Type = TagType.Custom, Color = TagColor.Orange }, Ct);
			(await Client.Tags.GetAsync(tagId, Ct)).Color.Should().Be(TagColor.Orange);

			await Client.Tags.UpdateSearchCriteriaAsync(tagId, Rapid7Fixture.NoAssets(), Ct);
			var criteria = await Client.Tags.GetSearchCriteriaAsync(tagId, Ct);
			criteria.Filters.Should().ContainSingle().Which.Field.Should().Be(SearchField.HostName);
			await Client.Tags.DeleteSearchCriteriaAsync(tagId, Ct);

			(await Client.TagMembers.ListAssetsAsync(tagId, Ct)).Resources.Should().BeEmpty();
			await Client.TagMembers.RemoveAllAssetGroupsAsync(tagId, Ct);
			await Client.TagMembers.RemoveAllSitesAsync(tagId, Ct);
			await Client.TagMembers.SetSitesAsync(tagId, [], Ct);
			await Client.TagMembers.SetAssetGroupsAsync(tagId, [], Ct);
			(await Client.TagMembers.ListSitesAsync(tagId, Ct)).Resources.Should().BeEmpty();
			(await Client.TagMembers.ListAssetGroupsAsync(tagId, Ct)).Resources.Should().BeEmpty();
		}
		finally
		{
			await Client.Tags.DeleteAsync(tagId, CancellationToken.None);
		}
	}
}
