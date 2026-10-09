using Refit;
using System.Text.RegularExpressions;

namespace Rapid7.Api;

/// <summary>
/// What every Rapid7 client is made of: the handler pipeline (read-only guard, authentication, retries) over the network
/// handler, the <see cref="HttpClient"/> over it, and the Refit endpoint groups created from them.
/// </summary>
internal sealed class Rapid7ClientCore : IDisposable
{
	private readonly HttpMessageHandler _pipeline;
	private readonly HttpClient _httpClient;

	/// <summary>Builds the pipeline over <paramref name="innerHandler"/>, which this takes ownership of.</summary>
	/// <param name="options">Validated connection options.</param>
	/// <param name="baseAddress">The address endpoint paths are appended to, ending in <c>/</c>.</param>
	/// <param name="authentication">The handler that authenticates each request.</param>
	/// <param name="readOnlyPosts">The POST paths a read-only client allows, or <see langword="null"/> when not read-only.</param>
	/// <param name="innerHandler">The handler that sends requests to the network.</param>
	public Rapid7ClientCore(
		Rapid7ConnectionOptions options,
		Uri baseAddress,
		DelegatingHandler authentication,
		Regex? readOnlyPosts,
		HttpMessageHandler innerHandler)
	{
		_pipeline = Rapid7Pipeline.Create(options, baseAddress, authentication, readOnlyPosts, innerHandler);
		_httpClient = Rapid7Pipeline.CreateHttpClient(_pipeline, baseAddress);
	}

	/// <summary>Creates an endpoint group.</summary>
	public T For<T>() => For<T>(Rapid7Pipeline.Settings);

	/// <summary>A Refit client over the shared pipeline, with settings other than the default (the GraphQL client's).</summary>
	public T For<T>(RefitSettings settings) => RestService.For<T>(_httpClient, settings);

	/// <inheritdoc />
	public void Dispose()
	{
		_httpClient.Dispose();
		_pipeline.Dispose();
	}
}
