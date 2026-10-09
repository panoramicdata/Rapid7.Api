using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>What a console command printed (<c>POST api/3/administration/commands</c>).</summary>
public sealed class ConsoleCommandOutput : LinksResource
{
	/// <summary>The text the command wrote.</summary>
	[JsonPropertyName("output")]
	public string? Output { get; init; }
}
