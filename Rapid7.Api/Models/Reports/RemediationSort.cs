using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>How a remediation report orders its solutions.</summary>
public enum RemediationSort
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>By the number of assets affected.</summary>
	[JsonStringEnumMemberName("assets")]
	Assets,

	/// <summary>By the number of vulnerabilities fixed.</summary>
	[JsonStringEnumMemberName("vulnerabilities")]
	Vulnerabilities,

	/// <summary>By the number of malware kits.</summary>
	[JsonStringEnumMemberName("malware_kits")]
	MalwareKits,

	/// <summary>By the number of exploits.</summary>
	[JsonStringEnumMemberName("exploits")]
	Exploits,

	/// <summary>By risk score.</summary>
	[JsonStringEnumMemberName("riskscore")]
	RiskScore
}
