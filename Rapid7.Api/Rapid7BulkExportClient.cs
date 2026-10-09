using Rapid7.Api.Handlers;
using Refit;
using System.Text.RegularExpressions;

namespace Rapid7.Api;

/// <summary>
/// Client for the InsightVM Bulk Export API, a GraphQL API at <c>https://&lt;region&gt;.api.insight.rapid7.com/export/graphql</c>
/// that exports asset, vulnerability, policy, remediation and software data as Parquet files.
/// </summary>
/// <remarks>
/// Every GraphQL request sends the <c>X-Api-Key</c> header (the key needs Platform Administrator permissions) and retries
/// transient failures. HTTP errors raise <see cref="Rapid7ApiException"/>, GraphQL errors
/// <see cref="Rapid7GraphQLException"/>. File downloads go through the same transport and retries but never carry the
/// API key: the URLs are pre-signed and point at another host. A client is thread-safe and intended to be long-lived;
/// dispose it when done.
/// </remarks>
public sealed partial class Rapid7BulkExportClient : IDisposable
{
	private readonly HttpMessageHandler _pipeline;
	private readonly HttpClient _httpClient;
	private readonly HttpMessageHandler _downloadPipeline;
	private readonly HttpClient _downloadClient;

	/// <summary>Creates a client.</summary>
	/// <param name="options">Platform options: region (or base URL) and API key.</param>
	public Rapid7BulkExportClient(Rapid7PlatformOptions options) : this(options, Rapid7Transport.Create(Validated(options)))
	{
	}

	/// <summary>
	/// Creates a client that sends requests through <paramref name="innerHandler"/>, which it takes ownership of and
	/// disposes. The certificate settings in <paramref name="options"/> are ignored: configure them on the handler.
	/// </summary>
	/// <param name="options">Platform options: region (or base URL) and API key.</param>
	/// <param name="innerHandler">The handler that sends requests to the network.</param>
	public Rapid7BulkExportClient(Rapid7PlatformOptions options, HttpMessageHandler innerHandler)
	{
		ArgumentNullException.ThrowIfNull(innerHandler);
		Validated(options);
		BaseAddress = options.PlatformAddress;
		_pipeline = Rapid7Pipeline.Create(
			options,
			BaseAddress,
			new ApiKeyAuthenticationHandler(options.ApiKey),
			options.ReadOnly ? ReadOnlyPosts() : null,
			innerHandler);
		_httpClient = Rapid7Pipeline.CreateHttpClient(_pipeline, BaseAddress);
		// Downloads share the transport and retries, but not the authentication: pre-signed URLs must not get the API key.
		_downloadPipeline = new RetryHandler(options) { InnerHandler = innerHandler };
		_downloadClient = new HttpClient(_downloadPipeline, disposeHandler: false) { Timeout = System.Threading.Timeout.InfiniteTimeSpan };
	}

	/// <summary>The platform address the GraphQL path (<c>export/graphql</c>) is appended to, always ending in <c>/</c>.</summary>
	public Uri BaseAddress { get; }

	/// <summary>Exports only read data, so the GraphQL endpoint stays usable when the client is read-only.</summary>
	[GeneratedRegex("^export/graphql$", RegexOptions.CultureInvariant)]
	private static partial Regex ReadOnlyPosts();

	private static Rapid7PlatformOptions Validated(Rapid7PlatformOptions options)
	{
		ArgumentNullException.ThrowIfNull(options);
		options.Validate();
		return options;
	}

	internal T For<T>() => RestService.For<T>(_httpClient, Rapid7GraphQL.Settings);

	/// <inheritdoc />
	public void Dispose()
	{
		_downloadClient.Dispose();
		_httpClient.Dispose();
		_pipeline.Dispose();
		// Disposes the transport a second time (the GraphQL pipeline already did), which handlers tolerate.
		_downloadPipeline.Dispose();
	}
}
