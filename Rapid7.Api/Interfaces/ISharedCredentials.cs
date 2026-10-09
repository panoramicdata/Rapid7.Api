using Rapid7.Api.Models;
using Rapid7.Api.Models.Credentials;
using Refit;

namespace Rapid7.Api.Interfaces;

/// <summary>
/// Shared scan credentials (<c>api/3/shared_credentials</c>), which sites use to authenticate to the assets they scan.
/// Requests carry passwords, hashes and private keys; the console never returns them and the client never logs bodies.
/// </summary>
public interface ISharedCredentials
{
	/// <summary>Lists every shared credential (<c>GET api/3/shared_credentials</c>).</summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The credentials, without their secrets.</returns>
	[Get("api/3/shared_credentials")]
	Task<ResourceList<SharedCredential>> ListAsync(CancellationToken cancellationToken);

	/// <summary>Creates a shared credential (<c>POST api/3/shared_credentials</c>).</summary>
	/// <param name="request">The credential, including its secrets.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The identifier of the new credential.</returns>
	[Post("api/3/shared_credentials")]
	Task<CreatedReference<int>> CreateAsync([Body] SharedCredentialRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes <b>every</b> shared credential on the console (<c>DELETE api/3/shared_credentials</c>). Sites that use them can
	/// no longer authenticate; this cannot be undone.
	/// </summary>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Related links.</returns>
	[Delete("api/3/shared_credentials")]
	Task<Links> DeleteAllAsync(CancellationToken cancellationToken);

	/// <summary>Gets a shared credential (<c>GET api/3/shared_credentials/{id}</c>).</summary>
	/// <param name="id">The identifier of the credential.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The credential, without its secrets.</returns>
	[Get("api/3/shared_credentials/{id}")]
	Task<SharedCredential> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>Replaces a shared credential (<c>PUT api/3/shared_credentials/{id}</c>).</summary>
	/// <param name="id">The identifier of the credential.</param>
	/// <param name="request">The new credential, including its secrets.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Links to the credential.</returns>
	[Put("api/3/shared_credentials/{id}")]
	Task<Links> UpdateAsync(int id, [Body] SharedCredentialRequest request, CancellationToken cancellationToken);

	/// <summary>Deletes a shared credential (<c>DELETE api/3/shared_credentials/{id}</c>).</summary>
	/// <param name="id">The identifier of the credential.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>Related links.</returns>
	[Delete("api/3/shared_credentials/{id}")]
	Task<Links> DeleteAsync(int id, CancellationToken cancellationToken);
}
