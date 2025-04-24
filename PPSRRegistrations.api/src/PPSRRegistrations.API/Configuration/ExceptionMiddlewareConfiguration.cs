using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using PPSRRegistrations.Application.ViewModels;
using PPSRRegistrations.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace PPSRRegistrations.API.Configuration
{
    public static class ExceptionMiddlewareConfiguration
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        // Get the options
                        var jsonOptions = context.RequestServices.GetService<IOptions<JsonOptions>>();

                        // Serialise using the settings provided
                        var json = JsonSerializer.Serialize(
                            ResponseViewModel.Error(contextFeature.Error.Message), // Switch this with your object
                            jsonOptions?.Value.SerializerOptions);

                        BusinessException? bussinessEx;
                        if ((bussinessEx = contextFeature.Error as BusinessException) != null && bussinessEx.Code != null)
                        {
                            context.Response.StatusCode = bussinessEx.Code.Value;
                        }

                        await context.Response.WriteAsync(json);

                    }
                });
            });

            
        }
    }

    public class ValidationProblemDetailsResult : Microsoft.AspNetCore.Mvc.IActionResult
    {
        public async Task ExecuteResultAsync(Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            var modelStateEntries = context.ModelState.Where(e => e.Value.Errors.Count > 0).ToArray();

            if (modelStateEntries.Any())
            {
                var problemDetails = ResponseViewModel.Error(string.Join("\n", modelStateEntries.SelectMany(x => x.Value != null ? x.Value.Errors.Select(y => y.ErrorMessage) : new string[0])));

                await context.HttpContext.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
