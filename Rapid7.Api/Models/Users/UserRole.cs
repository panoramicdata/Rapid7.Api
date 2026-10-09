using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Users;

/// <summary>The role a user holds and the privileges and access it gives them.</summary>
public sealed class UserRole : UserRoleBase
{
	/// <summary>The identifier of the role, such as <c>global-admin</c>.</summary>
	[JsonPropertyName("id")]
	public string? Id { get; init; }

	/// <summary>The display name of the role.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The privileges the role grants, such as <c>manage-sites</c>.</summary>
	[JsonPropertyName("privileges")]
	public IReadOnlyList<string> Privileges { get; init; } = [];
}
