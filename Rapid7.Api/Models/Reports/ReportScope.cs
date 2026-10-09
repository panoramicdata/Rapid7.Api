using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>What a report covers. Give one kind of scope.</summary>
public sealed class ReportScope
{
	/// <summary>The identifiers of the assets.</summary>
	[JsonPropertyName("assets")]
	public IReadOnlyList<long>? Assets { get; init; }

	/// <summary>The identifiers of the sites.</summary>
	[JsonPropertyName("sites")]
	public IReadOnlyList<int>? Sites { get; init; }

	/// <summary>The identifiers of the asset groups.</summary>
	[JsonPropertyName("assetGroups")]
	public IReadOnlyList<int>? AssetGroups { get; init; }

	/// <summary>The identifiers of the tags.</summary>
	[JsonPropertyName("tags")]
	public IReadOnlyList<int>? Tags { get; init; }

	/// <summary>The identifier of a scan.</summary>
	[JsonPropertyName("scan")]
	public long? Scan { get; init; }
}
