using System.Text.Json.Serialization;

namespace Rapid7.Api.Models.Reports;

/// <summary>How a risk trend aggregates the risk of sites, asset groups or tags.</summary>
public enum RiskTrendAggregate
{
	/// <summary>A value this library does not recognise.</summary>
	Unknown = 0,

	/// <summary>The average risk.</summary>
	[JsonStringEnumMemberName("average")]
	Average,

	/// <summary>The total risk.</summary>
	[JsonStringEnumMemberName("total")]
	Total
}
