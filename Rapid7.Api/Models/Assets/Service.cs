using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Assets;

/// <summary>A service discovered on an asset, with what was enumerated through it.</summary>
public sealed class Service : LinksResource
{
	/// <summary>Settings enumerated on the service, as name and value pairs.</summary>
	[JsonPropertyName("configurations")]
	public IReadOnlyList<Configuration> Configurations { get; init; } = [];

	/// <summary>The databases enumerated through the service.</summary>
	[JsonPropertyName("databases")]
	public IReadOnlyList<Database> Databases { get; init; } = [];

	/// <summary>The family of the service.</summary>
	[JsonPropertyName("family")]
	public string? Family { get; init; }

	/// <summary>The name of the service, such as <c>CIFS Name Service</c>.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The network interface the service listens on, when known.</summary>
	[JsonPropertyName("nic")]
	public string? Nic { get; init; }

	/// <summary>The port of the service.</summary>
	[JsonPropertyName("port")]
	public int Port { get; init; }

	/// <summary>The product providing the service, such as <c>Samba</c>.</summary>
	[JsonPropertyName("product")]
	public string? Product { get; init; }

	/// <summary>The protocol of the service.</summary>
	[JsonPropertyName("protocol")]
	public ServiceProtocol Protocol { get; init; }

	/// <summary>The group accounts enumerated through the service.</summary>
	[JsonPropertyName("userGroups")]
	public IReadOnlyList<GroupAccount> UserGroups { get; init; } = [];

	/// <summary>The user accounts enumerated through the service.</summary>
	[JsonPropertyName("users")]
	public IReadOnlyList<UserAccount> Users { get; init; } = [];

	/// <summary>The vendor of the product providing the service.</summary>
	[JsonPropertyName("vendor")]
	public string? Vendor { get; init; }

	/// <summary>The version of the product providing the service.</summary>
	[JsonPropertyName("version")]
	public string? Version { get; init; }

	/// <summary>The web applications found on the service.</summary>
	[JsonPropertyName("webApplications")]
	public IReadOnlyList<WebApplication> WebApplications { get; init; } = [];
}
