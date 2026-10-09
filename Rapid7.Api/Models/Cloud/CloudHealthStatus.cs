using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>The status of the Cloud Integrations API.</summary>
public enum CloudHealthStatus
{
	/// <summary>The status is unknown (<c>UNKNOWN</c>), or a value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Healthy (<c>UP</c>).</summary>
	[JsonStringEnumMemberName("UP")]
	Up,

	/// <summary>Unavailable (<c>DOWN</c>).</summary>
	[JsonStringEnumMemberName("DOWN")]
	Down,

	/// <summary>Deliberately taken out of service (<c>OUT-OF-SERVICE</c>).</summary>
	[JsonStringEnumMemberName("OUT-OF-SERVICE")]
	OutOfService
}
