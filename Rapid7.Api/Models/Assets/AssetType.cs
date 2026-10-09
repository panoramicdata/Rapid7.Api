using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>The kind of machine an asset is.</summary>
public enum AssetType
{
	/// <summary>The console does not know the kind, or the value is one this library does not recognise.</summary>
	[JsonStringEnumMemberName("unknown")]
	Unknown = 0,

	/// <summary>A virtual machine guest.</summary>
	[JsonStringEnumMemberName("guest")]
	Guest,

	/// <summary>A virtual machine host.</summary>
	[JsonStringEnumMemberName("hypervisor")]
	Hypervisor,

	/// <summary>A physical machine.</summary>
	[JsonStringEnumMemberName("physical")]
	Physical,

	/// <summary>A mobile device.</summary>
	[JsonStringEnumMemberName("mobile")]
	Mobile
}
