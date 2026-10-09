using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanEngines;

/// <summary>The state of a scan engine as seen by the Security Console.</summary>
public enum ScanEngineStatus
{
	/// <summary>The console does not know the engine's state (also any value this library does not recognise).</summary>
	[JsonStringEnumMemberName("unknown")]
	Unknown = 0,

	/// <summary>The engine is available for scanning.</summary>
	[JsonStringEnumMemberName("active")]
	Active,

	/// <summary>The engine's product version does not work with this console.</summary>
	[JsonStringEnumMemberName("incompatible-version")]
	IncompatibleVersion,

	/// <summary>The engine does not answer the console.</summary>
	[JsonStringEnumMemberName("not-responding")]
	NotResponding,

	/// <summary>The engine has not yet authorized the console to connect.</summary>
	[JsonStringEnumMemberName("pending-authorization")]
	PendingAuthorization
}
