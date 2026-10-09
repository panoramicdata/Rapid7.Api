using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Cloud;

/// <summary>How credentials fared on one port and protocol of an asset in its last scan.</summary>
public sealed class CloudCredentialAssessment
{
	/// <summary>The port the credentials were used on.</summary>
	[JsonPropertyName("port")]
	public long? Port { get; init; }

	/// <summary>The protocol the credentials were used on.</summary>
	[JsonPropertyName("protocol")]
	public string? Protocol { get; init; }

	/// <summary>The outcome of authenticating in the last scan.</summary>
	[JsonPropertyName("status")]
	public string? Status { get; init; }
}
