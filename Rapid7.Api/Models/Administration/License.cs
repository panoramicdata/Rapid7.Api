using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The console licence: its status, expiry, features and limits (<c>GET api/3/administration/license</c>).</summary>
public sealed class License : Links
{
	/// <summary>The status of the licence.</summary>
	[JsonPropertyName("status")]
	public LicenseStatus? Status { get; init; }

	/// <summary>The product edition, such as <c>InsightVM</c>.</summary>
	[JsonPropertyName("edition")]
	public string? Edition { get; init; }

	/// <summary>Whether this is a time-limited evaluation licence.</summary>
	[JsonPropertyName("evaluation")]
	public bool? Evaluation { get; init; }

	/// <summary>Whether the licence never expires.</summary>
	[JsonPropertyName("perpetual")]
	public bool? Perpetual { get; init; }

	/// <summary>When the licence expires.</summary>
	[JsonPropertyName("expires")]
	public DateTimeOffset? Expires { get; init; }

	/// <summary>The features the licence enables.</summary>
	[JsonPropertyName("features")]
	public LicenseFeatures? Features { get; init; }

	/// <summary>The limits the licence sets.</summary>
	[JsonPropertyName("limits")]
	public LicenseLimits? Limits { get; init; }
}
