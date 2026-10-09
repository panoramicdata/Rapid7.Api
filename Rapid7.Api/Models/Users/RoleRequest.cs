using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Users;

/// <summary>The new details of a role (<c>PUT api/3/roles/{id}</c>).</summary>
public sealed class RoleRequest
{
	/// <summary>The identifier of the role, matching the one in the path. Required.</summary>
	[JsonPropertyName("id")]
	public required string Id { get; init; }

	/// <summary>The name of the role. Required.</summary>
	[JsonPropertyName("name")]
	public required LocalizedMessage Name { get; init; }

	/// <summary>The description of the role. Required.</summary>
	[JsonPropertyName("description")]
	public required LocalizedMessage Description { get; init; }

	/// <summary>The privileges the role grants, such as <c>manage-sites</c>.</summary>
	[JsonPropertyName("privileges")]
	public IReadOnlyList<string>? Privileges { get; init; }
}
