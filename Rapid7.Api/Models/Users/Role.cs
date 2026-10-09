using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Users;

/// <summary>A role: a named set of privileges that users are assigned.</summary>
public sealed class Role : Links
{
	/// <summary>The identifier of the role, such as <c>global-admin</c>.</summary>
	[JsonPropertyName("id")]
	public string? Id { get; init; }

	/// <summary>The name of the role.</summary>
	[JsonPropertyName("name")]
	public LocalizedMessage? Name { get; init; }

	/// <summary>The description of the role.</summary>
	[JsonPropertyName("description")]
	public LocalizedMessage? Description { get; init; }

	/// <summary>The privileges the role grants, such as <c>manage-sites</c> (see <c>IPrivileges</c>).</summary>
	[JsonPropertyName("privileges")]
	public IReadOnlyList<string> Privileges { get; init; } = [];
}
