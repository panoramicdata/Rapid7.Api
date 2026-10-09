using Rapid7.Api.Test.Support;
using System.Net;
using System.Text;

namespace Rapid7.Api.Test.Core;

/// <summary>How <see cref="Rapid7ErrorMapper"/> turns error responses (v3 JSON, v4 JSON or XML, anything else) into exceptions.</summary>
public class ErrorMapperTests
{
	/// <summary>Content whose body cannot be read.</summary>
	private sealed class FailingContent(Exception exception) : HttpContent
	{
		protected override Task SerializeToStreamAsync(Stream stream, System.Net.TransportContext? context) => throw exception;

		protected override bool TryComputeLength(out long length)
		{
			length = 0;
			return false;
		}
	}

	private static async Task<Rapid7ApiException> MapAsync(HttpContent content, HttpStatusCode status = HttpStatusCode.BadRequest, string? reason = null)
	{
		using var response = new HttpResponseMessage(status) { Content = content, ReasonPhrase = reason };
		var exception = await Rapid7ErrorMapper.CreateAsync(response);
		return exception.Should().BeOfType<Rapid7ApiException>().Subject;
	}

	private static Task<Rapid7ApiException> MapAsync(string body, HttpStatusCode status = HttpStatusCode.BadRequest)
		=> MapAsync(new StringContent(body, Encoding.UTF8, "application/json"), status);

	[Fact]
	public async Task Success_IsNotAnError()
	{
		using var response = new HttpResponseMessage(HttpStatusCode.OK);

		(await Rapid7ErrorMapper.CreateAsync(response)).Should().BeNull();
	}

	[Fact]
	public async Task V3Body_GivesStatusMessageAndLinks()
	{
		var error = await MapAsync(
			"""  {"status":"NOT_FOUND","message":"Site 7 not found.","links":[{"href":"https://console.test:3780/api/3/sites/7","rel":"self"},"junk",{"href":42}]}""",
			HttpStatusCode.NotFound);

		error.StatusCode.Should().Be(HttpStatusCode.NotFound);
		error.ErrorStatus.Should().Be("NOT_FOUND");
		error.Message.Should().Be("Site 7 not found.");
		error.LocalizedMessage.Should().BeNull();
		error.Links.Should().HaveCount(2);
		error.Links[0].Href.Should().Be("https://console.test:3780/api/3/sites/7");
		error.Links[0].Rel.Should().Be("self");
		error.Links[1].Href.Should().Be("42");
		error.Links[1].Rel.Should().BeNull();
	}

	[Theory]
	[InlineData("""{"status":404,"message":"No such asset","localized_message":"Asset not found"}""")]
	[InlineData("""{"status":404,"message":"No such asset","localizedMessage":"Asset not found"}""")]
	[InlineData("""<?xml version="1.0"?><ErrorResource><status>404</status><message> No such asset </message><localizedMessage>Asset not found</localizedMessage></ErrorResource>""")]
	[InlineData("""<ErrorResource xmlns="urn:rapid7"><status>404</status><message>No such asset</message><localized_message>Asset not found</localized_message></ErrorResource>""")]
	public async Task V4Body_JsonOrXml_GivesNumericStatusAndLocalizedMessage(string body)
	{
		var error = await MapAsync(body, HttpStatusCode.NotFound);

		error.ErrorStatus.Should().Be("404");
		error.Message.Should().Be("No such asset");
		error.LocalizedMessage.Should().Be("Asset not found");
		error.Links.Should().BeEmpty();
	}

	[Theory]
	[InlineData("""{"localized_message":"Only this"}""")]
	[InlineData("""{"message":"   ","localized_message":"Only this"}""")]
	[InlineData("""<ErrorResource><message></message><localizedMessage>Only this</localizedMessage></ErrorResource>""")]
	public async Task WithoutAMessage_TheLocalizedMessageIsUsed(string body)
		=> (await MapAsync(body)).Message.Should().Be("Only this");

	[Theory]
	[InlineData("")]
	[InlineData("   ")]
	[InlineData("<html><body>502 Bad Gateway</body></html")]
	[InlineData("Bad Gateway")]
	[InlineData("{not json")]
	[InlineData("""["an","array"]""")]
	[InlineData("""{"status":true,"message":{"nested":1},"links":{"not":"an array"}}""")]
	[InlineData("<ErrorResource/>")]
	public async Task AnyOtherBody_FallsBackToTheHttpStatus(string body)
	{
		var error = await MapAsync(body, HttpStatusCode.BadGateway);

		error.Message.Should().Be("HTTP 502 (Bad Gateway)");
		error.ErrorStatus.Should().BeNull();
		error.LocalizedMessage.Should().BeNull();
		error.Links.Should().BeEmpty();
	}

	[Fact]
	public async Task XmlWithADocumentTypeDefinition_IsNotParsed()
	{
		const string body = """<?xml version="1.0"?><!DOCTYPE ErrorResource [<!ENTITY e "expanded">]><ErrorResource><message>&e;</message></ErrorResource>""";

		var error = await MapAsync(body, HttpStatusCode.BadGateway);

		error.Message.Should().Be("HTTP 502 (Bad Gateway)", "a DTD in a response is never processed: entity expansion is a denial-of-service vector");
	}

	[Fact]
	public async Task TheFallback_UsesTheReasonPhrase_OrTheStatusName()
	{
		(await MapAsync(new StringContent(""), HttpStatusCode.InternalServerError, "Kaput")).Message.Should().Be("HTTP 500 (Kaput)");

		using var response = new HttpResponseMessage(HttpStatusCode.Forbidden) { Content = new StringContent("") };
		response.ReasonPhrase = null;
		var error = (Rapid7ApiException)(await Rapid7ErrorMapper.CreateAsync(response))!;
		error.Message.Should().StartWith("HTTP 403 (");
	}

	[Fact]
	public async Task AnUnsupportedCharset_FallsBackToTheHttpStatus()
	{
		var content = new ByteArrayContent("""{"message":"unreadable"}"""u8.ToArray());
		content.Headers.TryAddWithoutValidation("Content-Type", "application/json; charset=no-such-charset");

		(await MapAsync(content, HttpStatusCode.BadRequest, "Bad Request")).Message.Should().Be("HTTP 400 (Bad Request)");
	}

	[Theory]
	[MemberData(nameof(ReadFailures))]
	public async Task ABodyThatCannotBeRead_FallsBackToTheHttpStatus(Exception failure)
		=> (await MapAsync(new FailingContent(failure), HttpStatusCode.ServiceUnavailable, "Service Unavailable")).Message.Should().Be("HTTP 503 (Service Unavailable)");

	public static TheoryData<Exception> ReadFailures => [new IOException("reset"), new HttpRequestException("lost")];

	[Fact]
	public async Task ThroughAClient_ErrorsRaiseRapid7ApiException()
		=> await TestClient.ShouldFailAsync(
			(c, ct) => c.Root.GetAsync(ct),
			HttpStatusCode.Forbidden,
			"""{"status":"FORBIDDEN","message":"Not allowed.","links":[]}""",
			"Not allowed.");
}
