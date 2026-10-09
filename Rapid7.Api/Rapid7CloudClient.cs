using System.Text.RegularExpressions;

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
	private readonly Rapid7ClientCore _core;

	/// <summary>Creates a client.</summary>
	/// <param name="options">Platform options: region (or base URL) and API key.</param>
	public Rapid7CloudClient(Rapid7PlatformOptions options) : this(options, Rapid7Transport.Create(Rapid7ConnectionOptions.Validated(options)))
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
		BaseAddress = new Uri(Rapid7ConnectionOptions.Validated(options).PlatformAddress, "vm/");
		_core = options.CreateCore(BaseAddress, options.ReadOnly ? ReadOnlyPosts() : null, innerHandler);
	}

	/// <summary>The Cloud Integrations address every endpoint path is appended to, always ending in <c>vm/</c>.</summary>
	public Uri BaseAddress { get; }

	/// <summary>POSTs that only read: the asset, site and vulnerability searches.</summary>
	[GeneratedRegex("^v4/integration/(assets|sites|vulnerabilities)$", RegexOptions.CultureInvariant)]
	private static partial Regex ReadOnlyPosts();

	internal T For<T>() => _core.For<T>();

	/// <inheritdoc />
	public void Dispose() => _core.Dispose();
}
