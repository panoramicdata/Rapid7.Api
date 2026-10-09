using Rapid7.Api.Handlers;
using Rapid7.Api.Test.Support;
using Refit;
using System.Net;
using System.Text.RegularExpressions;

namespace Rapid7.Api.Test.Core;

/// <summary>The read-only guard of each client: what it lets through, what it refuses, and that a refusal sends nothing.</summary>
public partial class ReadOnlyHandlerTests
{
	/// <summary>Any verb to any path below a client's base address.</summary>
	public interface IRaw
	{
		[Get("{**path}")]
		Task<HttpResponseMessage> GetAsync(string path, CancellationToken cancellationToken);

		[Head("{**path}")]
		Task<HttpResponseMessage> HeadAsync(string path, CancellationToken cancellationToken);

		[Post("{**path}")]
		Task<HttpResponseMessage> PostAsync(string path, CancellationToken cancellationToken);

		[Put("{**path}")]
		Task<HttpResponseMessage> PutAsync(string path, CancellationToken cancellationToken);

		[Delete("{**path}")]
		Task<HttpResponseMessage> DeleteAsync(string path, CancellationToken cancellationToken);

		[Patch("{**path}")]
		Task<HttpResponseMessage> PatchAsync(string path, CancellationToken cancellationToken);
	}

	private static Task<HttpResponseMessage> SendAsync(IRaw raw, string method, string path)
	{
		var ct = TestContext.Current.CancellationToken;
		return method switch
		{
			"GET" => raw.GetAsync(path, ct),
			"HEAD" => raw.HeadAsync(path, ct),
			"POST" => raw.PostAsync(path, ct),
			"PUT" => raw.PutAsync(path, ct),
			"DELETE" => raw.DeleteAsync(path, ct),
			_ => raw.PatchAsync(path, ct)
		};
	}

	/// <summary>A read-only client of the given kind over a stub answering 200, and its raw endpoint group.</summary>
	private static (IDisposable Client, IRaw Raw, StubHandler Stub) Create(string kind, string? baseUrl = null, bool readOnly = true)
	{
		var stub = new StubHandler();
		stub.Enqueue(HttpStatusCode.OK);
		Rapid7ClientBase client = kind switch
		{
			"console" => TestClient.Create(stub, o => (o.ReadOnly, o.BaseUrl) = (readOnly, baseUrl ?? o.BaseUrl)),
			"cloud" => TestClient.CreateCloud(stub, Platform),
			_ => TestClient.CreateBulkExport(stub, Platform)
		};
		return (client, client.For<IRaw>(), stub);

		void Platform(Rapid7PlatformOptions options) => (options.ReadOnly, options.BaseUrl) = (readOnly, baseUrl ?? options.BaseUrl);
	}

	[Theory]
	[InlineData("console", "GET", "api/3/sites")]
	[InlineData("console", "HEAD", "api/3/sites")]
	[InlineData("console", "POST", "api/3/assets/search")]
	[InlineData("console", "POST", "api/3/sonar_queries/search")]
	[InlineData("console", "POST", "api/3/assets/search/")]
	[InlineData("cloud", "GET", "v4/integration/assets/1")]
	[InlineData("cloud", "POST", "v4/integration/assets")]
	[InlineData("cloud", "POST", "v4/integration/sites")]
	[InlineData("cloud", "POST", "v4/integration/vulnerabilities")]
	[InlineData("bulk", "POST", "export/graphql")]
	[InlineData("bulk", "GET", "export/graphql")]
	public async Task ReadingRequests_AreSent(string kind, string method, string path)
	{
		var (client, raw, stub) = Create(kind);
		using (client)
		{
			using var response = await SendAsync(raw, method, path);

			response.StatusCode.Should().Be(HttpStatusCode.OK);
			stub.Calls.Should().ContainSingle();
		}
	}

