using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>What HTML form and HTTP header web authentications of a site have in common.</summary>
public abstract class WebAuthentication : LinksResource
{
	/// <summary>The address every path of the target web site starts from, including the scheme (<c>http://example.test</c>).</summary>
	[JsonPropertyName("baseURL")]
	public string? BaseUrl { get; init; }

	/// <summary>Whether the site's scans use this authentication.</summary>
	[JsonPropertyName("enabled")]
	public bool? Enabled { get; init; }

	/// <summary>The identifier of the authentication.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>A regular expression matching the page the web server returns when a logon fails.</summary>
	[JsonPropertyName("loginRegularExpression")]
	public string? LoginRegularExpression { get; init; }

	/// <summary>The name of the authentication.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>Whether this is an HTML form or an HTTP header authentication.</summary>
	[JsonPropertyName("service")]
	public WebAuthenticationService? Service { get; init; }
}
