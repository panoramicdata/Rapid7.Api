using Rapid7.Api.Models;
using Rapid7.Api.Models.PolicyOverrides;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class PolicyOverridesTests
{
	private const string Override = """
		{
			"expires": "2026-12-31T00:00:00Z",
			"id": 420,
			"links": [ { "href": "https://console.test:3780/api/3/policy_overrides/420", "rel": "self" } ],
			"review": { "comment": "Accepted.", "date": "2026-10-02T09:30:00Z", "links": [], "name": "reviewer", "user": 9 },
			"scope": { "asset": 282, "links": [], "newResult": "pass", "originalResult": "fail", "rule": 53, "type": "specific-asset" },
			"state": "approved",
			"submit": { "comment": "A compensating control is in place.", "date": "2026-10-01T08:00:00Z", "links": [], "name": "submitter", "user": 7 }
		}
		""";

	private const string OverridePage = """{ "resources": [ """ + Override + """ ], "page": { "number": 0, "size": 10, "totalPages": 1, "totalResources": 1 }, "links": [] }""";

	private const string OverrideList = """{ "resources": [ """ + Override + """ ], "links": [] }""";

	private const string LinksJson = """{ "links": [ { "href": "https://console.test:3780/api/3/policy_overrides", "rel": "Policy Overrides" } ] }""";

	[Fact]
	public async Task GetPolicyOverridesAsync_SendsPaging()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyOverrides.GetPolicyOverridesAsync(new PageOptions { Page = 1, Size = 10 }, ct), OverridePage);

		call.ShouldBe(HttpMethod.Get, "/api/3/policy_overrides", "?page=1&size=10");
	}

	[Fact]
	public async Task GetPolicyOverridesAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.PolicyOverrides.GetPolicyOverridesAsync(null, ct), OverridePage);

		ShouldBeExampleOverride(page.Resources.Should().ContainSingle().Subject);
	}

	[Fact]
	public async Task GetForAssetAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyOverrides.GetForAssetAsync(282, ct), OverrideList);

		call.ShouldBe(HttpMethod.Get, "/api/3/assets/282/policy_overrides");
	}

	[Fact]
	public async Task GetForAssetAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.PolicyOverrides.GetForAssetAsync(282, ct), OverrideList);

		ShouldBeExampleOverride(list.Resources.Should().ContainSingle().Subject);
	}

	[Fact]
	public async Task CreateAsync_PostsTheOverride()
	{
		var request = new PolicyOverrideRequest
		{
			State = PolicyOverrideState.UnderReview,
			Scope = new PolicyOverrideScopeRequest { Rule = 53, Type = PolicyOverrideScopeType.SpecificAsset, Asset = 282, NewResult = PolicyOverrideResult.Pass },
			Submit = new PolicyOverrideSubmission { Comment = "A compensating control is in place." },
			Expires = new DateTimeOffset(2026, 12, 31, 0, 0, 0, TimeSpan.Zero)
		};

		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyOverrides.CreateAsync(request, ct), """{"id":420,"links":[]}""");

		call.ShouldBe(
			HttpMethod.Post,
			"/api/3/policy_overrides",
			body: """{"state":"under-review","scope":{"rule":53,"type":"specific-asset","asset":282,"newResult":"pass"},"submit":{"comment":"A compensating control is in place."},"expires":"2026-12-31T00:00:00+00:00"}""");
	}

	[Fact]
	public async Task CreateAsync_LeavesOutUnsetOptionalFields()
	{
		var request = new PolicyOverrideRequest
		{
			State = PolicyOverrideState.Approved,
			Scope = new PolicyOverrideScopeRequest { Rule = 53, Type = PolicyOverrideScopeType.AllAssets, NewResult = PolicyOverrideResult.NotApplicable },
			Submit = new PolicyOverrideSubmission { Comment = "Not relevant here." }
		};

		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyOverrides.CreateAsync(request, ct), """{"id":421,"links":[]}""");

		call.Body.Should().Be("""{"state":"approved","scope":{"rule":53,"type":"all-assets","newResult":"not-applicable"},"submit":{"comment":"Not relevant here."}}""");
	}

	[Fact]
	public async Task CreateAsync_ReadsTheNewIdentifier()
	{
		var request = new PolicyOverrideRequest
		{
			State = PolicyOverrideState.UnderReview,
			Scope = new PolicyOverrideScopeRequest { Rule = 53, Type = PolicyOverrideScopeType.SpecificAssetUntilNextScan, Asset = 282, NewResult = PolicyOverrideResult.Fixed },
			Submit = new PolicyOverrideSubmission { Comment = "Fixed manually." }
		};

		var created = await TestClient.ReadAsync((c, ct) => c.PolicyOverrides.CreateAsync(request, ct), """{"id":420,"links":[{"href":"https://console.test:3780/api/3/policy_overrides/420","rel":"self"}]}""");

		created.Id.Should().Be(420);
		created.Links.Should().ContainSingle().Which.Rel.Should().Be("self");
	}

	[Fact]
	public async Task GetAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyOverrides.GetAsync(420, ct), Override);

		call.ShouldBe(HttpMethod.Get, "/api/3/policy_overrides/420");
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
		=> ShouldBeExampleOverride(await TestClient.ReadAsync((c, ct) => c.PolicyOverrides.GetAsync(420, ct), Override));

	[Fact]
	public async Task GetAsync_ReadsAnUnreviewedOverride_WithUnrecognisedResults()
	{
		const string json = """{"id":5,"state":"under-review","scope":{"rule":1,"type":"all-assets","newResult":"pass","originalResult":"something-new"},"submit":{"comment":"c"},"links":[]}""";

		var item = await TestClient.ReadAsync((c, ct) => c.PolicyOverrides.GetAsync(5, ct), json);

		item.Review.Should().BeNull();
		item.Expires.Should().BeNull();
		item.State.Should().Be(PolicyOverrideState.UnderReview);
		item.Scope.Asset.Should().BeNull();
		item.Scope.OriginalResult.Should().Be(PolicyCheckResult.Unknown);
	}

	[Fact]
	public async Task DeleteAsync_SendsDelete()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyOverrides.DeleteAsync(420, ct), LinksJson);

		call.ShouldBe(HttpMethod.Delete, "/api/3/policy_overrides/420");
	}

	[Fact]
	public async Task DeleteAsync_ReadsTheLinks()
		=> (await TestClient.ReadAsync((c, ct) => c.PolicyOverrides.DeleteAsync(420, ct), LinksJson)).Links.Should().ContainSingle().Which.Rel.Should().Be("Policy Overrides");

	[Theory]
	[InlineData(PolicyOverrideStatusChange.Approve, "approve")]
	[InlineData(PolicyOverrideStatusChange.Reject, "reject")]
	[InlineData(PolicyOverrideStatusChange.Recall, "recall")]
	public async Task SetStatusAsync_PostsTheCommentToTheStatusPath(PolicyOverrideStatusChange status, string segment)
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyOverrides.SetStatusAsync(420, status, "Reviewed by the security team.", ct), "");

		call.ShouldBe(HttpMethod.Post, $"/api/3/policy_overrides/420/{segment}", body: "\"Reviewed by the security team.\"");
	}

	[Fact]
	public async Task SetStatusAsync_WithoutAComment_SendsNoBody()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyOverrides.SetStatusAsync(420, PolicyOverrideStatusChange.Approve, ct), "");

		call.ShouldBe(HttpMethod.Post, "/api/3/policy_overrides/420/approve");
	}

	[Fact]
	public async Task GetExpirationAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyOverrides.GetExpirationAsync(420, ct), "\"2026-12-31T00:00:00Z\"");

		call.ShouldBe(HttpMethod.Get, "/api/3/policy_overrides/420/expires");
	}

	[Fact]
	public async Task GetExpirationAsync_ReadsTheDate()
		=> (await TestClient.ReadAsync((c, ct) => c.PolicyOverrides.GetExpirationAsync(420, ct), "\"2026-12-31T00:00:00Z\""))
			.Should().Be(new DateTimeOffset(2026, 12, 31, 0, 0, 0, TimeSpan.Zero));

	[Fact]
	public async Task GetExpirationAsync_ReadsNoDateAsNull()
		=> (await TestClient.ReadAsync((c, ct) => c.PolicyOverrides.GetExpirationAsync(420, ct), "null")).Should().BeNull();

	[Fact]
	public async Task SetExpirationAsync_PutsTheDateAsAJsonString()
	{
		var expires = new DateTimeOffset(2027, 1, 31, 12, 0, 0, TimeSpan.Zero);

		var call = await TestClient.CaptureAsync((c, ct) => c.PolicyOverrides.SetExpirationAsync(420, expires, ct), LinksJson);

		call.ShouldBe(HttpMethod.Put, "/api/3/policy_overrides/420/expires", body: "\"2027-01-31T12:00:00+00:00\"");
	}

	[Fact]
	public Task SetExpirationAsync_DateInThePast_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.PolicyOverrides.SetExpirationAsync(420, DateTimeOffset.UnixEpoch, ct),
			HttpStatusCode.BadRequest,
			"""{"status":"BAD_REQUEST","message":"The expiration date must be in the future.","links":[]}""",
			"The expiration date must be in the future.");

	private static void ShouldBeExampleOverride(PolicyOverride item) => item.Should().BeEquivalentTo(new
	{
		Expires = new DateTimeOffset(2026, 12, 31, 0, 0, 0, TimeSpan.Zero),
		Id = 420L,
		Links = new[] { new { Rel = "self" } },
		Review = new { Comment = "Accepted.", Date = new DateTimeOffset(2026, 10, 2, 9, 30, 0, TimeSpan.Zero), Name = "reviewer", User = 9, Links = Array.Empty<object>() },
		Scope = new
		{
			Asset = 282L,
			NewResult = PolicyOverrideResult.Pass,
			OriginalResult = PolicyCheckResult.Fail,
			Rule = 53L,
			Type = PolicyOverrideScopeType.SpecificAsset,
			Links = Array.Empty<object>()
		},
		State = PolicyOverrideState.Approved,
		Submit = new { Comment = "A compensating control is in place.", Date = new DateTimeOffset(2026, 10, 1, 8, 0, 0, TimeSpan.Zero), Name = "submitter", User = 7 }
	});
}
