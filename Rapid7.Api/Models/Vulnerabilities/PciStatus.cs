using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Vulnerabilities;

/// <summary>Whether a vulnerability passes or fails PCI compliance.</summary>
public enum PciStatus
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>The vulnerability does not cause a PCI failure.</summary>
	[JsonStringEnumMemberName("Pass")]
	Pass,

	/// <summary>The vulnerability causes a PCI failure.</summary>
	[JsonStringEnumMemberName("Fail")]
	Fail,
}
