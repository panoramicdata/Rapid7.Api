using System.Net;

namespace Rapid7.Api.Test.Support;

/// <summary>Builds clients over a <see cref="StubHandler"/>, and the capture, read and failure helpers every test uses.</summary>
internal static class TestClient
{
	public const string ConsoleUrl = "https://console.test:3780/";

	public const string PlatformUrl = "https://us.api.insight.test/";

	/// <summary>Console options pointing at <see cref="ConsoleUrl"/>, with retries off.</summary>
	public static Rapid7ClientOptions ConsoleOptions(Action<Rapid7ClientOptions>? tweak = null)
		=> Tweaked(new Rapid7ClientOptions { BaseUrl = ConsoleUrl, Username = "nxadmin", Password = "fake-password", MaxRetries = 0 }, tweak);

	/// <summary>Platform options pointing at <see cref="PlatformUrl"/>, with retries off.</summary>
	public static Rapid7PlatformOptions PlatformOptions(Action<Rapid7PlatformOptions>? tweak = null)
		=> Tweaked(new Rapid7PlatformOptions { BaseUrl = PlatformUrl, ApiKey = "fake-api-key", MaxRetries = 0 }, tweak);

	/// <summary>A Security Console (v3) client over <paramref name="stub"/>.</summary>
	public static Rapid7Client Create(StubHandler stub) => Create(stub, null);

	/// <summary>A Security Console (v3) client over <paramref name="stub"/>, with options changed by <paramref name="tweak"/>.</summary>
	public static Rapid7Client Create(StubHandler stub, Action<Rapid7ClientOptions>? tweak) => new(ConsoleOptions(tweak), stub);

	/// <summary>A Cloud Integrations (v4) client over <paramref name="stub"/>.</summary>
	public static Rapid7CloudClient CreateCloud(StubHandler stub) => CreateCloud(stub, null);

	/// <summary>A Cloud Integrations (v4) client over <paramref name="stub"/>, with options changed by <paramref name="tweak"/>.</summary>
	public static Rapid7CloudClient CreateCloud(StubHandler stub, Action<Rapid7PlatformOptions>? tweak) => new(PlatformOptions(tweak), stub);

	/// <summary>A Bulk Export client over <paramref name="stub"/>.</summary>
	public static Rapid7BulkExportClient CreateBulkExport(StubHandler stub) => CreateBulkExport(stub, null);

	/// <summary>A Bulk Export client over <paramref name="stub"/>, with options changed by <paramref name="tweak"/>.</summary>
	public static Rapid7BulkExportClient CreateBulkExport(StubHandler stub, Action<Rapid7PlatformOptions>? tweak)
		=> new(PlatformOptions(tweak), stub);

	/// <summary>A stub that answers one request with <paramref name="json"/>.</summary>
	public static StubHandler Stub(string json, HttpStatusCode status = HttpStatusCode.OK)
	{
		var stub = new StubHandler();
		stub.Enqueue(status, json);
		return stub;
	}

	/// <summary>
	/// Sends <paramref name="call"/> through a console client whose transport answers <paramref name="response"/>, and
	/// returns the one request it made.
	/// </summary>
	public static Task<RecordedCall> CaptureAsync(Func<Rapid7Client, CancellationToken, Task> call, string response)
		=> CaptureAsync(Create, call, response);

	/// <summary>Captures the one request <paramref name="call"/> makes through a client built by <paramref name="create"/>.</summary>
	public static async Task<RecordedCall> CaptureAsync<TClient>(
		Func<StubHandler, TClient> create,
		Func<TClient, CancellationToken, Task> call,
		string response)
		where TClient : IDisposable
	{
		var stub = Stub(response);
		using var client = create(stub);
		await call(client, TestContext.Current.CancellationToken);
		return stub.Calls.Should().ContainSingle().Subject;
	}

	/// <summary>Sends <paramref name="call"/> through a console client answering <paramref name="response"/>, and returns what it read.</summary>
	public static Task<T> ReadAsync<T>(Func<Rapid7Client, CancellationToken, Task<T>> call, string response)
		=> ReadAsync(Create, call, response);

	/// <summary>Returns what <paramref name="call"/> reads through a client built by <paramref name="create"/>.</summary>
	public static async Task<T> ReadAsync<TClient, T>(
		Func<StubHandler, TClient> create,
		Func<TClient, CancellationToken, Task<T>> call,
		string response)
		where TClient : IDisposable
	{
		using var client = create(Stub(response));
		return await call(client, TestContext.Current.CancellationToken);
	}

	/// <summary>Asserts that <paramref name="call"/> on a console client raises <see cref="Rapid7ApiException"/> for the given answer.</summary>
	public static Task ShouldFailAsync(Func<Rapid7Client, CancellationToken, Task> call, HttpStatusCode status, string response, string message)
		=> ShouldFailAsync(Create, call, status, response, message);

	/// <summary>Asserts that <paramref name="call"/> raises <see cref="Rapid7ApiException"/> through a client built by <paramref name="create"/>.</summary>
	public static async Task ShouldFailAsync<TClient>(
		Func<StubHandler, TClient> create,
		Func<TClient, CancellationToken, Task> call,
		HttpStatusCode status,
		string response,
		string message)
		where TClient : IDisposable
	{
		using var client = create(Stub(response, status));
		await ShouldFailWithAsync(() => call(client, TestContext.Current.CancellationToken), status, message);
	}

	/// <summary>Asserts that <paramref name="act"/> raises <see cref="Rapid7ApiException"/> with the status and message.</summary>
	public static async Task ShouldFailWithAsync(Func<Task> act, HttpStatusCode status, string message)
	{
		var thrown = await act.Should().ThrowAsync<Rapid7ApiException>();
		thrown.Which.StatusCode.Should().Be(status);
		thrown.Which.Message.Should().Be(message);
	}

	private static T Tweaked<T>(T options, Action<T>? tweak)
	{
		tweak?.Invoke(options);
		return options;
	}
}
