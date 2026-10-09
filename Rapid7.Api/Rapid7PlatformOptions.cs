namespace Rapid7.Api;

/// <summary>
/// Configuration for the Insight platform clients: <see cref="Rapid7CloudClient"/> (the InsightVM Cloud Integrations API
/// v4) and <see cref="Rapid7BulkExportClient"/> (the Bulk Export GraphQL API). The values are read once when a client is
/// constructed; changing this object afterwards does not affect an existing client.
/// </summary>
public class Rapid7PlatformOptions : Rapid7ConnectionOptions
{
	/// <summary>The Insight platform regions, as used in host names such as <c>us.api.insight.rapid7.com</c>.</summary>
	public static IReadOnlyList<string> Regions { get; } = ["us", "us2", "us3", "eu", "ca", "au", "ap"];

	/// <summary>
	/// The region of your Insight platform organisation: one of <see cref="Regions"/> (<c>us</c>, <c>us2</c>, <c>us3</c>,
	/// <c>eu</c>, <c>ca</c>, <c>au</c> or <c>ap</c>). Ignored when <see cref="BaseUrl"/> is set.
	/// </summary>
	public string Region { get; set; } = string.Empty;

	/// <summary>
	/// Overrides the platform address derived from <see cref="Region"/>, e.g. <c>https://us.api.insight.rapid7.com</c>
	/// (for a proxy, or a region added after this release). Must not carry credentials, a query or a fragment.
	/// </summary>
	public string? BaseUrl { get; set; }

	/// <summary>
	/// An Insight platform organisation API key, or a user API key with the permissions the API needs (Bulk Export needs
	/// Platform Administrator), sent as <c>X-Api-Key</c>.
	/// </summary>
	public string ApiKey { get; set; } = string.Empty;

	/// <summary>
	/// When <see langword="true"/>, the clients refuse (with <see cref="Rapid7ReadOnlyException"/>, before anything is
	/// sent) every request that could change InsightVM: starting or stopping scans, changing or removing scan engine
	/// configuration, and any other PUT or DELETE. Searches and exports, which only read, are allowed.
	/// </summary>
	public bool ReadOnly { get; set; }

	/// <summary>The platform address every API is reached through, always ending in <c>/</c>.</summary>
	internal Uri PlatformAddress
		=> new(string.IsNullOrWhiteSpace(BaseUrl)
			? $"https://{Region}.api.insight.rapid7.com/"
			: BaseUrl.EndsWith('/') ? BaseUrl : BaseUrl + "/");

	internal void Validate()
	{
		if (string.IsNullOrWhiteSpace(BaseUrl))
		{
			if (!Regions.Contains(Region, StringComparer.Ordinal))
			{
				throw new ArgumentException($"Region must be one of {string.Join(", ", Regions)}, or set BaseUrl.", nameof(Region));
			}
		}
		else
		{
			ValidateBaseUrl(BaseUrl, nameof(BaseUrl));
		}

		if (string.IsNullOrWhiteSpace(ApiKey))
		{
			throw new ArgumentException("Set ApiKey to an Insight platform API key.", nameof(ApiKey));
		}

		ValidateConnection();
	}

	/// <inheritdoc />
	public override string ToString()
		=> $"Rapid7PlatformOptions {{ Region = {Region}, BaseUrl = {MaskBaseUrl(BaseUrl)}, ApiKey = {Mask(ApiKey)}, ReadOnly = {ReadOnly} }}";
}
