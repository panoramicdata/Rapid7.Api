using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>A site's web authentication that logs on through an HTML form.</summary>
public sealed class WebFormAuthentication : WebAuthentication
{
	/// <summary>The address of the page holding the logon form, including the base URL (<c>http://example.test/login</c>).</summary>
	[JsonPropertyName("loginURL")]
	public string? LoginUrl { get; init; }
}
