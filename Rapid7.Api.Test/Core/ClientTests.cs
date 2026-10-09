using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Core;

/// <summary>Construction, base addresses, endpoint groups and disposal of the three clients.</summary>
public class ClientTests
{
	/// <summary>A transport that records whether it was disposed.</summary>
	private sealed class TrackingHandler : HttpMessageHandler
	{
		public bool Disposed { get; private set; }

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
			=> Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("""{"links":[]}""") });

		protected override void Dispose(bool disposing)
		{
			Disposed = true;
			base.Dispose(disposing);
		}
	}

	[Theory]
	[InlineData("https://console.test:3780", "https://console.test:3780/")]
	[InlineData("https://console.test:3780/", "https://console.test:3780/")]
	[InlineData("https://proxy.test/rapid7", "https://proxy.test/rapid7/")]
	[InlineData("https://proxy.test/rapid7/", "https://proxy.test/rapid7/")]
	[InlineData(" https://console.test:3780 ", "https://console.test:3780/")]
	[InlineData("http://console.test", "http://console.test/")]
	public void Console_BaseAddress_AlwaysEndsInASlash_KeepingAPathPrefix(string baseUrl, string expected)
	{
		using var client = TestClient.Create(new StubHandler(), o => o.BaseUrl = baseUrl);

		client.BaseAddress.Should().Be(new Uri(expected));
	}

	[Fact]
	public async Task Console_EndpointPaths_AreAppendedToAPathPrefix()
	{
		var call = await TestClient.CaptureAsync(
			stub => TestClient.Create(stub, o => o.BaseUrl = "https://proxy.test/rapid7"),
			(c, ct) => c.Root.GetAsync(ct),
			"""{"links":[]}""");

		call.Uri.ToString().Should().Be("https://proxy.test/rapid7/api/3");
	}

	[Theory]
	[InlineData("eu", null, "https://eu.api.insight.rapid7.com/vm/", "https://eu.api.insight.rapid7.com/")]
	[InlineData("us2", "", "https://us2.api.insight.rapid7.com/vm/", "https://us2.api.insight.rapid7.com/")]
	[InlineData("", "https://proxy.test", "https://proxy.test/vm/", "https://proxy.test/")]
	[InlineData("eu", "https://proxy.test/r7/", "https://proxy.test/r7/vm/", "https://proxy.test/r7/")]
	[InlineData("", " https://proxy.test/r7 ", "https://proxy.test/r7/vm/", "https://proxy.test/r7/")]
	public void Platform_BaseAddresses_FollowTheRegion_OrTheBaseUrlOverride(string region, string? baseUrl, string cloud, string export)
	{
		var options = TestClient.PlatformOptions(o => (o.Region, o.BaseUrl) = (region, baseUrl));
		using var cloudClient = new Rapid7CloudClient(options, new StubHandler());
		using var exportClient = new Rapid7BulkExportClient(options, new StubHandler());

		cloudClient.BaseAddress.Should().Be(new Uri(cloud));
		exportClient.BaseAddress.Should().Be(new Uri(export));
	}

	[Fact]
	public void PublicConstructors_CreateTheirOwnTransport_WithoutTouchingTheNetwork()
	{
		var thumbprint = new string('a', 64);
		using var console = new Rapid7Client(TestClient.ConsoleOptions(o => o.TrustedServerCertificateThumbprint = thumbprint));
		using var cloud = new Rapid7CloudClient(new Rapid7PlatformOptions { Region = "eu", ApiKey = "k" });
		using var export = new Rapid7BulkExportClient(new Rapid7PlatformOptions { Region = "ap", ApiKey = "k" });

		console.BaseAddress.Should().Be(new Uri(TestClient.ConsoleUrl));
		cloud.BaseAddress.Should().Be(new Uri("https://eu.api.insight.rapid7.com/vm/"));
		export.BaseAddress.Should().Be(new Uri("https://ap.api.insight.rapid7.com/"));
	}

	[Fact]
	public void EndpointGroups_AreCreatedOnce()
	{
		using var client = TestClient.Create(new StubHandler());

		client.Root.Should().BeSameAs(client.Root);
	}

	[Fact]
	public void Dispose_DisposesTheInnerHandler_AndIsIdempotent()
	{
		var console = new TrackingHandler();
		var cloud = new TrackingHandler();
		var export = new TrackingHandler();
		var clients = new IDisposable[]
		{
			new Rapid7Client(TestClient.ConsoleOptions(), console),
			new Rapid7CloudClient(TestClient.PlatformOptions(), cloud),
			new Rapid7BulkExportClient(TestClient.PlatformOptions(), export)
		};

		foreach (var client in clients)
		{
			client.Dispose();
			client.Dispose();
		}

		new[] { console, cloud, export }.Should().OnlyContain(h => h.Disposed);
	}

	[Fact]
	public async Task ADisposedClient_RefusesRequests()
	{
		var client = new Rapid7Client(TestClient.ConsoleOptions(), new TrackingHandler());
		var root = client.Root;
		client.Dispose();

		var act = () => root.GetAsync(TestContext.Current.CancellationToken);

		await act.Should().ThrowAsync<ObjectDisposedException>();
	}

	[Fact]
	public async Task OptionsChangedAfterConstruction_DoNotAffectTheClient()
	{
		var stub = TestClient.Stub("""{"links":[]}""");
		var options = TestClient.ConsoleOptions();
		using var client = new Rapid7Client(options, stub);
		options.Username = "changed";
		options.Password = "changed";
		options.ReadOnly = true;
		options.BaseUrl = "https://elsewhere.test/";

		await client.Root.GetAsync(TestContext.Current.CancellationToken);

		stub.Calls[0].Uri.Host.Should().Be("console.test");
		stub.Calls[0].Authorization.Should().Be("Basic " + Convert.ToBase64String("nxadmin:fake-password"u8.ToArray()));
	}

	[Fact]
	public async Task ConcurrentRequests_ShareOneClientSafely()
	{
		var transport = new DelegateHandler((_, _) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("""{"links":[]}""") }));
		using var client = new Rapid7Client(TestClient.ConsoleOptions(), transport);

		await Task.WhenAll(Enumerable.Range(0, 50).Select(_ => Task.Run(() => client.Root.GetAsync(TestContext.Current.CancellationToken), TestContext.Current.CancellationToken)));

		transport.Requests.Should().HaveCount(50);
		var expected = "Basic " + Convert.ToBase64String("nxadmin:fake-password"u8.ToArray());
		transport.Authorizations.Should().OnlyContain(a => a == expected);
	}
}
