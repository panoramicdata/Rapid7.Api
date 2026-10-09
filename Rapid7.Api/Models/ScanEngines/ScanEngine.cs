using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanEngines;

/// <summary>A scan engine paired with the Security Console.</summary>
public sealed class ScanEngine : Links
{
	/// <summary>The scan engine identifier.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>The scan engine name.</summary>
	[JsonPropertyName("name")]
	public string Name { get; init; } = string.Empty;

	/// <summary>The host name or IP address the scan engine runs on.</summary>
	[JsonPropertyName("address")]
	public string Address { get; init; } = string.Empty;

	/// <summary>The port the scan engine and the Security Console communicate over.</summary>
	[JsonPropertyName("port")]
	public int Port { get; init; }

	/// <summary>The identifiers of the sites assigned to the scan engine.</summary>
	[JsonPropertyName("sites")]
	public IReadOnlyList<int> Sites { get; init; } = [];

	/// <summary>Whether the console can reach and use the scan engine.</summary>
	[JsonPropertyName("status")]
	public ScanEngineStatus? Status { get; init; }

	/// <summary>The version of the vulnerability content installed on the scan engine.</summary>
	[JsonPropertyName("contentVersion")]
	public string? ContentVersion { get; init; }

	/// <summary>The version of the scan engine software.</summary>
	[JsonPropertyName("productVersion")]
	public string? ProductVersion { get; init; }

	/// <summary>The scan engine serial number.</summary>
	[JsonPropertyName("serialNumber")]
	public string? SerialNumber { get; init; }

	/// <summary>Whether this is an engine pre-authorized for scanning in Amazon Web Services.</summary>
	[JsonPropertyName("isAWSPreAuthEngine")]
	public bool? IsAwsPreAuthorizedEngine { get; init; }

	/// <summary>When the scan engine was last refreshed.</summary>
	[JsonPropertyName("lastRefreshedDate")]
	public DateTimeOffset? LastRefreshedDate { get; init; }

	/// <summary>When the scan engine was last updated.</summary>
	[JsonPropertyName("lastUpdatedDate")]
	public DateTimeOffset? LastUpdatedDate { get; init; }
}
