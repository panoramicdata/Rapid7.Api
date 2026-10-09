using Rapid7.Api.Models.BulkExport;
using Refit;
using System.Text.Json;

namespace Rapid7.Api;

/// <summary>
/// The Refit settings of the Bulk Export GraphQL endpoint: the shared settings, except that a response whose body carries
/// GraphQL <c>errors</c> raises <see cref="Rapid7GraphQLException"/>, whatever its HTTP status.
/// </summary>
internal static class Rapid7GraphQL
{
	/// <summary>The settings, built from <see cref="Rapid7Pipeline.Settings"/>.</summary>
	internal static RefitSettings Settings { get; } = new()
	{
		ContentSerializer = Rapid7Pipeline.Settings.ContentSerializer,
		UrlResolution = Rapid7Pipeline.Settings.UrlResolution,
		UrlParameterFormatter = Rapid7Pipeline.Settings.UrlParameterFormatter,
		Buffered = Rapid7Pipeline.Settings.Buffered,
		ExceptionFactory = response => new ValueTask<Exception?>(CreateAsync(response)),
		TransportExceptionFactory = Rapid7Pipeline.Settings.TransportExceptionFactory
	};

	/// <summary>
	/// A <see cref="Rapid7GraphQLException"/> when the body carries GraphQL errors; otherwise what
	/// <see cref="Rapid7ErrorMapper"/> makes of the response (nothing, for a success).
	/// </summary>
	internal static async Task<Exception?> CreateAsync(HttpResponseMessage response)
	{
		// Reading the body buffers it, so Refit can still deserialise a successful response afterwards.
		var errors = ReadErrors(await Rapid7ErrorMapper.ReadBodyAsync(response).ConfigureAwait(false));
		return errors.Count > 0
			? new Rapid7GraphQLException(response.StatusCode, errors)
			: await Rapid7ErrorMapper.CreateAsync(response).ConfigureAwait(false);
	}

	/// <summary>The GraphQL errors in a response body; empty when there are none or the body is not a JSON object.</summary>
	internal static IReadOnlyList<GraphQLError> ReadErrors(string body)
	{
		if (!body.TrimStart().StartsWith('{'))
		{
			return [];
		}

		try
		{
			return JsonSerializer.Deserialize<GraphQLResponse<JsonElement>>(body, Rapid7Json.Options)!.Errors;
		}
		catch (JsonException)
		{
			return [];
		}
	}
}
