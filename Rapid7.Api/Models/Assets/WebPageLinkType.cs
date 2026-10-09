using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>How the web spider found a page.</summary>
public enum WebPageLinkType
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>A seed address the spider started from.</summary>
	[JsonStringEnumMemberName("seed")]
	Seed,

	/// <summary>A reference in HTML.</summary>
	[JsonStringEnumMemberName("html-ref")]
	HtmlReference,

	/// <summary>Listed in <c>robots.txt</c>.</summary>
	[JsonStringEnumMemberName("robots")]
	Robots,

	/// <summary>A string in JavaScript.</summary>
	[JsonStringEnumMemberName("js-string")]
	JavaScriptString,

	/// <summary>A query parameter.</summary>
	[JsonStringEnumMemberName("query-param")]
	QueryParameter,

	/// <summary>A link in a PDF document.</summary>
	[JsonStringEnumMemberName("pdf")]
	Pdf,

	/// <summary>A reference in a style sheet.</summary>
	[JsonStringEnumMemberName("css")]
	Css,

	/// <summary>A directory implied by another page.</summary>
	[JsonStringEnumMemberName("implied-dir")]
	ImpliedDirectory,

	/// <summary>A link in an RSS feed.</summary>
	[JsonStringEnumMemberName("rss")]
	Rss,

	/// <summary>The target of a redirect.</summary>
	[JsonStringEnumMemberName("redirection")]
	Redirection,

	/// <summary>Listed in a site map.</summary>
	[JsonStringEnumMemberName("sitemap")]
	Sitemap,

	/// <summary>A guessed backup file.</summary>
	[JsonStringEnumMemberName("backup")]
	Backup,

	/// <summary>A rewritten address.</summary>
	[JsonStringEnumMemberName("vck-rewrite")]
	VckRewrite,

	/// <summary>A guess that no reference pointed to.</summary>
	[JsonStringEnumMemberName("non-ref-guess")]
	NonReferenceGuess,

	/// <summary>A page that answered "not found" with a success status.</summary>
	[JsonStringEnumMemberName("soft-404")]
	Soft404
}
