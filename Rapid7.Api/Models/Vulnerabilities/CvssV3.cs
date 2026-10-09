using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Vulnerabilities;

/// <summary>A CVSS v3 score and its components.</summary>
public sealed class CvssV3 : CvssScore
{
	/// <summary>The Attack Complexity (AC) component.</summary>
	[JsonPropertyName("attackComplexity")]
	public CvssAttackComplexity? AttackComplexity { get; init; }

	/// <summary>The Attack Vector (AV) component.</summary>
	[JsonPropertyName("attackVector")]
	public CvssAttackVector? AttackVector { get; init; }

	/// <summary>The Availability Impact (A) component.</summary>
	[JsonPropertyName("availabilityImpact")]
	public CvssV3Rating? AvailabilityImpact { get; init; }

	/// <summary>The Confidentiality Impact (C) component.</summary>
	[JsonPropertyName("confidentialityImpact")]
	public CvssV3Rating? ConfidentialityImpact { get; init; }

	/// <summary>The Integrity Impact (I) component.</summary>
	[JsonPropertyName("integrityImpact")]
	public CvssV3Rating? IntegrityImpact { get; init; }

	/// <summary>The Privileges Required (PR) component (the console names it <c>privilegeRequired</c>).</summary>
	[JsonPropertyName("privilegeRequired")]
	public CvssV3Rating? PrivilegesRequired { get; init; }

	/// <summary>The Scope (S) component.</summary>
	[JsonPropertyName("scope")]
	public CvssScope? Scope { get; init; }

	/// <summary>The User Interaction (UI) component.</summary>
	[JsonPropertyName("userInteraction")]
	public CvssUserInteraction? UserInteraction { get; init; }
}
