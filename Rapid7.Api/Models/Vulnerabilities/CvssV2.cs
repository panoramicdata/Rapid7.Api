using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Vulnerabilities;

/// <summary>A CVSS v2 score and its components.</summary>
public sealed class CvssV2 : CvssScore
{
	/// <summary>The Access Complexity (AC) component.</summary>
	[JsonPropertyName("accessComplexity")]
	public CvssAccessComplexity? AccessComplexity { get; init; }

	/// <summary>The Access Vector (AV) component.</summary>
	[JsonPropertyName("accessVector")]
	public CvssAccessVector? AccessVector { get; init; }

	/// <summary>The Authentication (Au) component.</summary>
	[JsonPropertyName("authentication")]
	public CvssAuthentication? Authentication { get; init; }

	/// <summary>The Availability Impact (A) component.</summary>
	[JsonPropertyName("availabilityImpact")]
	public CvssV2Impact? AvailabilityImpact { get; init; }

	/// <summary>The Confidentiality Impact (C) component.</summary>
	[JsonPropertyName("confidentialityImpact")]
	public CvssV2Impact? ConfidentialityImpact { get; init; }

	/// <summary>The Integrity Impact (I) component.</summary>
	[JsonPropertyName("integrityImpact")]
	public CvssV2Impact? IntegrityImpact { get; init; }
}
