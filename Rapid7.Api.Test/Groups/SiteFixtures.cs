using Rapid7.Api.Models;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

/// <summary>Responses and assertions shared by the tests of the site interfaces (sites, alerts, schedules and settings).</summary>
internal static class SiteFixtures
{
	public const int SiteId = 7;

	/// <summary>A links-only answer, as the console returns to a PUT or DELETE.</summary>
	public const string LinksJson = """{"links":[{"href":"https://console.test:3780/api/3/sites/7","rel":"self"}]}""";

	/// <summary>The answer to a create: the new identifier and a link to it.</summary>
	public const string CreatedJson = """{"id":42,"links":[{"href":"https://console.test:3780/api/3/sites/7/x/42","rel":"self"}]}""";

	/// <summary>The console's answer for a site that does not exist.</summary>
	public const string NotFoundJson = """{"status":"NOT_FOUND","message":"The resource with identifier 7 was not found.","links":[]}""";

	public const string NotFoundMessage = "The resource with identifier 7 was not found.";

	/// <summary>Asserts that <paramref name="call"/> raises a not-found <see cref="Rapid7ApiException"/>.</summary>
	public static Task ShouldRaiseNotFoundAsync(Func<Rapid7Client, CancellationToken, Task> call)
		=> TestClient.ShouldFailAsync(call, HttpStatusCode.NotFound, NotFoundJson, NotFoundMessage);

	/// <summary>Asserts that a create answer was read: identifier 42 and its self link.</summary>
	public static void ShouldBeCreated42(this CreatedReference<int> created)
	{
		created.Id.Should().Be(42);
		created.Links.Should().ContainSingle().Which.Href.Should().Be("https://console.test:3780/api/3/sites/7/x/42");
	}

	/// <summary>Asserts that a links-only answer was read.</summary>
	public static void ShouldBeSiteLinks(this LinksResource links)
		=> links.Links.Should().ContainSingle().Which.Rel.Should().Be("self");
}
