using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Users;

/// <summary>The access flags shared by a user's role as read (<see cref="UserRole"/>) and as assigned (<see cref="UserRoleAssignment"/>).</summary>
public abstract class UserRoleBase
{
	/// <summary>Whether the user can access every asset group (individual grants are then ignored).</summary>
	[JsonPropertyName("allAssetGroups")]
	public bool? AllAssetGroups { get; init; }

	/// <summary>Whether the user can access every site (individual grants are then ignored).</summary>
	[JsonPropertyName("allSites")]
	public bool? AllSites { get; init; }

	/// <summary>Whether the user is a superuser.</summary>
	[JsonPropertyName("superuser")]
	public bool? Superuser { get; init; }
}
