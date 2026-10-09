namespace Rapid7.Api.Test.Support;

/// <summary>Assertions on a request recorded by <see cref="StubHandler"/>, shared by every endpoint group's tests.</summary>
internal static class RecordedCallAssertions
{
	/// <summary>
	/// Asserts the method, escaped path, query (empty string for none) and body of a recorded request. A body must be
	/// sent as <c>application/json</c> unless the calling test asserts another content type itself.
	/// </summary>
	public static void ShouldBe(this RecordedCall call, HttpMethod method, string path, string query = "", string? body = null)
	{
		call.Method.Should().Be(method);
		call.Uri.AbsolutePath.Should().Be(path);
		call.Uri.Query.Should().Be(query);
		call.Body.Should().Be(body);
		if (body is not null && call.ContentType is not ("text/plain" or "application/octet-stream" or "multipart/form-data"))
		{
			call.ContentType.Should().Be("application/json");
		}
	}
}
