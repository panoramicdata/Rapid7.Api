using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The database connection pool settings; <c>-1</c> means unlimited.</summary>
public sealed class DatabaseConnectionSettings
{
	/// <summary>The most connections in the pool.</summary>
	[JsonPropertyName("maximumPoolSize")]
	public int? MaximumPoolSize { get; init; }

	/// <summary>The most administrative connections in the pool.</summary>
	[JsonPropertyName("maximumAdministrationPoolSize")]
	public int? MaximumAdministrationPoolSize { get; init; }

	/// <summary>The most prepared statements in the pool.</summary>
	[JsonPropertyName("maximumPreparedStatementPoolSize")]
	public int? MaximumPreparedStatementPoolSize { get; init; }
}
