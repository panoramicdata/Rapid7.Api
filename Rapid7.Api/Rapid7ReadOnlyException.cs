namespace Rapid7.Api;

/// <summary>
/// Raised, before anything is sent, when a client created with a <c>ReadOnly</c> option is asked to make a request that
/// could change InsightVM.
/// </summary>
/// <param name="method">The refused HTTP method.</param>
/// <param name="path">The refused request path (without the query string).</param>
public sealed class Rapid7ReadOnlyException(string method, string path)
	: InvalidOperationException($"The Rapid7 client is read-only and refused {method} {path}.")
{
	/// <summary>The refused HTTP method.</summary>
	public string Method { get; } = method;

	/// <summary>The refused request path, without the query string.</summary>
	public string Path { get; } = path;
}
