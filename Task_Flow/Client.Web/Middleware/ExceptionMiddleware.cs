using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Client.Web.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            //string message = "Something went wrong";

            //if (ex is CustomException customEx)
            //{
            //    statusCode = customEx.StatusCode;
            //    message = ex.Message;
            //}

            //_logger.LogError($"Exception caught: {message}");

            //var problemDetails = new ProblemDetails
            //{
            //    Status = (int)statusCode,
            //    Title = "An error occurred",
            //    Detail = message
            //};

            //httpContext.Response.ContentType = "application/json";
            //httpContext.Response.StatusCode = (int)statusCode;
            //await httpContext.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));

            httpContext.Response.Redirect("/error");
        }
    }
}
