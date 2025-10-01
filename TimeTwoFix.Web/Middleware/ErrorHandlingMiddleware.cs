using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using TimeTwoFix.Core.Common.Exceptions;
using TimeTwoFix.Web.Models.ErrorModels;

namespace TimeTwoFix.Web.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public ErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger,
            IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string errorMessage = GetUserFriendlyMessage(exception);
            var errorViewModel = CreateErrorViewModel(exception);
            
            // Log the error with detailed information
            LogError(context, exception);

            if (IsApiRequest(context))
            {
                await HandleApiError(context, errorViewModel);
            }
            else
            {
                await HandleWebError(context, errorViewModel);
            }
        }

        private string GetUserFriendlyMessage(Exception exception)
        {
            return exception switch
            {
                AppException appEx => appEx.UserFriendlyMessage,
                UnauthorizedAccessException => "You don't have permission to perform this action.",
                _ => _environment.IsDevelopment() 
                    ? exception.Message 
                    : "An unexpected error occurred. Our technical team has been notified."
            };
        }

        private ErrorViewModel CreateErrorViewModel(Exception exception)
        {
            return exception switch
            {
                EntityNotFoundException notFoundEx => ErrorViewModel.Create404(),
                ValidationException validationEx => ErrorViewModel.CreateValidationError(
                    new Dictionary<string, string[]> { { "Error", new[] { validationEx.Message } } }),
                BusinessRuleException businessEx => ErrorViewModel.CreateBusinessError(businessEx.UserFriendlyMessage),
                UnauthorizedAccessException => ErrorViewModel.Create403(),
                _ => ErrorViewModel.Create500()
            };
        }

        private void LogError(HttpContext context, Exception exception)
        {
            var logMessage = new
            {
                Error = exception.Message,
                StackTrace = exception.StackTrace,
                InnerException = exception.InnerException?.Message,
                Path = context.Request.GetDisplayUrl(),
                User = context.User?.Identity?.Name ?? "Anonymous",
                Timestamp = DateTime.UtcNow
            };

            if (exception is AppException)
            {
                _logger.LogWarning(exception, "Application error occurred: {LogMessage}", 
                    JsonSerializer.Serialize(logMessage));
            }
            else
            {
                _logger.LogError(exception, "Unhandled exception: {LogMessage}", 
                    JsonSerializer.Serialize(logMessage));
            }
        }

        private bool IsApiRequest(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/api") || 
                   context.Request.Headers["Accept"].Any(x => x.Contains("application/json"));
        }

        private async Task HandleApiError(HttpContext context, ErrorViewModel errorViewModel)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorViewModel.ErrorCode switch
            {
                "404" => (int)HttpStatusCode.NotFound,
                "403" => (int)HttpStatusCode.Forbidden,
                "400" => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var response = new
            {
                errorViewModel.ErrorCode,
                errorViewModel.UserFriendlyMessage,
                errorViewModel.ValidationErrors
            };

            await context.Response.WriteAsJsonAsync(response);
        }

        private async Task HandleWebError(HttpContext context, ErrorViewModel errorViewModel)
        {
            // Store the error details in TempData for display on error page
            var tempData = context.RequestServices.GetService<ITempDataDictionary>();
            if (tempData != null)
            {
                // Store individual properties that the error view expects
                tempData["ErrorTitle"] = errorViewModel.Title;
                tempData["ErrorMessage"] = errorViewModel.UserFriendlyMessage;
                tempData["ErrorCode"] = errorViewModel.ErrorCode;
                tempData["ShowRequestId"] = errorViewModel.ShowRequestId;
                tempData["RequestId"] = errorViewModel.RequestId;
                tempData["ValidationErrors"] = errorViewModel.ValidationErrors;
                tempData["ShowHomeLink"] = errorViewModel.ShowHomeLink;
                tempData["ReturnUrl"] = errorViewModel.ReturnUrl;
                tempData["ReturnLinkText"] = errorViewModel.ReturnLinkText;
            }

            // Redirect to the error page
            context.Response.Redirect($"/Shared/Error");
            await Task.CompletedTask;
        }
    }

    // Extension method to add the middleware
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
