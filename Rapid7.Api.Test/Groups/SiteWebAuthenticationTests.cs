using Rapid7.Api.Models.Sites;
using Rapid7.Api.Test.Support;
using System.Net;

namespace Rapid7.Api.Test.Groups;

public class SiteWebAuthenticationTests
{
	private const string HtmlFormsJson = """
		{
			"resources": [
				{
					"baseURL": "http://intranet.example.test",
					"enabled": true,
					"id": 4,
					"loginRegularExpression": "^.*Login failed.*$",
					"loginURL": "http://intranet.example.test/login",
					"name": "Intranet logon",
					"service": "html-form",
					"links": [ { "href": "https://console.test:3780/api/3/sites/7/web_authentication/html_forms/4", "rel": "self" } ]
				}
			],
			"links": [ { "href": "https://console.test:3780/api/3/sites/7/web_authentication/html_forms", "rel": "self" } ]
		}
		""";

	private const string HttpHeadersJson = """
		{
			"resources": [
				{
					"baseURL": "https://portal.example.test",
					"enabled": false,
					"headers": { "Cookie": "session=fake-session" },
					"id": 5,
					"loginRegularExpression": "denied",
					"name": "Portal session",
					"service": "http-header",
					"links": [ { "href": "https://console.test:3780/api/3/sites/7/web_authentication/http_headers/5", "rel": "self" } ]
				},
				{ "id": 6, "service": "something-new", "links": [] }
			],
			"links": [ { "href": "https://console.test:3780/api/3/sites/7/web_authentication/http_headers", "rel": "self" } ]
		}
		""";

	[Fact]
	public async Task ListHtmlFormsAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteWebAuthentication.ListHtmlFormsAsync(7, ct), HtmlFormsJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/web_authentication/html_forms");
	}

	[Fact]
	public async Task ListHtmlFormsAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.SiteWebAuthentication.ListHtmlFormsAsync(7, ct), HtmlFormsJson);

		var form = list.Resources.Should().ContainSingle().Subject;
		form.BaseUrl.Should().Be("http://intranet.example.test");
		form.Enabled.Should().BeTrue();
		form.Id.Should().Be(4);
		form.LoginRegularExpression.Should().Be("^.*Login failed.*$");
		form.LoginUrl.Should().Be("http://intranet.example.test/login");
		form.Name.Should().Be("Intranet logon");
		form.Service.Should().Be(WebAuthenticationService.HtmlForm);
		form.Items.Should().ContainSingle().Which.Href.Should().EndWith("/html_forms/4");
		list.Items.Should().ContainSingle();
	}

	[Fact]
	public async Task ListHttpHeadersAsync_SendsGet()
	{
		var call = await TestClient.CaptureAsync((c, ct) => c.SiteWebAuthentication.ListHttpHeadersAsync(7, ct), HttpHeadersJson);

		call.ShouldBe(HttpMethod.Get, "/api/3/sites/7/web_authentication/http_headers");
	}

	[Fact]
	public async Task ListHttpHeadersAsync_MapsEveryField()
	{
		var list = await TestClient.ReadAsync((c, ct) => c.SiteWebAuthentication.ListHttpHeadersAsync(7, ct), HttpHeadersJson);

		list.Resources.Should().HaveCount(2);
		var header = list.Resources[0];
		header.BaseUrl.Should().Be("https://portal.example.test");
		header.Enabled.Should().BeFalse();
		header.Headers.Should().ContainSingle().Which.Should().Be(new KeyValuePair<string, string>("Cookie", "session=fake-session"));
		header.Id.Should().Be(5);
		header.LoginRegularExpression.Should().Be("denied");
		header.Name.Should().Be("Portal session");
		header.Service.Should().Be(WebAuthenticationService.HttpHeader);
		header.Items.Should().ContainSingle().Which.Href.Should().EndWith("/http_headers/5");
		var bare = list.Resources[1];
		bare.Service.Should().Be(WebAuthenticationService.Unknown);
		bare.Headers.Should().BeEmpty();
	}

	[Fact]
	public Task ListHtmlFormsAsync_Unauthorized_RaisesRapid7ApiException()
		=> TestClient.ShouldFailAsync(
			(c, ct) => c.SiteWebAuthentication.ListHtmlFormsAsync(7, ct),
			HttpStatusCode.Unauthorized,
			"""{"status":"UNAUTHORIZED","message":"Full authentication is required to access this resource.","links":[]}""",
			"Full authentication is required to access this resource.");
}
