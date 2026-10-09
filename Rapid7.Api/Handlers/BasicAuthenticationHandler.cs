using System.Net.Http.Headers;
using System.Text;

namespace Rapid7.Api.Handlers;

/// <summary>
/// Authenticates Security Console requests with HTTP basic credentials and, for accounts with two-factor authentication,
/// the <c>Token</c> header; asks for JSON (<c>Accept: application/json</c>). The credentials are copied at construction.
/// </summary>
internal sealed class BasicAuthenticationHandler : DelegatingHandler
{
	internal const string TokenHeader = "Token";

	private readonly AuthenticationHeaderValue _authorization;
	private readonly string? _twoFactorToken;

	public BasicAuthenticationHandler(string username, string password, string? twoFactorToken)
	{
		_authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));
		_twoFactorToken = string.IsNullOrWhiteSpace(twoFactorToken) ? null : twoFactorToken;
	}

	/// <inheritdoc />
	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		request.Headers.Authorization = _authorization;
		if (_twoFactorToken is not null)
		{
			request.Headers.Remove(TokenHeader);
			request.Headers.Add(TokenHeader, _twoFactorToken);
		}

		JsonAccept.Apply(request);
		return base.SendAsync(request, cancellationToken);
	}
}
