namespace Rapid7.Api.Handlers;

/// <summary>
/// Authenticates Insight platform requests with the <c>X-Api-Key</c> header and asks for JSON
/// (<c>Accept: application/json</c>; the Cloud Integrations API can otherwise answer in XML).
/// </summary>
internal sealed class ApiKeyAuthenticationHandler(string apiKey) : DelegatingHandler
{
	internal const string ApiKeyHeader = "X-Api-Key";

	/// <inheritdoc />
	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		request.Headers.Remove(ApiKeyHeader);
		request.Headers.Add(ApiKeyHeader, apiKey);
		JsonAccept.Apply(request);
		return base.SendAsync(request, cancellationToken);
	}
}
