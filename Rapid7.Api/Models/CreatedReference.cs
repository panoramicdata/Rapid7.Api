using System.Text.Json.Serialization;

namespace Rapid7.Api.Models;

/// <summary>The answer to a create (<c>POST</c>): the new resource's identifier and links to it.</summary>
/// <typeparam name="TId">The identifier type (an integer for most console resources, a string for some).</typeparam>
public sealed class CreatedReference<TId> : Links
{
	/// <summary>The identifier of the created resource.</summary>
	[JsonPropertyName("id")]
	public TId? Id { get; init; }
}
