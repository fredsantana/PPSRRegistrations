using PPSRRegistrations.API.Filters;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace PPSRRegistrations.API.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PPSRRegistrations API Reference", Version = "v1"});

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                c.DocumentFilter<LowercaseDocumentFilter>();
                
                c.CustomSchemaIds(type =>
                {
                    string returnedValue = type.Name;
                    if (returnedValue.EndsWith("ViewModel"))
                        returnedValue = returnedValue.Replace("ViewModel", string.Empty);
                    return returnedValue;
                });

                c.EnableAnnotations();
            });
        }
    }
}
