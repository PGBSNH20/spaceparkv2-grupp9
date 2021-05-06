using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePark_API.Attribues
{
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "ApiKey";
        private const string APIKEYADMIN = "ApiKeyAdmin";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //if there are normal api key
            if (context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME,out var extractedApiKey))
            {
                var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

                var apiKey = appSettings.GetValue<string>(APIKEYNAME);

                if (!apiKey.Equals(extractedApiKey))
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 401,
                        Content = "Api Key is not valid"
                    };
                    return;
                }
            }

            //if there are admin api key
            else if (context.HttpContext.Request.Headers.TryGetValue(APIKEYADMIN, out var extractedApiKeyAdmin))
            {
                var appSettingsAdmin = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

                var apiKeyAdmin = appSettingsAdmin.GetValue<string>(APIKEYADMIN);

                if (!apiKeyAdmin.Equals(extractedApiKeyAdmin))
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 401,
                        Content = "Api Key is not valid (admin)"
                    };
                    return;
                }
            }

            //If there are no api key
            if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYADMIN, out var a) && !context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var b))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api Key was not provided"
                };
                return;
            }


            await next();
        }
    }
}
