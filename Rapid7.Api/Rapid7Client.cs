using System.Text.RegularExpressions;
using Rapid7.Api.Handlers;
using Refit;

namespace Rapid7.Api;

/// <summary>
/// Client for the InsightVM Security Console API (v3, <c>https://&lt;console&gt;:3780/api/3/</c>). Each group of
/// endpoints is a property (for example <see cref="Root"/>); the groups are created on first use.
/// </summary>
/// <remarks>
/// <para>
/// Every request authenticates with HTTP basic credentials (plus the <c>Token</c> header for two-factor accounts), asks
/// for JSON, and retries transient failures. Non-success responses raise <see cref="Rapid7ApiException"/>. Read every page
/// of a paged collection with <see cref="Rapid7Paging"/>.
/// </para>
/// <para>
/// A client is thread-safe and intended to be long-lived: create one per console and identity, and dispose it when done.
/// For the Insight platform APIs, use <see cref="Rapid7CloudClient"/> and <see cref="Rapid7BulkExportClient"/>.
/// </para>
/// </remarks>
public sealed partial class Rapid7Client : IDisposable
{
	private readonly HttpMessageHandler _pipeline;
	private readonly HttpClient _httpClient;

	/// <summary>Creates a client.</summary>
	/// <param name="options">Connection options.</param>
	public Rapid7Client(Rapid7ClientOptions options) : this(options, Rapid7Transport.Create(Validated(options)))
	{
	}

	/// <summary>
	/// Creates a client that sends requests through <paramref name="innerHandler"/> (for example a proxy-aware or
	/// instrumented handler), which it takes ownership of and disposes. The certificate settings in
	/// <paramref name="options"/> are ignored: configure certificate validation on the handler instead.
	/// </summary>
	/// <param name="options">Connection options.</param>
	/// <param name="innerHandler">The handler that sends requests to the network.</param>
	public Rapid7Client(Rapid7ClientOptions options, HttpMessageHandler innerHandler)
	{
		ArgumentNullException.ThrowIfNull(innerHandler);
		Validated(options);
		BaseAddress = Rapid7Pipeline.WithTrailingSlash(options.BaseUrl);
		_pipeline = Rapid7Pipeline.Create(
			options,
			BaseAddress,
			new BasicAuthenticationHandler(options.Username, options.Password, options.TwoFactorToken),
			options.ReadOnly ? ReadOnlyPosts() : null,
			innerHandler);
		_httpClient = Rapid7Pipeline.CreateHttpClient(_pipeline, BaseAddress);
	}

	/// <summary>The console address every endpoint path is appended to, always ending in <c>/</c>.</summary>
	public Uri BaseAddress { get; }

	/// <summary>POSTs that only read: searches.</summary>
	[GeneratedRegex("^api/3/(assets/search|sonar_queries/search)$", RegexOptions.CultureInvariant)]
	private static partial Regex ReadOnlyPosts();

	// Validation comes before the transport is created, so invalid options do not leave an undisposed network handler.
	private static Rapid7ClientOptions Validated(Rapid7ClientOptions options)
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
