using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>The kind of platform a CPE name identifies.</summary>
public enum CpePart
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>An operating system.</summary>
	[JsonStringEnumMemberName("o")]
	OperatingSystem,

	/// <summary>An application.</summary>
	[JsonStringEnumMemberName("a")]
	Application,

	/// <summary>A hardware device.</summary>
	[JsonStringEnumMemberName("h")]
	Hardware
}
