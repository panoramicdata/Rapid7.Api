using Microsoft.Extensions.Logging;
using Rapid7.Api.Models;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Core;

/// <summary>What the clients log through the full pipeline: methods and paths, never credentials or query strings.</summary>
public class LoggingTests
{
	private static readonly PageOptions SecretQuery = new() { Page = 1, Sort = ["secret-sort"] };

	[Fact]
	public async Task ConsoleClient_LogsMethodAndPath_ButNoCredentialsOrQuery()
	{
		var logger = new CapturingLogger();
		var stub = new StubHandler();
		stub.Enqueue(HttpStatusCode.ServiceUnavailable);
		stub.Enqueue(HttpStatusCode.OK, """{"resources":[],"links":[]}""");
		using var client = TestClient.Create(stub, o => (o.Logger, o.MaxRetries, o.RetryBaseDelay, o.TwoFactorToken, o.Password) = (logger, 1, TimeSpan.Zero, "987654", "s3cret-pw"));

		await client.For<IProbe>().ListAsync(SecretQuery, TestContext.Current.CancellationToken);

		logger.Entries.Should().Equal(
			(LogLevel.Debug, "Rapid7 GET https://console.test:3780/api/3/probe (attempt 1)"),
			(LogLevel.Warning, "Rapid7 returned 503 for GET https://console.test:3780/api/3/probe; retrying in 00:00:00"),
			(LogLevel.Debug, "Rapid7 GET https://console.test:3780/api/3/probe (attempt 2)"));
		ShouldNotLeak(logger, "s3cret-pw", "987654", Convert.ToBase64String("nxadmin:s3cret-pw"u8.ToArray()));
	}

	[Fact]
	public async Task PlatformClient_LogsNoApiKeyOrQuery()
	{
		var logger = new CapturingLogger();
		var stub = TestClient.Stub("""{"resources":[],"links":[]}""");
		using var client = TestClient.CreateCloud(stub, o => (o.Logger, o.ApiKey) = (logger, "platform-key-123"));

		await client.For<IProbe>().ListAsync(SecretQuery, TestContext.Current.CancellationToken);

		logger.Messages.Should().Equal("Rapid7 GET https://us.api.insight.test/vm/api/3/probe (attempt 1)");
		ShouldNotLeak(logger, "platform-key-123");
	}

	private static void ShouldNotLeak(CapturingLogger logger, params string[] secrets)
		=> logger.Messages.Should().NotContain(m => m.Contains('?') || m.Contains("secret-sort") || secrets.Any(m.Contains));
}
