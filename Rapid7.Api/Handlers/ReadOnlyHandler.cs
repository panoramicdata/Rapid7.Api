using System.Text.RegularExpressions;

namespace Rapid7.Api.Handlers;

/// <summary>
/// Refuses, before anything is sent, every request that could change the target: any method other than GET or HEAD,
/// except POSTs whose path (relative to the client's base address) matches <paramref name="readOnlyPosts"/>, such as
/// searches.
/// </summary>
internal sealed class ReadOnlyHandler(Uri baseUri, Regex readOnlyPosts) : DelegatingHandler
{
	/// <inheritdoc />
	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		if (!IsAllowed(request))
		{
			throw new Rapid7ReadOnlyException(request.Method.Method, request.RequestUri!.GetLeftPart(UriPartial.Path));
		}

		return base.SendAsync(request, cancellationToken);
	}

	private bool IsAllowed(HttpRequestMessage request)
	{
		if (request.Method == HttpMethod.Get || request.Method == HttpMethod.Head)
		{
			return true;
		}

		return request.Method == HttpMethod.Post
			&& RelativePath(baseUri, request.RequestUri!) is { } relative
			&& readOnlyPosts.IsMatch(relative);
	}

	/// <summary>
	/// The request path relative to <paramref name="baseUri"/>, without a trailing slash, or <see langword="null"/> when the
	/// request is not below the base address.
	/// </summary>
	internal static string? RelativePath(Uri baseUri, Uri requestUri)
	{
		var basePath = UriPaths.WithTrailingSlash(baseUri.AbsolutePath);
		var path = requestUri.AbsolutePath;
		return path.StartsWith(basePath, StringComparison.Ordinal) ? path[basePath.Length..].TrimEnd('/') : null;
	}
}
