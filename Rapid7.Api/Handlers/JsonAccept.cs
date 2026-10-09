using System.Net.Http.Headers;

namespace Rapid7.Api.Handlers;

/// <summary>Asks for a JSON response unless the request already chose a media type (for example a report download).</summary>
internal static class JsonAccept
{
	private static readonly MediaTypeWithQualityHeaderValue Json = new("application/json");

	public static void Apply(HttpRequestMessage request)
	{
		if (request.Headers.Accept.Count == 0)
		{
			request.Headers.Accept.Add(Json);
		}
	}
}
