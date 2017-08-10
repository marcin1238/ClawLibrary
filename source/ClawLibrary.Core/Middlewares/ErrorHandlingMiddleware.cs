using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using ClawLibrary.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ClawLibrary.Core.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception is UnauthorizedAccessException)
            {
                return HandleUnauthorizedException(context);
            }
            else if (exception is BusinessException)
            {
                // If the exception is of type BusinessException then a business rule has been violated...
                return HandleBusinessException(context, exception);
            }
            else
            {
                // For all other exceptions, log it and let it fall the through to ASP.Net
                string result = JsonConvert.SerializeObject(new { error = exception.Message });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)500;
                return context.Response.WriteAsync(result);
            }
        }

        private static Task HandleBusinessException(HttpContext context, Exception exception)
        {
            var ex = exception as BusinessException;

            string name = null;

            if (ex.ErrorCode != null)
            {
                var errorCodeType = ex.ErrorCode.GetType();
                var description = errorCodeType.GetField(ex.ErrorCode.ToString()).GetCustomAttributes<DescriptionAttribute>().FirstOrDefault();

                if (description != null)
                    name = description.Description;
                else
                    name = ex.ErrorCode.ToString();
            }

            // Business rule exceptions are to be returned as HTTP 429 responses
            string result = JsonConvert.SerializeObject(new
            {
                message = ex.Message,
                errorName = name,
                errorCode = ex.ErrorCode?.ToString("d"),
                details = ex.Details?.Data
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)429;
            return context.Response.WriteAsync(result);

        }

        private static Task HandleUnauthorizedException(HttpContext context)
        {

            string result = JsonConvert.SerializeObject(new
            {
                message = "Unauthorized access requested",
                errorName = "Unauthorized",
                errorCode = 401
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return context.Response.WriteAsync(result);
        }
    }
}
