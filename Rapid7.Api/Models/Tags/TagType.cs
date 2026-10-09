using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Tags;

/// <summary>The kind of a tag.</summary>
public enum TagType
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>A free-form tag.</summary>
	[JsonStringEnumMemberName("custom")]
	Custom,

	/// <summary>Where assets are.</summary>
	[JsonStringEnumMemberName("location")]
	Location,

	/// <summary>Who is responsible for assets.</summary>
	[JsonStringEnumMemberName("owner")]
	Owner,

	/// <summary>How important assets are; adjusts their risk scores.</summary>
	[JsonStringEnumMemberName("criticality")]
	Criticality
}
