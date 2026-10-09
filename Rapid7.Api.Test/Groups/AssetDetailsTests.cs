using System.Net;
using Rapid7.Api.Models.Assets;
using Rapid7.Api.Test.Support;
using static Rapid7.Api.Test.Groups.AssetSamples;

namespace Rapid7.Api.Test.Groups;

public class AssetDetailsTests
{
	[Fact]
	public Task ListDatabasesAsync_SendsGet()
		=> SendsAsync((c, ct) => c.AssetDetails.ListDatabasesAsync(282, ct), HttpMethod.Get, "/api/3/assets/282/databases", response: EmptyList);

	[Fact]
	public Task ListFilesAsync_SendsGet()
		=> SendsAsync((c, ct) => c.AssetDetails.ListFilesAsync(282, ct), HttpMethod.Get, "/api/3/assets/282/files", response: EmptyList);

	[Fact]
	public Task ListSoftwareAsync_SendsGet()
		=> SendsAsync((c, ct) => c.AssetDetails.ListSoftwareAsync(282, ct), HttpMethod.Get, "/api/3/assets/282/software", response: EmptyList);

	[Fact]
	public Task ListUserGroupsAsync_SendsGet()
		=> SendsAsync((c, ct) => c.AssetDetails.ListUserGroupsAsync(282, ct), HttpMethod.Get, "/api/3/assets/282/user_groups", response: EmptyList);

	[Fact]
	public Task ListUsersAsync_SendsGet()
		=> SendsAsync((c, ct) => c.AssetDetails.ListUsersAsync(282, ct), HttpMethod.Get, "/api/3/assets/282/users", response: EmptyList);

	[Fact]
	public Task ListTagsAsync_SendsGet()
		=> SendsAsync((c, ct) => c.AssetDetails.ListTagsAsync(282, ct), HttpMethod.Get, "/api/3/assets/282/tags", response: EmptyList);

	[Fact]
	public Task AddTagAsync_SendsPutWithoutABody()
		=> SendsAsync((c, ct) => c.AssetDetails.AddTagAsync(282, 6, ct), HttpMethod.Put, "/api/3/assets/282/tags/6");

	[Fact]
	public Task RemoveTagAsync_SendsDelete()
		=> SendsAsync((c, ct) => c.AssetDetails.RemoveTagAsync(282, 6, ct), HttpMethod.Delete, "/api/3/assets/282/tags/6");

	[Fact]
	public async Task ListDatabasesAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.AssetDetails.ListDatabasesAsync(282, ct), List(DatabaseJson));

		ShouldRoundTrip(list.Resources.Should().ContainSingle().Subject, DatabaseJson);
		list.Links.Should().ContainSingle();
	}

	[Fact]
	public async Task ListFilesAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.AssetDetails.ListFilesAsync(282, ct), List(AssetFileJson));

		var file = list.Resources.Should().ContainSingle().Subject;
		ShouldRoundTrip(file, AssetFileJson);
		file.Size.Should().Be(-1);
		file.Attributes[0].Value.Should().Be("Remote Admin");
	}

	[Fact]
	public async Task ListSoftwareAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.AssetDetails.ListSoftwareAsync(282, ct), List(SoftwareJson));

		ShouldRoundTrip(list.Resources.Should().ContainSingle().Subject, SoftwareJson);
	}

	[Fact]
	public async Task ListUserGroupsAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.AssetDetails.ListUserGroupsAsync(282, ct), List(GroupAccountJson));

		ShouldRoundTrip(list.Resources.Should().ContainSingle().Subject, GroupAccountJson);
	}

	[Fact]
	public async Task ListUsersAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.AssetDetails.ListUsersAsync(282, ct), List(UserAccountJson));

		ShouldRoundTrip(list.Resources.Should().ContainSingle().Subject, UserAccountJson);
	}

	[Fact]
	public async Task ListTagsAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.AssetDetails.ListTagsAsync(282, ct), List(AssetTagJson));

		var tag = list.Resources.Should().ContainSingle().Subject;
		ShouldRoundTrip(tag, AssetTagJson);
		tag.Color.Should().Be(AssetTagColor.Red);
		tag.Type.Should().Be(AssetTagType.Criticality);
		tag.Source.Should().Be(AssetTagOrigin.Custom);
		tag.Sources[0].Source.Should().Be(AssetTagSourceType.Site);
		tag.RiskModifier.Should().Be(2);
		tag.Created.Should().Be(new DateTimeOffset(2017, 10, 7, 23, 50, 1, 205, TimeSpan.Zero));
		tag.SearchCriteria!.Filters.Should().HaveCount(3);
	}

	[Fact]
	public Task ListTagsAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync((c, ct) => c.AssetDetails.ListTagsAsync(999, ct), HttpStatusCode.NotFound, NotFound, NotFoundMessage);
}
