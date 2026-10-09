using System.Net;
using Rapid7.Api.Models;
using Rapid7.Api.Test.Support;
using static Rapid7.Api.Test.Groups.AssetSamples;

namespace Rapid7.Api.Test.Groups;

public class AssetCatalogTests
{
	private static readonly PageOptions Paging = new() { Page = 0, Size = 500 };

	[Fact]
	public Task ListOperatingSystemsAsync_SendsGetWithPaging()
		=> SendsAsync((c, ct) => c.AssetCatalog.ListOperatingSystemsAsync(Paging, ct), HttpMethod.Get, "/api/3/operating_systems", "?page=0&size=500", response: EmptyPage);

	[Fact]
	public Task GetOperatingSystemAsync_SendsGet()
		=> SendsAsync((c, ct) => c.AssetCatalog.GetOperatingSystemAsync(35, ct), HttpMethod.Get, "/api/3/operating_systems/35", response: OperatingSystemJson);

	[Fact]
	public Task ListSoftwareAsync_SendsGetWithPaging()
		=> SendsAsync((c, ct) => c.AssetCatalog.ListSoftwareAsync(Paging, ct), HttpMethod.Get, "/api/3/software", "?page=0&size=500", response: EmptyPage);

	[Fact]
	public Task GetSoftwareAsync_SendsGet()
		=> SendsAsync((c, ct) => c.AssetCatalog.GetSoftwareAsync(3, ct), HttpMethod.Get, "/api/3/software/3", response: SoftwareJson);

	[Fact]
	public async Task ListOperatingSystemsAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.AssetCatalog.ListOperatingSystemsAsync(null, ct), Page(OperatingSystemJson));

		var os = page.Resources.Should().ContainSingle().Subject;
		ShouldRoundTrip(os, OperatingSystemJson);
		os.Architecture.Should().Be("x86");
		os.Cpe!.V23.Should().StartWith("cpe:2.3:o:microsoft");
	}

	[Fact]
	public async Task GetOperatingSystemAsync_MapsEveryField()
	{
		var os = await TestClient.ReadAsync((c, ct) => c.AssetCatalog.GetOperatingSystemAsync(35, ct), OperatingSystemJson);

		ShouldRoundTrip(os, OperatingSystemJson);
		os.Id.Should().Be(35);
	}

	[Fact]
	public async Task ListSoftwareAsync_MapsEveryField()
	{
		var page = await TestClient.ReadAsync((c, ct) => c.AssetCatalog.ListSoftwareAsync(null, ct), Page(SoftwareJson));

		ShouldRoundTrip(page.Resources.Should().ContainSingle().Subject, SoftwareJson);
	}

	[Fact]
	public async Task GetSoftwareAsync_MapsEveryField()
	{
		var software = await TestClient.ReadAsync((c, ct) => c.AssetCatalog.GetSoftwareAsync(3, ct), SoftwareJson);

		ShouldRoundTrip(software, SoftwareJson);
		software.Product.Should().Be("Outlook 2013");
	}

	[Fact]
	public Task GetSoftwareAsync_NotFound_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync((c, ct) => c.AssetCatalog.GetSoftwareAsync(999, ct), HttpStatusCode.NotFound, NotFound, NotFoundMessage);
}
