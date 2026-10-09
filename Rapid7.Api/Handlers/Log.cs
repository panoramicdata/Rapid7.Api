using Microsoft.Extensions.Logging;

namespace Rapid7.Api.Handlers;

/// <summary>Source-generated log messages. Only methods and paths are logged, never query strings or credentials.</summary>
internal static partial class Log
{
	[LoggerMessage(EventId = 1, Level = LogLevel.Debug, Message = "Rapid7 {Method} {Path} (attempt {Attempt})")]
	public static partial void Sending(ILogger logger, HttpMethod method, string path, int attempt);

	[LoggerMessage(EventId = 2, Level = LogLevel.Warning, Message = "Rapid7 returned {Status} for {Method} {Path}; retrying in {Delay}")]
	public static partial void Retrying(ILogger logger, int status, HttpMethod method, string path, TimeSpan delay);

	[LoggerMessage(EventId = 3, Level = LogLevel.Warning, Message = "Rapid7 connection failed ({Error}) for {Method} {Path}; retrying in {Delay}")]
	public static partial void RetryingConnection(ILogger logger, HttpRequestError error, HttpMethod method, string path, TimeSpan delay);
}
