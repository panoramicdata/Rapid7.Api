using Rapid7.Api.Models.Assets;
using Rapid7.Api.Models.Tags;
using Rapid7.Api.Test.Support;
using System.Net;
using System.Text.Json;

namespace Rapid7.Api.Test.Groups;

public class TagsTests
{
	private const string Criteria = """
		{
			"match": "all",
			"filters": [
				{ "field": "risk-score", "operator": "is-greater-than", "value": 5000 },
				{ "field": "ip-address", "operator": "in-range", "lower": "10.0.0.1", "upper": "10.0.0.255" },
				{ "field": "operating-system", "operator": "in", "values": ["windows", "linux"] }
			]
		}
		""";

	private const string TagJson = $$"""
		{
			"id": 6,
			"name": "Very High",
			"type": "criticality",
			"source": "built-in",
			"created": "2017-10-07T23:50:01.205Z",
			"color": "purple",
			"riskModifier": 2,
			"searchCriteria": {{Criteria}},
			"links": [{ "href": "https://console.test:3780/api/3/tags/6", "rel": "self" }]
		}
		""";

	private static readonly TagRequest Request = new()
	{
		Name = "Datacentre A",
		Type = TagType.Location,
		Color = TagColor.Blue,
		SearchCriteria = new SearchCriteria
		{
			Match = SearchMatch.Any,
			Filters = [new SearchFilter(SearchField.HostName, SearchOperator.StartsWith) { Value = "dca-" }]
		}
	};

	[Fact]
	public async Task ListAsync_SendsFiltersAndPaging()
		=> (await TestClient.CaptureAsync(
				(c, ct) => c.Tags.ListAsync(new TagListOptions { Name = "Very High", Type = TagType.Criticality, Size = 500 }, ct),
				ScanJson.Empty))
			.ShouldBe(HttpMethod.Get, "/api/3/tags", "?name=Very%20High&type=criticality&size=500");

	[Fact]
	public async Task ListAsync_WithoutOptions_SendsNoQuery()
		=> (await TestClient.CaptureAsync((c, ct) => c.Tags.ListAsync(null, ct), ScanJson.Empty))
			.ShouldBe(HttpMethod.Get, "/api/3/tags");

	[Fact]
	public async Task CreateAsync_PostsTheTag()
		=> (await TestClient.CaptureAsync((c, ct) => c.Tags.CreateAsync(Request, ct), """{"id":12,"links":[]}"""))
			.ShouldBe(
				HttpMethod.Post,
				"/api/3/tags",
				body: """{"name":"Datacentre A","type":"location","color":"blue","searchCriteria":{"match":"any","filters":[{"field":"host-name","operator":"starts-with","value":"dca-"}]}}""");

	[Fact]
	public async Task GetAsync_SendsGet()
		=> (await TestClient.CaptureAsync((c, ct) => c.Tags.GetAsync(6, ct), TagJson))
			.ShouldBe(HttpMethod.Get, "/api/3/tags/6");

