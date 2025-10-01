using Microsoft.AspNetCore.Builder;
using TimeTwoFix.Web;
using TimeTwoFix.Web.Middleware;

namespace TimeTwoFix.Web.Extensions
{
    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            return app;
        }
    }
}
