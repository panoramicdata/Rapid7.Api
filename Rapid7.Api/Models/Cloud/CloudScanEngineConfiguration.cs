using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>The custom properties set on a scan engine.</summary>
public sealed class CloudScanEngineConfiguration
{
	/// <summary>When the platform last read the configuration from the scan engine.</summary>
	/// <remarks>
	/// The specification names this <c>lastRetrieved</c> while its examples use <c>last_retrieved</c>; either is read.
	/// </remarks>
	[JsonPropertyName("last_retrieved")]
	public DateTimeOffset? LastRetrieved { get; init; }

	/// <summary>The custom properties and their values, each as <c>name:value</c>.</summary>
	[JsonPropertyName("properties")]
	public IReadOnlyList<string> Properties { get; init; } = [];

	/// <summary>Reads the specification spelling of <see cref="LastRetrieved"/>; never written.</summary>
	[JsonInclude]
	[JsonPropertyName("lastRetrieved")]
	private DateTimeOffset? LastRetrievedCamelCase
	{
		get => null;
		init => LastRetrieved ??= value;
	}
}
