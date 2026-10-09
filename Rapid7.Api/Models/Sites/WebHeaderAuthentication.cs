using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>A site's web authentication that sends fixed HTTP headers (for example a session cookie).</summary>
public sealed class WebHeaderAuthentication : WebAuthentication
{
	/// <summary>
	/// The headers sent to reach authenticated pages, by name. The console leaves them out of its responses, as they are
	/// secrets, so this is usually empty when read.
	/// </summary>
	[JsonPropertyName("headers")]
	public IReadOnlyDictionary<string, string> Headers { get; init; } = new Dictionary<string, string>();
}
