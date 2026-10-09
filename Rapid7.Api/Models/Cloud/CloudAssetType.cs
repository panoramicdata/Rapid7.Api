using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>The kind of machine an asset is.</summary>
public enum CloudAssetType
{
	/// <summary>Not known (<c>unknown</c>), or a value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>A virtual machine (<c>guest</c>).</summary>
	[JsonStringEnumMemberName("guest")]
	Guest,

	/// <summary>A virtualisation host (<c>hypervisor</c>).</summary>
	[JsonStringEnumMemberName("hypervisor")]
	Hypervisor,

	/// <summary>A physical machine (<c>physical</c>).</summary>
	[JsonStringEnumMemberName("physical")]
	Physical,

	/// <summary>A mobile device (<c>mobile</c>).</summary>
	[JsonStringEnumMemberName("mobile")]
	Mobile
}
