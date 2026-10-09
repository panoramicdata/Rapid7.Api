using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.AssetDiscovery;

/// <summary>
/// A discovery connection: a link to an external source (such as VMware vSphere, AWS, Active Directory, DHCP or an
/// Exchange server) that the console discovers assets through. Which properties are set depends on the connection type.
/// </summary>
public sealed class DiscoveryConnection : LinksResource
{
	/// <summary>The AWS access key identifier, for an AWS connection.</summary>
	[JsonPropertyName("accessKeyId")]
	public string? AccessKeyId { get; init; }

	/// <summary>The address the console connects to.</summary>
	[JsonPropertyName("address")]
	public string? Address { get; init; }

	/// <summary>The AWS role ARN, for an AWS connection.</summary>
	[JsonPropertyName("arn")]
	public string? Arn { get; init; }

	/// <summary>The AWS session name, for an AWS connection.</summary>
	[JsonPropertyName("awsSessionName")]
	public string? AwsSessionName { get; init; }

	/// <summary>The type of the connection (the source it discovers assets from).</summary>
	[JsonPropertyName("connectionType")]
	public string? ConnectionType { get; init; }

	/// <summary>The event source type, for a DHCP connection.</summary>
	[JsonPropertyName("eventSource")]
	public string? EventSource { get; init; }

	/// <summary>The Exchange server host name, for an Exchange connection.</summary>
	[JsonPropertyName("exchangeServerHostname")]
	public string? ExchangeServerHostname { get; init; }

	/// <summary>The Exchange user name, for an Exchange connection.</summary>
	[JsonPropertyName("exchangeUser")]
	public string? ExchangeUser { get; init; }

	/// <summary>The folder logs are read from, for a DHCP connection.</summary>
	[JsonPropertyName("folderPath")]
	public string? FolderPath { get; init; }

	/// <summary>The identifier of the connection.</summary>
	[JsonPropertyName("id")]
	public long? Id { get; init; }

	/// <summary>The LDAP server, for a connection that reads a directory.</summary>
	[JsonPropertyName("ldapServer")]
	public string? LdapServer { get; init; }

	/// <summary>The name of the connection.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The port the console connects to.</summary>
	[JsonPropertyName("port")]
	public int? Port { get; init; }

	/// <summary>The protocol the console connects with, such as <c>https</c>.</summary>
	[JsonPropertyName("protocol")]
	public string? Protocol { get; init; }

	/// <summary>The AWS region, for an AWS connection.</summary>
	[JsonPropertyName("region")]
	public string? Region { get; init; }

	/// <summary>Whether the scan engine runs inside AWS, for an AWS connection.</summary>
	[JsonPropertyName("scanEngineIsInsideAWS")]
	public bool? ScanEngineIsInsideAws { get; init; }

	/// <summary>The AWS secret access key, for an AWS connection (the console normally withholds it).</summary>
	[JsonPropertyName("secretAccessKey")]
	public string? SecretAccessKey { get; init; }

	/// <summary>The status of the connection, such as <c>connected</c>.</summary>
	[JsonPropertyName("status")]
	public string? Status { get; init; }

	/// <summary>The user name the console authenticates with.</summary>
	[JsonPropertyName("username")]
	public string? Username { get; init; }

	/// <summary>The WinRM server, for a connection that reads Windows event logs.</summary>
	[JsonPropertyName("winRMServer")]
	public string? WinRmServer { get; init; }
}
