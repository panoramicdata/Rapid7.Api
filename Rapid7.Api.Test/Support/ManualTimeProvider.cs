namespace Rapid7.Api.Test.Support;

/// <summary>A clock that only moves when told to, for deterministic timeout tests.</summary>
internal sealed class ManualTimeProvider : TimeProvider
{
	private long _ticks;

	public override long TimestampFrequency => TimeSpan.TicksPerSecond;

	public override long GetTimestamp() => _ticks;

	public void Advance(TimeSpan by) => _ticks += by.Ticks;
}
