using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>The policy checks a scan template runs (the spec's <c>Policy</c> schema within a scan template).</summary>
public sealed class ScanTemplatePolicy : Links
{
	/// <summary>The identifiers of the enabled policies.</summary>
	[JsonPropertyName("enabled")]
	public IReadOnlyList<long> Enabled { get; init; } = [];

	/// <summary>Whether to search Windows file systems recursively.</summary>
	[JsonPropertyName("recursiveWindowsFSSearch")]
	public bool? RecursiveWindowsFileSystemSearch { get; init; }

	/// <summary>Whether to store SCAP data.</summary>
	[JsonPropertyName("storeSCAP")]
	public bool? StoreScap { get; init; }
}
