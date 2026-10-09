using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanEngines;

/// <summary>The state of a scan engine as the console sees it.</summary>
public enum ScanEngineStatus
{
	/// <summary>A value this library does not recognise, or the console's own <c>unknown</c>.</summary>
	Unknown = 0,

	/// <summary>The engine is connected and ready.</summary>
	[JsonStringEnumMemberName("active")]
	Active,

	/// <summary>The engine runs a version the console cannot work with.</summary>
	[JsonStringEnumMemberName("incompatible-version")]
	IncompatibleVersion,

	/// <summary>The engine does not answer.</summary>
	[JsonStringEnumMemberName("not-responding")]
	NotResponding,

	/// <summary>The engine waits to be authorized on the console.</summary>
	[JsonStringEnumMemberName("pending-authorization")]
	PendingAuthorization
}
