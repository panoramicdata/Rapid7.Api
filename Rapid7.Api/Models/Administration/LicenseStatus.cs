using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The status of the console licence.</summary>
public enum LicenseStatus
{
	/// <summary>The console reports the status as unknown (<c>Unknown</c>), or sent a value this client does not recognise.</summary>
	Unknown = 0,

	/// <summary>Licensed and active (<c>Activated</c>).</summary>
	[JsonStringEnumMemberName("Activated")]
	Activated,

	/// <summary>Not licensed (<c>Unlicensed</c>).</summary>
	[JsonStringEnumMemberName("Unlicensed")]
	Unlicensed,

	/// <summary>The licence has expired (<c>Expired</c>).</summary>
	[JsonStringEnumMemberName("Expired")]
	Expired,

	/// <summary>Running under an evaluation licence (<c>Evaluation Mode</c>).</summary>
	[JsonStringEnumMemberName("Evaluation Mode")]
	EvaluationMode,

	/// <summary>The licence was revoked (<c>Revoked</c>).</summary>
	[JsonStringEnumMemberName("Revoked")]
	Revoked
}
