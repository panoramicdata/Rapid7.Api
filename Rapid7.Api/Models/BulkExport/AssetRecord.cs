using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.BulkExport;

/// <summary>A row of the <c>asset</c> dataset: an asset, its identifiers in cloud providers, its operating system and groupings.</summary>
public sealed class AssetRecord : BulkExportRecord
{
	/// <summary>The Insight Agent identifier (<c>agentId</c>), when the asset runs one.</summary>
	[JsonPropertyName("agentId")]
	public string? AgentId { get; init; }

	/// <summary>The AWS instance identifier (<c>awsInstanceId</c>), when applicable.</summary>
	[JsonPropertyName("awsInstanceId")]
	public string? AwsInstanceId { get; init; }

	/// <summary>The Azure resource identifier (<c>azureResourceId</c>), when applicable.</summary>
	[JsonPropertyName("azureResourceId")]
	public string? AzureResourceId { get; init; }

	/// <summary>The Google Cloud object identifier (<c>gcpObjectId</c>), when applicable.</summary>
	[JsonPropertyName("gcpObjectId")]
	public string? GcpObjectId { get; init; }

	/// <summary>The primary MAC address (<c>mac</c>).</summary>
	[JsonPropertyName("mac")]
	public string? Mac { get; init; }

	/// <summary>The primary IP address (<c>ip</c>).</summary>
	[JsonPropertyName("ip")]
	public string? Ip { get; init; }

	/// <summary>The primary host name (<c>hostName</c>).</summary>
	[JsonPropertyName("hostName")]
	public string? HostName { get; init; }

	/// <summary>The operating system architecture (<c>osArchitecture</c>).</summary>
	[JsonPropertyName("osArchitecture")]
	public string? OsArchitecture { get; init; }

	/// <summary>The operating system family (<c>osFamily</c>).</summary>
	[JsonPropertyName("osFamily")]
	public string? OsFamily { get; init; }

	/// <summary>The operating system product (<c>osProduct</c>).</summary>
	[JsonPropertyName("osProduct")]
	public string? OsProduct { get; init; }

	/// <summary>The operating system vendor (<c>osVendor</c>).</summary>
	[JsonPropertyName("osVendor")]
	public string? OsVendor { get; init; }

	/// <summary>The operating system version (<c>osVersion</c>).</summary>
	[JsonPropertyName("osVersion")]
	public string? OsVersion { get; init; }

	/// <summary>The operating system type (<c>osType</c>).</summary>
	[JsonPropertyName("osType")]
	public string? OsType { get; init; }

	/// <summary>The full operating system description (<c>osDescription</c>).</summary>
	[JsonPropertyName("osDescription")]
	public string? OsDescription { get; init; }

	/// <summary>The asset risk score (<c>riskScore</c>).</summary>
	[JsonPropertyName("riskScore")]
	public double? RiskScore { get; init; }

	/// <summary>The sites the asset belongs to (<c>sites</c>).</summary>
	[JsonPropertyName("sites")]
	public IReadOnlyList<string> Sites { get; init; } = [];

	/// <summary>The asset groups the asset belongs to (<c>assetGroups</c>).</summary>
	[JsonPropertyName("assetGroups")]
	public IReadOnlyList<string> AssetGroups { get; init; } = [];

	/// <summary>The tags on the asset (<c>tags</c>).</summary>
	[JsonPropertyName("tags")]
	public IReadOnlyList<string> Tags { get; init; } = [];
}
