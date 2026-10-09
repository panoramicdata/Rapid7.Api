using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.PolicyOverrides;

/// <summary>Which assets a policy override applies to.</summary>
public enum PolicyOverrideScopeType
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>Every asset the rule is checked against.</summary>
	[JsonStringEnumMemberName("all-assets")]
	AllAssets,

	/// <summary>One asset.</summary>
	[JsonStringEnumMemberName("specific-asset")]
	SpecificAsset,

	/// <summary>One asset, until it is next scanned.</summary>
	[JsonStringEnumMemberName("specific-asset-until-next-scan")]
	SpecificAssetUntilNextScan
}
