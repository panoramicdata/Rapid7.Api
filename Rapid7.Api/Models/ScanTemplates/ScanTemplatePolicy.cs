using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>The policy assessment settings of a scan template.</summary>
public sealed record ScanTemplatePolicy : ScanTemplateSection
{
	/// <summary>The identifiers of the policies to assess; none by default.</summary>
	[JsonPropertyName("enabled")]
	public IReadOnlyList<long>? Enabled { get; init; }

	/// <summary>Whether file searches on Windows targets recurse into subfolders.</summary>
	[JsonPropertyName("recursiveWindowsFSSearch")]
	public bool? RecursiveWindowsFileSystemSearch { get; init; }

	/// <summary>Whether results are stored in Asset Reporting Format (ARF), for SCAP compliance reporting.</summary>
	[JsonPropertyName("storeSCAP")]
	public bool? StoreScap { get; init; }
}
