using System.Text.RegularExpressions;

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
	private readonly Rapid7ClientCore _core;

	/// <summary>Creates a client.</summary>
	/// <param name="options">Connection options.</param>
	public Rapid7Client(Rapid7ClientOptions options) : this(options, Rapid7Transport.Create(Rapid7ConnectionOptions.Validated(options)))
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
		BaseAddress = Rapid7ConnectionOptions.Validated(options).BaseAddress;
		_core = options.CreateCore(BaseAddress, options.ReadOnly ? ReadOnlyPosts() : null, innerHandler);
	}

	/// <summary>The console address every endpoint path is appended to, always ending in <c>/</c>.</summary>
	public Uri BaseAddress { get; }

	/// <summary>POSTs that only read: searches.</summary>
	[GeneratedRegex("^api/3/(assets/search|sonar_queries/search)$", RegexOptions.CultureInvariant)]
	private static partial Regex ReadOnlyPosts();

	internal T For<T>() => _core.For<T>();

	/// <inheritdoc />
	public void Dispose() => _core.Dispose();
}
