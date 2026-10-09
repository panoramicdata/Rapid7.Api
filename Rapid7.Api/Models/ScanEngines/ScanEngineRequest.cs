using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanEngines;

/// <summary>The settings of a scan engine to create or update.</summary>
public sealed class ScanEngineRequest
{
	/// <summary>The scan engine name.</summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>The host name or IP address of the scan engine.</summary>
	[JsonPropertyName("address")]
	public required string Address { get; init; }

	/// <summary>The port the scan engine listens on for the console (40814 by default).</summary>
	[JsonPropertyName("port")]
	public required int Port { get; init; }

	/// <summary>The identifiers of the sites to assign to the scan engine, or <see langword="null"/> to leave them out.</summary>
	[JsonPropertyName("sites")]
	public IReadOnlyList<int>? Sites { get; init; }
}
