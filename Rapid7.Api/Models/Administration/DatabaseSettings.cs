using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The console's database settings.</summary>
public sealed class DatabaseSettings
{
	/// <summary>The database vendor, such as <c>postgresql</c>.</summary>
	[JsonPropertyName("vendor")]
	public string? Vendor { get; init; }

	/// <summary>The database host.</summary>
	[JsonPropertyName("host")]
	public string? Host { get; init; }

	/// <summary>The database port.</summary>
	[JsonPropertyName("port")]
	public int? Port { get; init; }

	/// <summary>The database connection URL.</summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }

	/// <summary>The database user.</summary>
	[JsonPropertyName("user")]
	public string? User { get; init; }

	/// <summary>The most maintenance tasks run in parallel.</summary>
	[JsonPropertyName("maintenanceThreadPoolSize")]
	public int? MaintenanceThreadPoolSize { get; init; }

	/// <summary>The connection pool settings.</summary>
	[JsonPropertyName("connection")]
	public DatabaseConnectionSettings? Connection { get; init; }
}
