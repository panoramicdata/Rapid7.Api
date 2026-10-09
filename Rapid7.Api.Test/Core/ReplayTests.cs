using Rapid7.Api.Test.Support;
using System.Net;
using System.Net.Sockets;

namespace Rapid7.Api.Test.Core;

/// <summary>
/// JSON request bodies built by Refit must be replayable through the real client, or a POST or PUT is never retried after a
/// 429/503 or a connection that could not be established.
/// </summary>
public class ReplayTests
{
	private const string ExpectedBody = """{"name":"a b","enabled":true}""";

	private static Task<Models.CreatedReference<int>> PostAsync(Rapid7Client client)
		=> client.For<IProbe>().CreateAsync("x", new Probe { Name = "a b", Enabled = true }, TestContext.Current.CancellationToken);

	private static Rapid7Client CreateRetrying(HttpMessageHandler inner)
		=> new(TestClient.ConsoleOptions(o => (o.MaxRetries, o.RetryBaseDelay) = (1, TimeSpan.Zero)), inner);

	[Fact]
	public async Task JsonPost_IsRetriedOn503_WithAnIdenticalBody()
	{
		var stub = new StubHandler();
		stub.Enqueue(HttpStatusCode.ServiceUnavailable);
		stub.Enqueue(HttpStatusCode.Created, """{"id":1,"links":[]}""");
		using var client = CreateRetrying(stub);

		await PostAsync(client);

		stub.Calls.Should().HaveCount(2);
		stub.Calls.Select(c => c.Body).Should().AllBe(ExpectedBody);
		stub.Calls.Select(c => c.ContentType).Should().AllBe("application/json");
	}

	[Fact]
	public async Task JsonPost_IsRetriedAfterAConnectionFailure_WithAnIdenticalBody()
	{
		var failedOnce = false;
		var bodies = new List<string>();
		var inner = new DelegateHandler(async (request, ct) =>
		{
			bodies.Add(await request.Content!.ReadAsStringAsync(ct));
			if (!failedOnce)
			{
				failedOnce = true;
				throw new HttpRequestException(HttpRequestError.ConnectionError, "refused", new SocketException((int)SocketError.ConnectionRefused));
			}

			return new HttpResponseMessage(HttpStatusCode.Created) { Content = new StringContent("""{"id":1,"links":[]}""") };
		});
		using var client = CreateRetrying(inner);

		await PostAsync(client);

		bodies.Should().Equal(ExpectedBody, ExpectedBody);
	}
}
