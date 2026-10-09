using System.Text.RegularExpressions;

namespace Rapid7.Api;

/// <summary>
/// Client for the InsightVM Bulk Export API, a GraphQL API at <c>https://&lt;region&gt;.api.insight.rapid7.com/export/graphql</c>
/// that exports asset, vulnerability, policy, remediation and software data as Parquet files.
/// </summary>
/// <remarks>
/// Every request sends the <c>X-Api-Key</c> header (the key needs Platform Administrator permissions) and retries
/// transient failures. HTTP errors raise <see cref="Rapid7ApiException"/>. A client is thread-safe and intended to be
/// long-lived; dispose it when done.
/// </remarks>
public sealed partial class Rapid7BulkExportClient : IDisposable
{
	private readonly Rapid7ClientCore _core;

	/// <summary>Creates a client.</summary>
	/// <param name="options">Platform options: region (or base URL) and API key.</param>
	public Rapid7BulkExportClient(Rapid7PlatformOptions options) : this(options, Rapid7Transport.Create(Rapid7ConnectionOptions.Validated(options)))
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
		BaseAddress = Rapid7ConnectionOptions.Validated(options).PlatformAddress;
		_core = options.CreateCore(BaseAddress, options.ReadOnly ? ReadOnlyPosts() : null, innerHandler);
	}

	/// <summary>The platform address the GraphQL path (<c>export/graphql</c>) is appended to, always ending in <c>/</c>.</summary>
	public Uri BaseAddress { get; }

	/// <summary>Exports only read data, so the GraphQL endpoint stays usable when the client is read-only.</summary>
	[GeneratedRegex("^export/graphql$", RegexOptions.CultureInvariant)]
	private static partial Regex ReadOnlyPosts();

	internal T For<T>() => _core.For<T>();

	/// <inheritdoc />
	public void Dispose() => _core.Dispose();
}
