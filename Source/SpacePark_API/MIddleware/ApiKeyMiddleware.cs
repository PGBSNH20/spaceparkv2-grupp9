using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace SpacePark_API.MIddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKey = "ApiKey";
        private const string ApiKeyAdmin = "ApiKeyAdmin";



        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //Om det finns en api nyckel
            if (httpContext.Request.Headers.TryGetValue(ApiKey, out var extractedApiKey))
            {
                var appSettings = httpContext.RequestServices.GetRequiredService<IConfiguration>();

                var apiKey = appSettings.GetValue<string>(ApiKey);

                if (!apiKey.Equals(extractedApiKey))
                {
                    httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsync("Unauthorized Api key");

                    return;
                }
            }

            if (httpContext.Request.Headers.TryGetValue(ApiKeyAdmin, out var extractedAdminApiKey))
            {
                var appSettingsAdmin = httpContext.RequestServices.GetRequiredService<IConfiguration>();

                var apiKeyAdmin = appSettingsAdmin.GetValue<string>(ApiKeyAdmin);

                if (apiKeyAdmin.Equals(extractedAdminApiKey))
                {
                    await _next(httpContext);
                    return;
                }
            }

            //Om det inte finns en api nyckel
            if (!httpContext.Request.Headers.TryGetValue(ApiKey, out var b))
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync("Unauthorized client");

                return;
            }

            await _next(httpContext);
            return;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ApiKeyMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiKeyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiKeyMiddleware>();
        }
    }
}
