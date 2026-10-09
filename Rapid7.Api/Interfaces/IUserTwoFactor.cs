using Rapid7.Api.Models;
using Rapid7.Api.Models.Users;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// A user's two-factor authentication token seed (<c>api/3/users/{id}/2FA</c>). Every key read, set or generated here is a
/// secret: anyone holding it can produce the user's one-time codes. The client never logs request or response bodies.
/// </summary>
public interface IUserTwoFactor
{
	/// <summary>Gets the user's current token seed, if one is configured (<c>GET api/3/users/{id}/2FA</c>).</summary>
	/// <param name="id">The identifier of the user.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The key, a secret.</returns>
	[Get("api/3/users/{id}/2FA")]
	Task<TwoFactorKey> GetKeyAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Generates a new token seed for the user and makes it current (<c>POST api/3/users/{id}/2FA</c>). The user's existing
	/// authenticator stops working until the new key is enrolled.
	/// </summary>
	/// <param name="id">The identifier of the user.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The new key, a secret.</returns>
	[Post("api/3/users/{id}/2FA")]
	Task<TwoFactorKey> RegenerateKeyAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Sets the user's token seed (<c>PUT api/3/users/{id}/2FA</c>); the body is the key as a JSON string.
	/// </summary>
	/// <param name="id">The identifier of the user.</param>
	/// <param name="key">The token seed to use, a secret.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Related links.</returns>
	[Put("api/3/users/{id}/2FA")]
	Task<LinksResource> SetKeyAsync(int id, [Body(BodySerializationMethod.Serialized)] string key, CancellationToken cancellationToken);
}
