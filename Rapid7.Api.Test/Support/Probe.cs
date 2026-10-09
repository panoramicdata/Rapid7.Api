using Rapid7.Api.Models;
using Refit;
using System.Text.Json.Serialization;

namespace Rapid7.Api.Test.Support;

/// <summary>A test-only endpoint group for exercising the core through Refit, independent of any real category.</summary>
public interface IProbe
{
	[Get("api/3/probe")]
	Task<Page<Probe>> ListAsync([Query] PageOptions? options, CancellationToken cancellationToken);

	[Post("api/3/probe/{name}")]
	Task<CreatedReference<int>> CreateAsync(string name, [Body] Probe body, CancellationToken cancellationToken);

	[Put("api/3/probe/{name}")]
	Task<Links> UpdateAsync(string name, [Body] Probe body, CancellationToken cancellationToken);

	[Get("api/3/probe/{name}")]
	[Headers("Accept: text/csv")]
	Task<string> DownloadAsync(string name, CancellationToken cancellationToken);
}

/// <summary>The body and resource of <see cref="IProbe"/>.</summary>
public sealed class Probe
{
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	[JsonPropertyName("enabled")]
	public bool? Enabled { get; init; }

	[JsonPropertyName("note")]
	public string? Note { get; init; }
}
