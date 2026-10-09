using Rapid7.Api.Models.Assets;
using Rapid7.Api.Models.Policies;

namespace Rapid7.Api.Test.Groups;

/// <summary>Asserts that models read from the <see cref="PolicyJson"/> responses carry every field.</summary>
internal static class PolicyAssertions
{
	public static void ShouldBeExamplePolicy(this Policy policy) => policy.Should().BeEquivalentTo(new
	{
		BenchmarkName = "CIS Example Server Benchmark",
		BenchmarkVersion = "1.4.0",
		Category = "CIS",
		Description = "Level 1 settings for servers.",
		FailedAssetsCount = 3,
		FailedRulesCount = 12,
		Id = "xccdf_org.example_profile_Level_1",
		IsCustom = false,
		Items = new[] { new { Href = "https://console.test:3780/api/3/policies/84", Rel = "self" } },
		NotApplicableAssetsCount = 1,
		NotApplicableRulesCount = 4,
		PassedAssetsCount = 7,
		PassedRulesCount = 250,
		PolicyName = "Level 1 - Member Server",
		RuleCompliance = 0.95,
		RuleComplianceDelta = -0.02,
		Scope = "Built-in",
		Status = PolicyComplianceStatus.Fail,
		SurrogateId = 84L,
		Title = "Example Server Level 1",
		UnscoredRules = 2
	});

	public static void ShouldBeExampleItem(this PolicyItem item) => item.Should().BeEquivalentTo(new
	{
		Assets = new { Total = 10, TotalFailed = 3, TotalNotApplicable = 1, TotalPassed = 6, Items = Array.Empty<object>() },
		Description = "Password settings.",
		HasOverride = true,
		Id = 71L,
		IsUnscored = false,
		Items = new[] { new { Rel = "self" } },
		Name = "xccdf_org.example_group_1.1",
		Policy = ExamplePolicyMetadata,
		Rules = new { Total = 5, TotalFailed = 1, TotalNotApplicable = 0, TotalPassed = 4, Unscored = 1 },
		Scope = "Built-in",
		Status = PolicyComplianceStatus.Pass,
		Title = "Password Policy",
		Type = PolicyItemType.Group
	});

	public static void ShouldBeExampleRule(this PolicyRule rule) => rule.Should().BeEquivalentTo(new
	{
		Assets = new { Total = 10, TotalFailed = 2, TotalNotApplicable = 3, TotalPassed = 5 },
		Benchmark = ExampleBenchmark,
		Description = "Keeps a history of passwords.",
		Id = "xccdf_org.example_rule_1.1.1",
		IsCustom = true,
		Items = new[] { new { Href = "https://console.test:3780/api/3/policies/84/rules/53" } },
		Name = "xccdf_org.example_rule_1.1.1",
		Role = PolicyRuleRole.Unscored,
		Scope = "Custom",
		Status = PolicyComplianceStatus.NotApplicable,
		SurrogateId = 53L,
		Title = "Enforce password history"
	});

	public static void ShouldBeExampleGroup(this PolicyGroup group) => group.Should().BeEquivalentTo(new
	{
		Assets = new { Total = 4, TotalFailed = 1, TotalNotApplicable = 0, TotalPassed = 3 },
		Benchmark = ExampleBenchmark,
		Description = "Account policies.",
		Id = "xccdf_org.example_group_1",
		Items = new[] { new { Href = "https://console.test:3780/api/3/policies/84/groups/71" } },
		Name = "xccdf_org.example_group_1",
		Policy = ExamplePolicyMetadata,
		Scope = "Built-in",
		Status = PolicyComplianceStatus.Fail,
		SurrogateId = 71L,
		Title = "Account Policies"
	});

	public static void ShouldBeExampleAsset(this PolicyAsset asset) => asset.Should().BeEquivalentTo(new
	{
		Hostname = "server01.example.test",
		Id = 282L,
		Ip = "192.0.2.10",
		Items = new[] { new { Rel = "Asset" } },
		Status = PolicyAssetStatus.Failed,
		Os = new
		{
			Architecture = "x86_64",
			Configurations = new[] { new { Name = "kernel", Value = (string?)"5.15" }, new { Name = "selinux", Value = (string?)null } },
			Cpe = new
			{
				Edition = "enterprise",
				Language = "en",
				Other = "other-info",
				Part = CpePart.OperatingSystem,
				Product = "example_server",
				SoftwareEdition = "server",
				TargetHardware = "x64",
				TargetSoftware = "none",
				Update = "sp1",
				V22 = "cpe:/o:example:example_server:2:sp1:enterprise",
				V23 = "cpe:2.3:o:example:example_server:2:sp1:enterprise:*:*:*:*:*",
				Vendor = "example",
				Version = "2"
			},
			Description = "Example Server 2 SP1",
			Family = "Linux",
			Id = 35L,
			Product = "Example Server",
			SystemName = "Example Linux",
			Type = "General",
			Vendor = "Example",
			Version = "2"
		}
	});

	private static readonly object ExamplePolicyMetadata = new
	{
		Name = "xccdf_org.example_profile_Level_1",
		Title = "Example Server Level 1",
		Version = "1.4.0",
		Items = Array.Empty<object>()
	};

	private static readonly object ExampleBenchmark = new
	{
		Name = "xccdf_org.example_benchmark",
		Title = "CIS Example Server Benchmark",
		Version = "1.4.0",
		Items = Array.Empty<object>()
	};
}
