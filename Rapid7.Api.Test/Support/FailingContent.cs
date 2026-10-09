using System.Net;

namespace Rapid7.Api.Test.Support;

/// <summary>Content whose body cannot be read: serializing it raises <paramref name="exception"/>.</summary>
internal sealed class FailingContent(Exception exception) : HttpContent
{
	protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context) => Task.FromException(exception);

	protected override bool TryComputeLength(out long length)
	{
		length = 0;
		return false;
	}
}
