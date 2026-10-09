using Rapid7.Api.Models;
using System.Net;

namespace Rapid7.Api;

/// <summary>Raised when a Rapid7 API returns a non-success response.</summary>
/// <param name="statusCode">The HTTP status code.</param>
/// <param name="errorStatus">The API's own status, such as <c>NOT_FOUND</c> (v3) or <c>404</c> (v4), when supplied.</param>
/// <param name="message">The exception message: the API's error message, or a fallback naming the status.</param>
/// <param name="localizedMessage">The API's user-facing message (<c>localized_message</c>), when supplied.</param>
/// <param name="links">Hypermedia links to related resources returned with the error; empty when none.</param>
public sealed class Rapid7ApiException(
	HttpStatusCode statusCode,
	string? errorStatus,
	string message,
	string? localizedMessage,
	IReadOnlyList<Link> links)
	: Exception(message)
{
	/// <summary>The HTTP status code.</summary>
	public HttpStatusCode StatusCode { get; } = statusCode;

	/// <summary>The API's own status, such as <c>NOT_FOUND</c> (v3) or <c>404</c> (v4), when supplied.</summary>
	public string? ErrorStatus { get; } = errorStatus;

	/// <summary>The API's user-facing message (<c>localized_message</c>), when supplied.</summary>
	public string? LocalizedMessage { get; } = localizedMessage;

	/// <summary>Hypermedia links to related resources returned with the error; empty when none.</summary>
	public IReadOnlyList<Link> Links { get; } = links;
}
