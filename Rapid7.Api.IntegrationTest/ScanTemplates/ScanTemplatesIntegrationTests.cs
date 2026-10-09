namespace Rapid7.Api.IntegrationTest.ScanTemplates;

/// <summary>Reads scan templates, and round-trips a copy of one that the test creates and deletes.</summary>
[Collection(Rapid7TestGroup.Name)]
public class ScanTemplatesIntegrationTests(Rapid7Fixture fixture)
{
	private const string BuiltInTemplate = "full-audit-without-web-spider";

	private static CancellationToken Ct => TestContext.Current.CancellationToken;

	private Rapid7Client Client => fixture.Client;

	[Fact]
	public async Task ListAsync_IncludesTheBuiltInTemplates()
	{
		var templates = await Client.ScanTemplates.ListAsync(Ct);

		templates.Resources.Should().Contain(t => t.Id == BuiltInTemplate);
		templates.Resources.Should().OnlyContain(t => !string.IsNullOrEmpty(t.Name));
	}

	[Fact]
	public async Task GetAsync_ReadsTheWholeTemplate()
	{
		var template = await Client.ScanTemplates.GetAsync(BuiltInTemplate, Ct);

		template.Id.Should().Be(BuiltInTemplate);
		template.Discovery.Should().NotBeNull();
		template.Checks.Should().NotBeNull();
	}

	[Fact]
	public async Task CopiedTemplate_RoundTrip()
	{
		var original = await Client.ScanTemplates.GetAsync(BuiltInTemplate, Ct);
		var name = Rapid7Fixture.UniqueName("template");
		var created = await Client.ScanTemplates.CreateAsync(original with { Id = null, Links = null, Name = name }, Ct);
		var templateId = created.Id!;
		try
		{
			var copy = await Client.ScanTemplates.GetAsync(templateId, Ct);
			copy.Name.Should().Be(name);
			copy.MaxParallelAssets.Should().Be(original.MaxParallelAssets);
			copy.Discovery!.Service!.Tcp!.Ports.Should().Be(original.Discovery!.Service!.Tcp!.Ports);

			await Client.ScanTemplates.UpdateAsync(templateId, copy with { Links = null, Description = "Updated by Rapid7.Api integration tests" }, Ct);

			(await Client.ScanTemplates.GetAsync(templateId, Ct)).Description.Should().Be("Updated by Rapid7.Api integration tests");
		}
		finally
		{
			await Client.ScanTemplates.DeleteAsync(templateId, CancellationToken.None);
		}
	}
}
