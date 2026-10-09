namespace Rapid7.Api;

/// <summary>Helpers for the path part of URLs.</summary>
internal static class UriPaths
{
	/// <summary>The URL path segment separator.</summary>
	private const char Separator = '/';

	/// <summary><paramref name="path"/> ending in exactly one more <c>/</c> if it did not already end in one.</summary>
	internal static string WithTrailingSlash(string path) => path.EndsWith(Separator) ? path : path + Separator;
}
