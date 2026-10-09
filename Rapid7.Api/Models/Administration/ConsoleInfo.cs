using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Administration;

/// <summary>The Security Console host and installation: CPU, memory, disk, JVM and version (<c>GET api/3/administration/info</c>).</summary>
public sealed class ConsoleInfo : Links
{
	/// <summary>The local host name of the console.</summary>
	[JsonPropertyName("host")]
	public string? Host { get; init; }

	/// <summary>The fully qualified domain name of the console host.</summary>
	[JsonPropertyName("fqdn")]
	public string? Fqdn { get; init; }

	/// <summary>The IP address of the console host.</summary>
	[JsonPropertyName("ip")]
	public string? Ip { get; init; }

	/// <summary>The operating system of the console host.</summary>
	[JsonPropertyName("operatingSystem")]
	public string? OperatingSystem { get; init; }

	/// <summary>The operating system account the console service runs as.</summary>
	[JsonPropertyName("user")]
	public string? User { get; init; }

	/// <summary>Whether the service runs as a superuser.</summary>
	[JsonPropertyName("superuser")]
	public bool? Superuser { get; init; }

	/// <summary>The serial number of the console.</summary>
	[JsonPropertyName("serial")]
	public string? Serial { get; init; }

	/// <summary>The distinguished name of the console (as in its certificate).</summary>
	[JsonPropertyName("distinguishedName")]
	public string? DistinguishedName { get; init; }

	/// <summary>The host processors.</summary>
	[JsonPropertyName("cpu")]
	public CpuInfo? Cpu { get; init; }

	/// <summary>The host memory.</summary>
	[JsonPropertyName("memory")]
	public MemoryInfo? Memory { get; init; }

	/// <summary>The host disk and the space the installation uses.</summary>
	[JsonPropertyName("disk")]
	public DiskInfo? Disk { get; init; }

	/// <summary>The Java virtual machine running the console.</summary>
	[JsonPropertyName("jvm")]
	public JvmInfo? Jvm { get; init; }

	/// <summary>The installed version and updates.</summary>
	[JsonPropertyName("version")]
	public VersionInfo? Version { get; init; }
}
