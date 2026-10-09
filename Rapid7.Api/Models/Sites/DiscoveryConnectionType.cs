using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>The kind of discovery connection a dynamic site draws its assets from.</summary>
public enum DiscoveryConnectionType
{
	/// <summary>A value this client does not recognise.</summary>
	Unknown = 0,

	/// <summary>Exchange ActiveSync through LDAP.</summary>
	[JsonStringEnumMemberName("activesync-ldap")]
	ActiveSyncLdap,

	/// <summary>Exchange ActiveSync through Office 365.</summary>
	[JsonStringEnumMemberName("activesync-office365")]
	ActiveSyncOffice365,

	/// <summary>Exchange ActiveSync through PowerShell.</summary>
	[JsonStringEnumMemberName("activesync-powershell")]
	ActiveSyncPowerShell,

	/// <summary>Amazon Web Services.</summary>
	[JsonStringEnumMemberName("aws")]
	Aws,

	/// <summary>DHCP service logs.</summary>
	[JsonStringEnumMemberName("dhcp")]
	Dhcp,

	/// <summary>Project Sonar.</summary>
	[JsonStringEnumMemberName("sonar")]
	Sonar,

	/// <summary>VMware vSphere.</summary>
	[JsonStringEnumMemberName("vsphere")]
	VSphere,
}
