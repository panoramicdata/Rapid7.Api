using Rapid7.Api.Models.BulkExport;
using Rapid7.Api.Serialization;

namespace Rapid7.Api.Test.Groups;

/// <summary>Edge cases of the Bulk Export record mapper and failure exception.</summary>
public class BulkExportEdgeTests
{
	[Fact]
	public void RecordMapper_SkipsNullValuesAndUnknownColumns_MatchingNamesIgnoringCase()
	{
		var record = ParquetRecordMapper<AssetSoftwareRecord>.Map(
		[
			new("PRODUCT", "Example Server"),
			new("vendor", null!),
			new("not_a_column", "ignored")
		]);

		record.Product.Should().Be("Example Server");
		record.Vendor.Should().BeNull();
	}

	[Theory]
	[InlineData("Export not found", new string[0], "Export not found")]
	[InlineData(null, new string[0], "(no message)")]
	[InlineData("Bad id", new[] { "export", "id" }, "Bad id (at export.id)")]
	[InlineData(null, new[] { "export" }, "(no message) (at export)")]
	public void GraphQLError_ToString_NamesTheMessageAndPath(string? message, string[] path, string expected)
		=> new GraphQLError { Message = message!, Path = path }.ToString().Should().Be(expected);

	[Fact]
	public void ExportFailedException_RequiresTheExport()
	{
		var act = () => new Rapid7ExportFailedException(null!);

		act.Should().Throw<ArgumentNullException>().WithParameterName("export");
	}
}
