using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>The settings of a report configuration: what it reports on, in which format and template, when it is generated and who receives it. Send it to create or update a report.</summary>
/// <remarks>Leave a property <see langword="null"/> to omit it. Which properties apply depends on <see cref="Format"/> and <see cref="Template"/>.</remarks>
public class ReportConfiguration
{
	/// <summary>The name of the report.</summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>The output format, which limits the templates and settings that apply.</summary>
	[JsonPropertyName("format")]
	public ReportFormat? Format { get; init; }

	/// <summary>The identifier of the report template, for templatised formats (for example <c>executive-overview</c>).</summary>
	[JsonPropertyName("template")]
	public string? Template { get; init; }

	/// <summary>The identifier of the user who owns the report.</summary>
	[JsonPropertyName("owner")]
	public int? Owner { get; init; }

	/// <summary>The locale the report is generated in, such as <c>en-US</c>.</summary>
	[JsonPropertyName("language")]
	public string? Language { get; init; }

	/// <summary>The time zone the report is generated in, such as <c>America/Los_Angeles</c>.</summary>
	[JsonPropertyName("timezone")]
	public string? Timezone { get; init; }

	/// <summary>For the baseline comparison and executive overview templates, the scan to compare against: <c>first</c>, <c>previous</c>, or a date.</summary>
	[JsonPropertyName("baseline")]
	public string? Baseline { get; init; }

	/// <summary>For CyberScope XML, the bureau name.</summary>
	[JsonPropertyName("bureau")]
	public string? Bureau { get; init; }

	/// <summary>For CyberScope XML, the component name.</summary>
	[JsonPropertyName("component")]
	public string? Component { get; init; }

	/// <summary>For CyberScope XML, the enclave name.</summary>
	[JsonPropertyName("enclave")]
	public string? Enclave { get; init; }

	/// <summary>For XCCDF XML, the organization name.</summary>
	[JsonPropertyName("organization")]
	public string? Organization { get; init; }

	/// <summary>For the SQL query format, the query to run against the reporting data model.</summary>
	[JsonPropertyName("query")]
	public string? Query { get; init; }

	/// <summary>For the SQL query format, the version of the reporting data model.</summary>
	[JsonPropertyName("version")]
	public string? Version { get; init; }

	/// <summary>For policy templates that report on one policy, the identifier of the policy.</summary>
	[JsonPropertyName("policy")]
	public long? Policy { get; init; }

	/// <summary>For the rule breakdown and top policy remediation templates, the identifiers of the policies.</summary>
	[JsonPropertyName("policies")]
	public IReadOnlyList<long>? Policies { get; init; }

	/// <summary>The identifiers of the users explicitly granted access to the report.</summary>
	[JsonPropertyName("users")]
	public IReadOnlyList<int>? Users { get; init; }

	/// <summary>The assets, sites, groups, tags or scan the report covers.</summary>
	[JsonPropertyName("scope")]
	public ReportScope? Scope { get; init; }

	/// <summary>Which vulnerability findings the report includes.</summary>
	[JsonPropertyName("filters")]
	public ReportFilters? Filters { get; init; }

	/// <summary>When the report is generated.</summary>
	[JsonPropertyName("frequency")]
	public ReportFrequency? Frequency { get; init; }

	/// <summary>Who receives the report by email, and how.</summary>
	[JsonPropertyName("email")]
	public ReportEmail? Email { get; init; }

	/// <summary>Where an extra copy of the report is stored.</summary>
	[JsonPropertyName("storage")]
	public ReportStorage? Storage { get; init; }

	/// <summary>For trend templates, the date range covered.</summary>
	[JsonPropertyName("range")]
	public ReportRange? Range { get; init; }

	/// <summary>For remediation templates, how solutions are listed.</summary>
	[JsonPropertyName("remediation")]
	public ReportRemediation? Remediation { get; init; }

	/// <summary>For the risk trend template, which trends are shown.</summary>
	[JsonPropertyName("riskTrend")]
	public ReportRiskTrend? RiskTrend { get; init; }
}
