using System.Text.RegularExpressions;
using Rapid7.Api.Handlers;
using Refit;

namespace Rapid7.Api;

/// <summary>
/// Client for the InsightVM Cloud Integrations API (v4, <c>https://&lt;region&gt;.api.insight.rapid7.com/vm/</c>), which
/// reads assets, sites, vulnerabilities and scan engines across your Insight platform organisation and starts and stops
/// scans. Each group of endpoints is a property; the groups are created on first use.
/// </summary>
/// <remarks>
/// Every request sends the <c>X-Api-Key</c> header, asks for JSON, and retries transient failures. Non-success responses
/// raise <see cref="Rapid7ApiException"/>. A client is thread-safe and intended to be long-lived; dispose it when done.
/// </remarks>
public sealed partial class Rapid7CloudClient : IDisposable
{
	private readonly HttpMessageHandler _pipeline;
	private readonly HttpClient _httpClient;

	/// <summary>Creates a client.</summary>
	/// <param name="options">Platform options: region (or base URL) and API key.</param>
	public Rapid7CloudClient(Rapid7PlatformOptions options) : this(options, Rapid7Transport.Create(Validated(options)))
	{
	}

	/// <summary>
	/// Creates a client that sends requests through <paramref name="innerHandler"/>, which it takes ownership of and
	/// disposes. The certificate settings in <paramref name="options"/> are ignored: configure them on the handler.
	/// </summary>
	/// <param name="options">Platform options: region (or base URL) and API key.</param>
	/// <param name="innerHandler">The handler that sends requests to the network.</param>
	public Rapid7CloudClient(Rapid7PlatformOptions options, HttpMessageHandler innerHandler)
	{
		ArgumentNullException.ThrowIfNull(innerHandler);
		Validated(options);
		BaseAddress = new Uri(options.PlatformAddress, "vm/");
		_pipeline = Rapid7Pipeline.Create(
			options,
			BaseAddress,
			new ApiKeyAuthenticationHandler(options.ApiKey),
			options.ReadOnly ? ReadOnlyPosts() : null,
			innerHandler);
		_httpClient = Rapid7Pipeline.CreateHttpClient(_pipeline, BaseAddress);
	}

	/// <summary>The Cloud Integrations address every endpoint path is appended to, always ending in <c>vm/</c>.</summary>
	public Uri BaseAddress { get; }

	/// <summary>POSTs that only read: the asset, site and vulnerability searches.</summary>
	[GeneratedRegex("^v4/integration/(assets|sites|vulnerabilities)$", RegexOptions.CultureInvariant)]
	private static partial Regex ReadOnlyPosts();

	private static Rapid7PlatformOptions Validated(Rapid7PlatformOptions options)
	{
		ArgumentNullException.ThrowIfNull(options);
		options.Validate();
		return options;
	}

	internal T For<T>() => RestService.For<T>(_httpClient, Rapid7Pipeline.Settings);

	/// <inheritdoc />
	public void Dispose()
	{
		_httpClient.Dispose();
		_pipeline.Dispose();
	}
}