	[Theory]
	[InlineData("console", "PUT", "api/3/sites/1")]
	[InlineData("console", "DELETE", "api/3/sites/1")]
	[InlineData("console", "PATCH", "api/3/sites/1")]
	[InlineData("console", "POST", "api/3/sites")]
	[InlineData("console", "POST", "api/3/assets/search/extra")]
	[InlineData("console", "POST", "api/3/Assets/search")]
	[InlineData("console", "POST", "api/3/sites/1/scans")]
	[InlineData("cloud", "POST", "v4/integration/scan")]
	[InlineData("cloud", "POST", "v4/integration/assets/1")]
	[InlineData("cloud", "PUT", "v4/integration/scan/engine/1/configuration")]
	[InlineData("cloud", "DELETE", "v4/integration/scan/engine/1")]
	[InlineData("bulk", "POST", "export/graphql/other")]
	[InlineData("bulk", "PUT", "export/graphql")]
	public async Task ChangingRequests_AreRefused_BeforeAnythingIsSent(string kind, string method, string path)
	{
		var (client, raw, stub) = Create(kind);
		using (client)
		{
			var act = () => SendAsync(raw, method, path);

			var thrown = (await act.Should().ThrowAsync<Rapid7ReadOnlyException>()).Which;
			thrown.Method.Should().Be(method);
			thrown.Path.Should().EndWith("/" + path).And.NotContain("?");
			thrown.Message.Should().Be($"The Rapid7 client is read-only and refused {method} {thrown.Path}.");
			stub.Calls.Should().BeEmpty();
		}
	}

	[Theory]
	[InlineData("console", "https://proxy.test/rapid7", "api/3/assets/search")]
	[InlineData("cloud", "https://proxy.test/r7/", "v4/integration/sites")]
	[InlineData("bulk", "https://proxy.test/r7", "export/graphql")]
	public async Task AllowedPosts_AreMatchedBelowAPathPrefixedBaseAddress(string kind, string baseUrl, string path)
	{
		var (client, raw, stub) = Create(kind, baseUrl);
		using (client)
		{
			using var response = await SendAsync(raw, "POST", path);

			stub.Calls.Should().ContainSingle().Which.Uri.AbsolutePath.Should().StartWith("/r");
		}
	}

	[Theory]
	[InlineData("console")]
	[InlineData("cloud")]
	[InlineData("bulk")]
	public async Task WithoutReadOnly_ChangingRequestsAreSent(string kind)
	{
		var (client, raw, stub) = Create(kind, readOnly: false);
		using (client)
		{
			using var response = await SendAsync(raw, "DELETE", "anything/1");

			stub.Calls.Should().ContainSingle();
		}
	}

	[GeneratedRegex("^.*$")]
	private static partial Regex AnyPath();

	[Fact]
	public async Task APostOutsideTheBaseAddress_IsRefused()
	{
		using var harness = new HandlerHarness(new ReadOnlyHandler(new Uri("https://proxy.test/rapid7/"), AnyPath()));

		var act = () => harness.SendAsync(HttpMethod.Post, "https://proxy.test/other/api/3/assets/search?q=secret");

		(await act.Should().ThrowAsync<Rapid7ReadOnlyException>()).Which.Path.Should().Be("https://proxy.test/other/api/3/assets/search");
		harness.Stub.Calls.Should().BeEmpty();
	}

	[Theory]
	[InlineData("https://c.test/", "https://c.test/api/3/x", "api/3/x")]
	[InlineData("https://c.test/p/", "https://c.test/p/api/3/x/", "api/3/x")]
	[InlineData("https://c.test/p", "https://c.test/p/api/3/x?y=1", "api/3/x")]
	[InlineData("https://c.test/p/", "https://c.test/P/api/3/x", null)]
	[InlineData("https://c.test/p/", "https://c.test/pq/api/3/x", null)]
	public void RelativePath_IsThePathBelowTheBaseAddress(string baseUri, string requestUri, string? expected)
		=> ReadOnlyHandler.RelativePath(new Uri(baseUri), new Uri(requestUri)).Should().Be(expected);
}
