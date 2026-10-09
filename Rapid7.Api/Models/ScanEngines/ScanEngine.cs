using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanEngines;

/// <summary>A scan engine: the component that scans assets on the console's behalf.</summary>
public sealed class ScanEngine : Links
{
	/// <summary>The scan engine identifier.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>The scan engine name.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The engine's host name or IP address.</summary>
	[JsonPropertyName("address")]
	public string? Address { get; init; }

	/// <summary>The port the engine listens on.</summary>
	[JsonPropertyName("port")]
	public int Port { get; init; }

	/// <summary>The engine's state.</summary>
	[JsonPropertyName("status")]
	public ScanEngineStatus? Status { get; init; }

	/// <summary>The engine's product version.</summary>
	[JsonPropertyName("productVersion")]
	public string? ProductVersion { get; init; }

	/// <summary>The version of the vulnerability content the engine has.</summary>
	[JsonPropertyName("contentVersion")]
	public string? ContentVersion { get; init; }

	/// <summary>The engine's serial number.</summary>
	[JsonPropertyName("serialNumber")]
	public string? SerialNumber { get; init; }

	/// <summary>Whether the engine is pre-authorized for scanning in Amazon Web Services.</summary>
	[JsonPropertyName("isAWSPreAuthEngine")]
	public bool? IsAwsPreAuthEngine { get; init; }

	/// <summary>When the console last refreshed its information about the engine.</summary>
	[JsonPropertyName("lastRefreshedDate")]
	public DateTimeOffset? LastRefreshedDate { get; init; }

	/// <summary>When the engine was last updated.</summary>
	[JsonPropertyName("lastUpdatedDate")]
	public DateTimeOffset? LastUpdatedDate { get; init; }

	/// <summary>The identifiers of the sites the engine scans.</summary>
	[JsonPropertyName("sites")]
	public IReadOnlyList<int> Sites { get; init; } = [];
}
