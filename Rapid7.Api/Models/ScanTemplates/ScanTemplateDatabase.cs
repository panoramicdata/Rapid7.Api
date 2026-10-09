using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.ScanTemplates;

/// <summary>The database names a scan template uses when checking database servers.</summary>
public sealed record ScanTemplateDatabase : ScanTemplateSection
{
	/// <summary>The DB2 database name.</summary>
	[JsonPropertyName("db2")]
	public string? Db2 { get; init; }

	/// <summary>The Oracle database names (SIDs) to try.</summary>
	[JsonPropertyName("oracle")]
	public IReadOnlyList<string>? Oracle { get; init; }

	/// <summary>The PostgreSQL database name.</summary>
	[JsonPropertyName("postgres")]
	public string? Postgres { get; init; }
}
