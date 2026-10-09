using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Policies;

/// <summary>A NIST SP 800-53 control mapping of a CCE item in a policy rule.</summary>
public sealed class PolicyControl : LinksResource
{
	/// <summary>The identifier of the control, as text.</summary>
	[JsonPropertyName("id")]
	public string? Id { get; init; }

	/// <summary>The name of the control mapping.</summary>
	[JsonPropertyName("controlName")]
	public string? ControlName { get; init; }

	/// <summary>The identifier of the CCE item.</summary>
	[JsonPropertyName("cceItemId")]
	public string? CceItemId { get; init; }

	/// <summary>The CCE platform.</summary>
	[JsonPropertyName("ccePlatform")]
	public string? CcePlatform { get; init; }

	/// <summary>When the control mapping was published, as the console reports it (a number).</summary>
	[JsonPropertyName("publishedDate")]
	public long? PublishedDate { get; init; }
}
