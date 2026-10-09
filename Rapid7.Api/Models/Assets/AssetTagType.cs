using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>The type of a tag.</summary>
public enum AssetTagType
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>A custom tag.</summary>
	[JsonStringEnumMemberName("custom")]
	Custom,

	/// <summary>A location tag.</summary>
	[JsonStringEnumMemberName("location")]
	Location,

	/// <summary>An owner tag.</summary>
	[JsonStringEnumMemberName("owner")]
	Owner,

	/// <summary>A criticality tag, which adjusts the risk score of the assets it is applied to.</summary>
	[JsonStringEnumMemberName("criticality")]
	Criticality
}
