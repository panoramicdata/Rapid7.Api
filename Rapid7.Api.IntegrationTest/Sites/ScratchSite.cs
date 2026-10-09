using Rapid7.Api.Models.Sites;

namespace Rapid7.Api.IntegrationTest.Sites;

/// <summary>
/// Runs a test against a new, never-scanned static site that the test owns, deleting the site afterwards. Its only target
/// is an address in the TEST-NET-1 documentation range, so nothing real is ever in scope.
/// </summary>
internal static class ScratchSite
{
	public const string TargetAddress = "192.0.2.10";

	/// <summary>Creates a scratch site, runs <paramref name="test"/> with its identifier, and deletes the site in all cases.</summary>
	public static async Task RunAsync(Rapid7Fixture fixture, Func<int, CancellationToken, Task> test)
	{
		var ct = TestContext.Current.CancellationToken;
		var created = await fixture.Client.Sites.CreateAsync(
			new SiteCreateRequest
			{
				Name = Rapid7Fixture.UniqueName("site"),
				Description = "Created by the Rapid7.Api integration tests; safe to delete.",
				Importance = SiteImportance.VeryLow,
				Scan = new ScanScope
				{
					Assets = new ScanScopeAssets { IncludedTargets = new ScanScopeTargets { Addresses = [TargetAddress] } }
				}
			},
			ct);
		var siteId = created.Id;
		try
		{
			await test(siteId, ct);
		}
		finally
		{
			await fixture.Client.Sites.DeleteAsync(siteId, ct);
		}
	}
}
