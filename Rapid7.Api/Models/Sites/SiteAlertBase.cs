using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Sites;

/// <summary>
/// The settings every kind of site alert shares. The typed alerts (<see cref="SmtpAlert"/>, <see cref="SnmpAlert"/>,
/// <see cref="SyslogAlert"/>) are read, created and updated as they are; links are ignored when sent.
/// </summary>
public abstract class SiteAlertBase : Links
{
	/// <summary>Creates an alert delivered by <paramref name="notification"/>.</summary>
	/// <param name="notification">How the alert is delivered.</param>
	protected SiteAlertBase(AlertNotificationType notification) => Notification = notification;

	/// <summary>The alert identifier (assigned by the console; leave it unset when creating).</summary>
	[JsonPropertyName("id")]
	public int? Id { get; init; }

	/// <summary>The alert name (required).</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>Whether the alert is enabled.</summary>
	[JsonPropertyName("enabled")]
	public bool Enabled { get; init; }

	/// <summary>How the alert is delivered; the typed alerts set it for you.</summary>
	[JsonPropertyName("notification")]
	public AlertNotificationType Notification { get; init; }

	/// <summary>The most alerts to send for one scan, or <see langword="null"/> for no limit.</summary>
	[JsonPropertyName("maximumAlerts")]
	public int? MaximumAlerts { get; init; }

	/// <summary>The scan status changes to alert on.</summary>
	[JsonPropertyName("enabledScanEvents")]
	public AlertScanEvents? EnabledScanEvents { get; init; }

	/// <summary>The vulnerability findings to alert on.</summary>
	[JsonPropertyName("enabledVulnerabilityEvents")]
	public AlertVulnerabilityEvents? EnabledVulnerabilityEvents { get; init; }
}
