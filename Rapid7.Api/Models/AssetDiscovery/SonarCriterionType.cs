using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.AssetDiscovery;

/// <summary>The kind of a <see cref="SonarCriterion"/>.</summary>
public enum SonarCriterionType
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Assets whose domain contains <see cref="SonarCriterion.Domain"/>.</summary>
	[JsonStringEnumMemberName("domain-contains")]
	DomainContains,

	/// <summary>Assets Sonar scanned within the last <see cref="SonarCriterion.Days"/> days.</summary>
	[JsonStringEnumMemberName("scan-date-within-the-last")]
	ScanDateWithinTheLast,

	/// <summary>Assets whose address lies from <see cref="SonarCriterion.Lower"/> to <see cref="SonarCriterion.Upper"/>.</summary>
	[JsonStringEnumMemberName("ip-address-range")]
	IpAddressRange
}
