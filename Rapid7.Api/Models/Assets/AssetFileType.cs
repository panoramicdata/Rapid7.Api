using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>Whether a discovered file system entry is a file or a directory.</summary>
public enum AssetFileType
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>A regular file.</summary>
	[JsonStringEnumMemberName("file")]
	File,

	/// <summary>A directory (or a share).</summary>
	[JsonStringEnumMemberName("directory")]
	Directory
}
