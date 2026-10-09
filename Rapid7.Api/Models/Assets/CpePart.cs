using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>The part (class of product) of a Common Platform Enumeration name.</summary>
public enum CpePart
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>An operating system (<c>o</c>).</summary>
	[JsonStringEnumMemberName("o")]
	OperatingSystem,

	/// <summary>An application (<c>a</c>).</summary>
	[JsonStringEnumMemberName("a")]
	Application,

	/// <summary>A hardware device (<c>h</c>).</summary>
	[JsonStringEnumMemberName("h")]
	Hardware
}
