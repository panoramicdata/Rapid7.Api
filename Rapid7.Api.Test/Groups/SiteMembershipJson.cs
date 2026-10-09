namespace Rapid7.Api.Test.Groups;

/// <summary>Responses shared by the tests of the site membership interfaces (credentials, targets, assets, tags, users, discovery).</summary>
internal static class SiteMembershipJson
{
	/// <summary>The links-only answer the console gives to most site membership updates and deletes.</summary>
	public const string Links = """
		{
			"links": [
				{ "href": "https://console.test:3780/api/3/sites/7", "rel": "self" }
			]
		}
		""";

	/// <summary>The answer to a POST that adds to a site: an identifier and a link.</summary>
	public const string Reference = """
		{
			"id": 7,
			"links": [
				{ "href": "https://console.test:3780/api/3/sites/7", "rel": "self" }
			]
		}
		""";

	/// <summary>The console's error body for a site that does not exist.</summary>
	public const string NotFound = """{"status":"NOT_FOUND","message":"The resource with identifier 404 could not be found.","links":[]}""";

	/// <summary>The message of <see cref="NotFound"/>.</summary>
	public const string NotFoundMessage = "The resource with identifier 404 could not be found.";

	/// <summary>Asserts the single self link of <see cref="Links"/> and <see cref="Reference"/>.</summary>
	public static void ShouldLinkToSite(this Rapid7.Api.Models.Links links)
		=> links.Items.Should().ContainSingle().Which.Href.Should().Be("https://console.test:3780/api/3/sites/7");
}
