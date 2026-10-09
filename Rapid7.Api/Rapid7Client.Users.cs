using Rapid7.Api.Interfaces;

namespace Rapid7.Api;

public sealed partial class Rapid7Client
{
	/// <summary>User accounts: create, read, update, delete, unlock, passwords and privileges (<c>api/3/users</c>).</summary>
	public IUsers Users => field ??= For<IUsers>();

	/// <summary>Users' two-factor authentication token seeds, which are secrets (<c>api/3/users/{id}/2FA</c>).</summary>
	public IUserTwoFactor UserTwoFactor => field ??= For<IUserTwoFactor>();

	/// <summary>The sites and asset groups each user can access (<c>api/3/users/{id}/sites</c>, <c>api/3/users/{id}/asset_groups</c>).</summary>
	public IUserAccess UserAccess => field ??= For<IUserAccess>();

	/// <summary>Roles and the users assigned them (<c>api/3/roles</c>).</summary>
	public IRoles Roles => field ??= For<IRoles>();

	/// <summary>The privileges roles grant, and the users holding each (<c>api/3/privileges</c>).</summary>
	public IPrivileges Privileges => field ??= For<IPrivileges>();

	/// <summary>The sources that authenticate users (<c>api/3/authentication_sources</c>).</summary>
	public IAuthenticationSources AuthenticationSources => field ??= For<IAuthenticationSources>();
}
