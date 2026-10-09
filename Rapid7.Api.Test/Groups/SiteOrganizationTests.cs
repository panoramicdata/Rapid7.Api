using Rapid7.Api.Models.Sites;
using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Groups;

public class SiteOrganizationTests
{
	private const string OrganizationJson = """
		{
			"name": "Example Ltd",
			"url": "https://www.example.test",
			"contact": "Jo Bloggs",
			"jobTitle": "Security manager",
			"email": "jo@example.test",
			"phone": "+44 20 7946 0000",
			"address": "1 High Street",
			"city": "London",
			"state": "Greater London",
			"zipCode": "N1 1AA",
			"country": "United Kingdom",
			"links": [ { "href": "https://console.test:3780/api/3/sites/7/organization", "rel": "self" } ]
		}
		""";

	[Fact]
	public async Task GetAsync_SendsGetToTheOrganization()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteOrganization.GetAsync(SiteFixtures.SiteId, ct), OrganizationJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/organization");
	}

	[Fact]
	public async Task GetAsync_MapsEveryField()
	{
		var organization = await TestClient.ReadAsync((c, ct) => c.SiteOrganization.GetAsync(SiteFixtures.SiteId, ct), OrganizationJson);

		organization.Name.Should().Be("Example Ltd");
		organization.Url.Should().Be("https://www.example.test");
		organization.Contact.Should().Be("Jo Bloggs");
		organization.JobTitle.Should().Be("Security manager");
		organization.Email.Should().Be("jo@example.test");
		organization.Phone.Should().Be("+44 20 7946 0000");
		organization.Address.Should().Be("1 High Street");
		organization.City.Should().Be("London");
		organization.State.Should().Be("Greater London");
		organization.ZipCode.Should().Be("N1 1AA");
		organization.Country.Should().Be("United Kingdom");
		organization.Items.Should().ContainSingle();
	}

	[Fact]
	public async Task UpdateAsync_PutsTheDetails()
	{
		var organization = new SiteOrganization { Name = "Example Ltd", Email = "jo@example.test", ZipCode = "N1 1AA" };

		var call = await TestClient.CaptureAsync((c, ct) => c.SiteOrganization.UpdateAsync(SiteFixtures.SiteId, organization, ct), SiteFixtures.LinksJson);

		call.ShouldBe(HttpMethod.Put, "/api/3/sites/7/organization", body: """{"name":"Example Ltd","email":"jo@example.test","zipCode":"N1 1AA","links":[]}""");
	}

	[Fact]
	public Task GetAsync_NotFound_RaisesRapid7ApiException()
		=> SiteFixtures.ShouldRaiseNotFoundAsync((c, ct) => c.SiteOrganization.GetAsync(SiteFixtures.SiteId, ct));
}
