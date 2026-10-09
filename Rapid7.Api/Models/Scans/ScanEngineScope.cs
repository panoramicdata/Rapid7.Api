using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Scans;

/// <summary>Where a scan engine is available.</summary>
public enum ScanEngineScope
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Available to the whole console.</summary>
	[JsonStringEnumMemberName("global")]
	Global,

	/// <summary>Available to one silo.</summary>
	[JsonStringEnumMemberName("silo")]
	Silo
}
