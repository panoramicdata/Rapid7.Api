using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>The database names a scan template connects to when checking database servers.</summary>
public sealed class ScanTemplateDatabase : Links
{
	/// <summary>The DB2 database name.</summary>
	[JsonPropertyName("db2")]
	public string? Db2 { get; init; }

	/// <summary>The Oracle service names.</summary>
	[JsonPropertyName("oracle")]
	public IReadOnlyList<string> Oracle { get; init; } = [];

	/// <summary>The PostgreSQL database name.</summary>
	[JsonPropertyName("postgres")]
	public string? Postgres { get; init; }
}
