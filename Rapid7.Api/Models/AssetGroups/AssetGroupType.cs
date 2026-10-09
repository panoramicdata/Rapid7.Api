using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.AssetGroups;

/// <summary>Whether an asset group's membership is fixed or follows search criteria.</summary>
public enum AssetGroupType
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>A static group: assets are added and removed explicitly.</summary>
	[JsonStringEnumMemberName("static")]
	Static,

	/// <summary>A dynamic group: the members are the assets that match the group's search criteria.</summary>
	[JsonStringEnumMemberName("dynamic")]
	Dynamic
}
