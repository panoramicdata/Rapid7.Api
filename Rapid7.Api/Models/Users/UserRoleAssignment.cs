using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Users;

/// <summary>The role and access to give a created or updated user. Unset flags default to <see langword="false"/>.</summary>
public sealed class UserRoleAssignment : UserRoleBase
{
	/// <summary>The identifier of the role to assign, such as <c>global-admin</c> or <c>user</c> (see <c>IRoles</c>). Required.</summary>
	[JsonPropertyName("id")]
	public required string Id { get; init; }
}
