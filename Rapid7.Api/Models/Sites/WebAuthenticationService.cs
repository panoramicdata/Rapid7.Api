using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>The kind of web application authentication a site configures.</summary>
public enum WebAuthenticationService
{
	/// <summary>A value this client does not recognise.</summary>
	Unknown = 0,

	/// <summary>Logging on through an HTML form.</summary>
	[JsonStringEnumMemberName("html-form")]
	HtmlForm,

	/// <summary>Sending fixed HTTP headers, such as a session cookie.</summary>
	[JsonStringEnumMemberName("http-header")]
	HttpHeader,
}
