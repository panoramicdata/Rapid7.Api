namespace Rapid7.Api.Test.Core;

/// <summary>The Insight platform regions the v4 specification documents are accepted, and map to their hosts.</summary>
public class PlatformRegionTests
{
	[Theory]
	[InlineData("us")]
	[InlineData("us2")]
	[InlineData("us3")]
	[InlineData("eu")]
	[InlineData("ca")]
	[InlineData("au")]
	[InlineData("ap")]
	[InlineData("aps2")]
	[InlineData("me1")]
	public void EveryDocumentedRegion_IsAccepted(string region)
	{
		using var client = new Rapid7CloudClient(new Rapid7PlatformOptions { Region = region, ApiKey = "fake-api-key" });

		client.BaseAddress.Should().Be(new Uri($"https://{region}.api.insight.rapid7.com/vm/"));
	}
}
