using System.Text.RegularExpressions;
using Rapid7.Api.Handlers;
using Rapid7.Api.Serialization;
using Refit;

namespace Rapid7.Api;

/// <summary>The Refit settings and HTTP handler pipeline every Rapid7 client is built from.</summary>
internal static class Rapid7Pipeline
{
	/// <summary>The Refit settings shared by every client and endpoint group.</summary>
	internal static RefitSettings Settings { get; } = new()
	{
		ContentSerializer = new SystemTextJsonContentSerializer(Rapid7Json.Options),
		// Interface paths are relative (no leading slash) so they append to a path-prefixed base address.
		UrlResolution = UrlResolutionMode.Rfc3986,
		UrlParameterFormatter = new Rapid7UrlParameterFormatter(),
		// Buffered bodies are replayable, so a request can be retried after a 429/503 or a failed connection.
		Buffered = true,
		ExceptionFactory = response => new ValueTask<Exception?>(Rapid7ErrorMapper.CreateAsync(response)),
		// Surface transport failures (TimeoutException, HttpRequestException...) as themselves, not wrapped by Refit.
		TransportExceptionFactory = static (_, exception, _) => exception
	};

	/// <summary>
	/// Builds the read-only guard (when <paramref name="readOnlyPosts"/> is set), then <paramref name="authentication"/>, then
	/// retries and timeouts, over <paramref name="innerHandler"/>.
	/// </summary>
	internal static HttpMessageHandler Create(
		Rapid7ConnectionOptions options,
		Uri baseAddress,
		DelegatingHandler authentication,
		Regex? readOnlyPosts,
		HttpMessageHandler innerHandler)
	{
		authentication.InnerHandler = new RetryHandler(options) { InnerHandler = innerHandler };
		return readOnlyPosts is null
			? authentication
			: new ReadOnlyHandler(baseAddress, readOnlyPosts) { InnerHandler = authentication };
	}

	/// <summary>An HttpClient over a pipeline it does not own, with timeouts left to the retry handler.</summary>
	internal static HttpClient CreateHttpClient(HttpMessageHandler pipeline, Uri baseAddress)
		=> new(pipeline, disposeHandler: false)
		{
			BaseAddress = baseAddress,
			// The per-attempt timeout is applied inside RetryHandler so retries and Retry-After waits are not cut short.
			Timeout = System.Threading.Timeout.InfiniteTimeSpan
		};
}
