using System.Net;

namespace Rapid7.Api.Test.Support;

/// <summary>A transport that answers every request with an empty links body and records whether it was disposed.</summary>
internal sealed class TrackingHandler : HttpMessageHandler
{
	public bool Disposed { get; private set; }

	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		=> Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { RequestMessage = request, Content = new StringContent("""{"links":[]}""") });

	protected override void Dispose(bool disposing)
	{
		Disposed = true;
		base.Dispose(disposing);
	}
}
