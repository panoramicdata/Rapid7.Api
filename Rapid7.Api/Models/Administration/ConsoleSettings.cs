using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The console's administration settings (<c>GET api/3/administration/settings</c>).</summary>
public sealed class ConsoleSettings : Links
{
	/// <summary>The unique identifier (UUID) of the console.</summary>
	[JsonPropertyName("uuid")]
	public string? Uuid { get; init; }

	/// <summary>The serial number of the console.</summary>
	[JsonPropertyName("serialNumber")]
	public string? SerialNumber { get; init; }

	/// <summary>The installation root directory.</summary>
	[JsonPropertyName("directory")]
	public string? Directory { get; init; }

	/// <summary>Whether asset linking is enabled.</summary>
	[JsonPropertyName("assetLinking")]
	public bool? AssetLinking { get; init; }

	/// <summary>Whether the Insight platform is enabled.</summary>
	[JsonPropertyName("insightPlatform")]
	public bool? InsightPlatform { get; init; }

	/// <summary>The Insight platform region, when enabled.</summary>
	[JsonPropertyName("insightPlatformRegion")]
	public string? InsightPlatformRegion { get; init; }

	/// <summary>The sign-in settings.</summary>
	[JsonPropertyName("authentication")]
	public AuthenticationSettings? Authentication { get; init; }

	/// <summary>The database settings.</summary>
	[JsonPropertyName("database")]
	public DatabaseSettings? Database { get; init; }

	/// <summary>The risk scoring settings.</summary>
	[JsonPropertyName("risk")]
	public RiskSettings? Risk { get; init; }

	/// <summary>The global scan settings.</summary>
	[JsonPropertyName("scan")]
	public GlobalScanSettings? Scan { get; init; }

	/// <summary>The email (SMTP) settings.</summary>
	[JsonPropertyName("smtp")]
	public SmtpSettings? Smtp { get; init; }

	/// <summary>The product and content update settings.</summary>
	[JsonPropertyName("updates")]
	public UpdateSettings? Updates { get; init; }

	/// <summary>The web server settings.</summary>
	[JsonPropertyName("web")]
	public WebSettings? Web { get; init; }
}
