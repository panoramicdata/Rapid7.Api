namespace Rapid7.Api.IntegrationTest;

[Collection(Rapid7TestGroup.Name)]
public class RootIntegrationTests(Rapid7Fixture fixture)
{
	[Fact]
	public async Task GetAsync_ListsTheConsolesResources()
	{
		var links = await fixture.Client.Root.GetAsync(TestContext.Current.CancellationToken);

		links.Items.Should().Contain(l => l.Rel == "self");
		links.Items.Should().Contain(l => l.Href != null && l.Href.EndsWith("/api/3/sites", StringComparison.Ordinal));
	}

	[Fact]
	public async Task WrongPassword_RaisesUnauthorized()
	{
		var options = fixture.CreateOptions();
		options.Password = "definitely-not-the-password";
		using var client = new Rapid7Client(options);

		var act = () => client.Root.GetAsync(TestContext.Current.CancellationToken);

		(await act.Should().ThrowAsync<Rapid7ApiException>()).Which.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
	}
}
