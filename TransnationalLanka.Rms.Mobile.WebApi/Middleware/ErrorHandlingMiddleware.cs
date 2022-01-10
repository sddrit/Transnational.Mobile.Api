using System.Net;
using System.Text.Json;
using TransnationalLanka.Rms.Mobile.Core.Exceptions;

namespace TransnationalLanka.Rms.Mobile.WebApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ServiceException ex)
            {
                await HandleExceptionAsync(context, ex);
            }

            catch (Exception ex)
            {
                await HandleExceptionAsync(context, new Exception($"Internal server error", ex));
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var errorMessages = new List<ErrorMessage>();

            if (exception is ServiceException)
            {
                code = HttpStatusCode.BadRequest;
                var serviceException = ((ServiceException)(exception));
                errorMessages.AddRange(serviceException.Messages);
            }
            else
            {
                errorMessages.Add(new ErrorMessage()
                {
                    Code = exception.InnerException.Message,
                    Message = exception.Message
                });
            }

            var result = System.Text.Json.JsonSerializer.Serialize(new
            {
                Errors = errorMessages
            }, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;
            }

            return context.Response.WriteAsync(result);
        }
    }
}
