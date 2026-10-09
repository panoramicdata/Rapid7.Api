namespace Rapid7.Api.Models.BulkExport;

/// <summary>How <see cref="Rapid7BulkExportClient"/> waits for an export to finish.</summary>
public sealed class ExportWaitOptions
{
	/// <summary>How long to wait between status queries. Must be greater than zero; the default is 15 seconds.</summary>
	public TimeSpan PollInterval { get; init; } = TimeSpan.FromSeconds(15);

	/// <summary>
	/// How long to wait in all before giving up with a <see cref="TimeoutException"/>. Must be greater than zero; the default
	/// is one hour.
	/// </summary>
	public TimeSpan Timeout { get; init; } = TimeSpan.FromHours(1);

	internal void Validate()
	{
		ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(PollInterval, TimeSpan.Zero);
		ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(Timeout, TimeSpan.Zero);
	}
}