	[Fact]
	public async Task UpdateAsync_PutsTheTag_LeavingOutNulls()
		=> (await TestClient.CaptureAsync(
				(c, ct) => c.Tags.UpdateAsync(6, new TagRequest { Name = "Very High", Type = TagType.Criticality, RiskModifier = 2.5 }, ct),
				ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Put, "/api/3/tags/6", body: """{"name":"Very High","type":"criticality","riskModifier":2.5}""");

	[Fact]
	public async Task DeleteAsync_SendsDelete()
		=> (await TestClient.CaptureAsync((c, ct) => c.Tags.DeleteAsync(6, ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Delete, "/api/3/tags/6");

	[Fact]
	public async Task GetSearchCriteriaAsync_SendsGet()
		=> (await TestClient.CaptureAsync((c, ct) => c.Tags.GetSearchCriteriaAsync(6, ct), Criteria))
			.ShouldBe(HttpMethod.Get, "/api/3/tags/6/search_criteria");

	[Fact]
	public async Task UpdateSearchCriteriaAsync_PutsTheCriteria()
		=> (await TestClient.CaptureAsync(
				(c, ct) => c.Tags.UpdateSearchCriteriaAsync(
					6,
					new SearchCriteria
					{
						Match = SearchMatch.All,
						Filters =
						[
							new SearchFilter(SearchField.IpAddress, SearchOperator.InRange) { Lower = "10.0.0.1", Upper = "10.0.0.255" },
							new SearchFilter(SearchField.OperatingSystem, SearchOperator.In) { Values = ["windows", "linux"] },
						]
					},
					ct),
				ScanJson.LinksOnly))
			.ShouldBe(
				HttpMethod.Put,
				"/api/3/tags/6/search_criteria",
				body: """{"match":"all","filters":[{"field":"ip-address","operator":"in-range","lower":"10.0.0.1","upper":"10.0.0.255"},{"field":"operating-system","operator":"in","values":["windows","linux"]}]}""");

	[Fact]
	public async Task DeleteSearchCriteriaAsync_SendsDelete()
		=> (await TestClient.CaptureAsync((c, ct) => c.Tags.DeleteSearchCriteriaAsync(6, ct), ScanJson.LinksOnly))
			.ShouldBe(HttpMethod.Delete, "/api/3/tags/6/search_criteria");

	[Fact]
	public async Task GetAsync_MapsEveryField()
	{
		var tag = await TestClient.ReadAsync((c, ct) => c.Tags.GetAsync(6, ct), TagJson);

		tag.Id.Should().Be(6);
		tag.Name.Should().Be("Very High");
		tag.Type.Should().Be(TagType.Criticality);
		tag.Source.Should().Be(TagSource.BuiltIn);
		tag.Created.Should().Be(new DateTimeOffset(2017, 10, 7, 23, 50, 1, 205, TimeSpan.Zero));
		tag.Color.Should().Be(TagColor.Purple);
		tag.RiskModifier.Should().Be(2);
		tag.SearchCriteria!.Match.Should().Be(SearchMatch.All);
		tag.SearchCriteria.Filters.Should().HaveCount(3);
		tag.Links.Should().ContainSingle().Which.Rel.Should().Be("self");
	}

	[Fact]
	public async Task ListAsync_MapsTagsAndDefaults()
	{
		var page = await TestClient.ReadAsync(
			(c, ct) => c.Tags.ListAsync(null, ct),
			$$"""{"resources":[{{TagJson}},{"id":7,"name":"Owner: ops","type":"owner","source":"custom","color":"default","riskModifier":"1.5","links":[]},{"id":8}],"page":{"number":0,"size":10,"totalPages":1,"totalResources":3},"links":[]}""");

		page.Resources.Should().HaveCount(3);
		page.Resources[1].Type.Should().Be(TagType.Owner);
		page.Resources[1].Source.Should().Be(TagSource.Custom);
		page.Resources[1].Color.Should().Be(TagColor.Default);
		page.Resources[1].RiskModifier.Should().Be(1.5);
		page.Resources[1].SearchCriteria.Should().BeNull();
		page.Resources[2].Name.Should().BeEmpty();
		page.Resources[2].Type.Should().Be(TagType.Unknown);
		page.PageInfo!.TotalResources.Should().Be(3);
	}

	[Theory]
	[InlineData("custom", TagType.Custom)]
	[InlineData("location", TagType.Location)]
	public async Task GetAsync_MapsOtherTypes(string wire, TagType expected)
		=> (await TestClient.ReadAsync((c, ct) => c.Tags.GetAsync(6, ct), $$"""{"name":"x","type":"{{wire}}","links":[]}"""))
			.Type.Should().Be(expected);

	[Theory]
	[InlineData("green", TagColor.Green)]
	[InlineData("orange", TagColor.Orange)]
	[InlineData("red", TagColor.Red)]
	[InlineData("blue", TagColor.Blue)]
	[InlineData("teal", TagColor.Unknown)]
	public async Task GetAsync_MapsEveryColour(string wire, TagColor expected)
		=> (await TestClient.ReadAsync((c, ct) => c.Tags.GetAsync(6, ct), $$"""{"name":"x","type":"custom","color":"{{wire}}","links":[]}"""))
			.Color.Should().Be(expected);

	[Fact]
	public async Task GetSearchCriteriaAsync_MapsEveryOperand()
	{
		var criteria = await TestClient.ReadAsync((c, ct) => c.Tags.GetSearchCriteriaAsync(6, ct), Criteria);

		criteria.Match.Should().Be(SearchMatch.All);
		criteria.Filters[0].Field.Should().Be("risk-score");
		criteria.Filters[0].Operator.Should().Be("is-greater-than");
		criteria.Filters[0].Value.Should().BeOfType<JsonElement>().Which.GetInt32().Should().Be(5000);
		criteria.Filters[1].Lower.Should().BeOfType<JsonElement>().Which.GetString().Should().Be("10.0.0.1");
		criteria.Filters[1].Upper.Should().BeOfType<JsonElement>().Which.GetString().Should().Be("10.0.0.255");
		criteria.Filters[2].Values!.Select(v => ((JsonElement)v).GetString()).Should().Equal("windows", "linux");
	}

	[Fact]
	public async Task CreateAsync_ReadsTheNewIdentifier()
		=> (await TestClient.ReadAsync((c, ct) => c.Tags.CreateAsync(Request, ct), """{"id":12,"links":[]}""")).Id.Should().Be(12);

	[Fact]
	public Task CreateAsync_DuplicateName_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.Tags.CreateAsync(Request, ct),
			HttpStatusCode.BadRequest,
			"""{"status":"BAD_REQUEST","message":"A tag with that name and type already exists.","links":[]}""",
			"A tag with that name and type already exists.");
}
