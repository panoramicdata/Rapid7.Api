using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>Which ports a scan template scans for services.</summary>
public enum ScanTemplatePortSet
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Every port.</summary>
	[JsonStringEnumMemberName("all")]
	All,

	/// <summary>The well-known ports.</summary>
	[JsonStringEnumMemberName("well-known")]
	WellKnown,

	/// <summary>Only the additional ports listed.</summary>
	[JsonStringEnumMemberName("custom")]
	Custom,

	/// <summary>No ports.</summary>
	[JsonStringEnumMemberName("none")]
	None
}
