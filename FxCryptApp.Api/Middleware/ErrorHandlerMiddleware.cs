using System.Net;
using System.Text.Json;
using FxCryptApp.Common.ApplicationExceptions;

namespace FxCryptApp.Api.Middleware;

public class ErrorHandlerMiddleware
{
	readonly RequestDelegate _next;

	public ErrorHandlerMiddleware(RequestDelegate next)
	{
		_next = next;
	}
	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception error)
		{
			var response = context.Response;
			response.ContentType = "application/json";

			switch (error)
			{
				case BaseApplicationException e:
					// not found error
					response.StatusCode = (int)e.StatusCode;
					break;
				case ArgumentNullException e:
					response.StatusCode = (int)HttpStatusCode.BadRequest;
					break;
				default:
					// unhandled error
					response.StatusCode = (int)HttpStatusCode.InternalServerError;
					break;
			}

			var result = JsonSerializer.Serialize(new { message = error?.Message });
			await response.WriteAsync(result);
		}
	}
}
