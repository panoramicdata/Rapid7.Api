using System.Text.RegularExpressions;

namespace Rapid7.Api;

/// <summary>
/// What <see cref="Rapid7Client"/>, <see cref="Rapid7CloudClient"/> and <see cref="Rapid7BulkExportClient"/> share: the
/// address requests go to and the HTTP pipeline (read-only guard, authentication, retries) they go through.
/// </summary>
/// <remarks>Only the clients in this library derive from it.</remarks>
public class Rapid7ClientBase : IDisposable
{
	/// <summary>Builds the pipeline over <paramref name="innerHandler"/>, which the client takes ownership of.</summary>
	/// <param name="options">Validated connection options.</param>
	/// <param name="baseAddress">The address endpoint paths are appended to, ending in <c>/</c>.</param>
	/// <param name="readOnlyPosts">The POST paths that only read, allowed when the options make the client read-only.</param>
	/// <param name="innerHandler">The handler that sends requests to the network.</param>
	private protected Rapid7ClientBase(
		Rapid7ConnectionOptions options,
		Uri baseAddress,
		Regex readOnlyPosts,
		HttpMessageHandler innerHandler)
	{
		ArgumentNullException.ThrowIfNull(innerHandler);
		BaseAddress = baseAddress;
		Core = options.CreateCore(baseAddress, readOnlyPosts, innerHandler);
	}

	/// <summary>The address every endpoint path is appended to, always ending in <c>/</c>.</summary>
	public Uri BaseAddress { get; }

	/// <summary>The pipeline and the Refit endpoint groups created over it.</summary>
	private protected Rapid7ClientCore Core { get; }

	/// <summary>Creates an endpoint group.</summary>
	internal virtual T For<T>() => Core.For<T>();

	/// <inheritdoc />
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	/// <summary>Releases the pipeline and the network handler under it.</summary>
	/// <param name="disposing">
	/// <see langword="true"/> when called from <see cref="Dispose()"/>; the clients have no finalizer, so it always is.
	/// </param>
	protected virtual void Dispose(bool disposing) => Core.Dispose();
}
