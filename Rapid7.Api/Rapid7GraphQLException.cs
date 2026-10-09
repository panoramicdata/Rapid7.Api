using Rapid7.Api.Models.BulkExport;
using System.Net;

namespace Rapid7.Api;

/// <summary>
/// Raised when the Bulk Export GraphQL API answers with GraphQL <c>errors</c>, which it can do with a 200 response, or
/// answers without the data asked for.
/// </summary>
public sealed class Rapid7GraphQLException : Exception
{
	/// <summary>Creates an exception for a response that carried <paramref name="errors"/>.</summary>
	/// <param name="statusCode">The HTTP status code of the response (often 200).</param>
	/// <param name="errors">The GraphQL errors, at least one.</param>
	public Rapid7GraphQLException(HttpStatusCode statusCode, IReadOnlyList<GraphQLError> errors)
		: this(statusCode, "The Bulk Export API returned GraphQL errors: " + string.Join("; ", errors), errors)
	{
	}

	/// <summary>Creates an exception with a message of its own.</summary>
	/// <param name="statusCode">The HTTP status code of the response.</param>
	/// <param name="message">The exception message.</param>
	/// <param name="errors">The GraphQL errors, if any.</param>
	public Rapid7GraphQLException(HttpStatusCode statusCode, string message, IReadOnlyList<GraphQLError> errors)
		: base(message)
	{
		StatusCode = statusCode;
		Errors = errors;
	}

	/// <summary>The HTTP status code of the response (often 200: GraphQL reports errors in the body).</summary>
	public HttpStatusCode StatusCode { get; }

	/// <summary>The GraphQL errors, each with its message and, when reported, the path of the field that failed.</summary>
	public IReadOnlyList<GraphQLError> Errors { get; }
}
