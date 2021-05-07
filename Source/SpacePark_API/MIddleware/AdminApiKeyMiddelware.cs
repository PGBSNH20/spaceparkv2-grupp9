using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace SpacePark_API.MIddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AdminApiKeyMiddelware
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyAdmin = "ApiKeyAdmin";


        public AdminApiKeyMiddelware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //Om det finns en admin api nyckel
            if (httpContext.Request.Headers.TryGetValue(ApiKeyAdmin, out var extractedApiKeyAdmin))
            {
                var appSettingsAdmin = httpContext.RequestServices.GetRequiredService<IConfiguration>();

                var apiKeyAdmin = appSettingsAdmin.GetValue<string>(ApiKeyAdmin);

                if (!apiKeyAdmin.Equals(extractedApiKeyAdmin))
                {
                    httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsync("Unauthorized Api key");

                    return;
                }
            }

            //Om det inte finns en admin api nyckel
            if (!httpContext.Request.Headers.TryGetValue(ApiKeyAdmin, out var b))
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync("Unauthorized client");

                return;
            }

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AdminApiKeyMiddelwareExtensions
    {
        public static IApplicationBuilder UseAdminApiKeyMiddelware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AdminApiKeyMiddelware>();
        }
    }
}
