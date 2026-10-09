using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Groups;

/// <summary>Response shapes shared by the user, credential and administration tests (hosts replaced).</summary>
internal static class AccessJson
{
	/// <summary>A links-only answer, as to most updates and deletes.</summary>
	public const string LinksOnly = """{"links":[{"href":"https://console.test:3780/api/3/users/9","rel":"self"}]}""";

	/// <summary>The answer to a create.</summary>
	public const string Created = """{"id":9,"links":[{"href":"https://console.test:3780/api/3/users/9","rel":"self"}]}""";

	/// <summary>A list of identifiers, as the user, site and asset group reference endpoints answer.</summary>
	public const string Ids = """{"resources":[9,12,37],"links":[{"href":"https://console.test:3780/api/3/roles/user/users","rel":"self"}]}""";

	/// <summary>A console error body.</summary>
	public static string Error(string status, string message) => $$"""{"status":"{{status}}","message":"{{message}}","links":[]}""";

	/// <summary>Asserts that <paramref name="links"/> is the one self link of <see cref="LinksOnly"/> or <see cref="Created"/>.</summary>
	public static void ShouldBeTheSelfLink(Models.LinksResource links)
		=> links.Links.Should().ContainSingle().Which.Href.Should().Be("https://console.test:3780/api/3/users/9");

	/// <summary>Asserts that <paramref name="ids"/> is the list in <see cref="Ids"/>.</summary>
	public static void ShouldBeTheIds(Models.ResourceList<int> ids)
	{
		ids.Resources.Should().Equal(9, 12, 37);
		ids.Links.ShouldBeSelfOnly();
	}
}
