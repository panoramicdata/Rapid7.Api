using Rapid7.Api.Models.BulkExport;
using Rapid7.Api.Test.Support;

namespace Rapid7.Api.Test.Groups;

/// <summary><see cref="Rapid7BulkExportClient"/> reading export files as records.</summary>
public class BulkExportRecordsTests
{
	private static Export ExportWith(params ExportResult[] results)
		=> new() { Id = BulkExportJson.ExportId, Status = ExportStatus.Succeeded, Result = results };

	private static ExportResult Result(string prefix, params string[] urls)
		=> new() { Prefix = prefix, Urls = [.. urls.Select(u => new Uri(u))] };

	private static async Task<List<T>> CollectAsync<T>(IAsyncEnumerable<T> records)
	{
		var list = new List<T>();
		await foreach (var record in records)
		{
			list.Add(record);
		}

		return list;
	}

	[Fact]
	public async Task ReadRecordsAsync_DownloadsAndReadsOneFile()
	{
		var stub = new StubHandler();
		stub.EnqueueFile(await ParquetFile.WriteAsync([new SoftwareRow { Product = "OpenSSL" }]), "application/octet-stream", "part-0.parquet");
		using var client = TestClient.CreateBulkExport(stub);
		var url = new Uri("https://files.example.test/asset_software/part-0.parquet?sig=1");

		var records = await CollectAsync(client.ReadRecordsAsync<AssetSoftwareRecord>(url, TestContext.Current.CancellationToken));

		records.Should().ContainSingle().Which.Product.Should().Be("OpenSSL");
		stub.Calls.Should().ContainSingle().Which.Uri.Should().Be(url);
	}

	[Fact]
	public async Task ReadRecordsAsync_ReadsEveryFileOfTheDataset_AndNoOther()
	{
		var stub = new StubHandler();
		stub.EnqueueFile(await ParquetFile.WriteAsync([new SoftwareRow { Product = "a" }]), "application/octet-stream", "0.parquet");
		stub.EnqueueFile(await ParquetFile.WriteAsync([new SoftwareRow { Product = "b" }, new SoftwareRow { Product = "c" }]), "application/octet-stream", "1.parquet");
		using var client = TestClient.CreateBulkExport(stub);
		var export = ExportWith(
			Result("asset", "https://files.example.test/asset/0.parquet"),
			Result("exports/1/asset_software/", "https://files.example.test/s/0.parquet", "https://files.example.test/s/1.parquet"));

		var records = await CollectAsync(client.ReadRecordsAsync<AssetSoftwareRecord>(export, ExportDatasets.AssetSoftware, TestContext.Current.CancellationToken));

		records.Select(r => r.Product).Should().Equal("a", "b", "c");
		stub.Calls.Select(c => c.Uri.AbsolutePath).Should().Equal("/s/0.parquet", "/s/1.parquet");
	}

	[Fact]
	public async Task ReadRecordsAsync_YieldsNothing_WithoutFilesForTheDataset()
	{
		var stub = new StubHandler();
		using var client = TestClient.CreateBulkExport(stub);

		var records = await CollectAsync(client.ReadRecordsAsync<AssetRecord>(ExportWith(), ExportDatasets.Asset, TestContext.Current.CancellationToken));

		records.Should().BeEmpty();
		stub.Calls.Should().BeEmpty();
	}

	[Fact]
	public async Task ReadRecordsAsync_RejectsAMissingExportOrDataset()
	{
		using var client = TestClient.CreateBulkExport(new StubHandler());
		var ct = TestContext.Current.CancellationToken;

		await FluentActions.Awaiting(() => CollectAsync(client.ReadRecordsAsync<AssetRecord>(null!, "asset", ct))).Should().ThrowAsync<ArgumentNullException>();
		await FluentActions.Awaiting(() => CollectAsync(client.ReadRecordsAsync<AssetRecord>(ExportWith(), " ", ct))).Should().ThrowAsync<ArgumentException>();
	}

	[Theory]
	[InlineData("asset", "asset", true)]
	[InlineData("ASSET", "asset", true)]
	[InlineData("exports/123/asset/", "asset", true)]
	[InlineData("asset_software", "asset", false)]
	[InlineData("exports/asset/part", "asset", false)]
	[InlineData(null, "asset", false)]
	public void IsDataset_MatchesTheNameOrLastPathSegment(string? prefix, string dataset, bool expected)
		=> Rapid7BulkExportClient.IsDataset(prefix, dataset).Should().Be(expected);
}
