using PPSRRegistrations.API.Configuration;
using ElmahCore.Mvc;
using ElmahCore;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddElmah<XmlFileErrorLog>(options =>
{
    var path = configuration.GetSection("LoggerOptions")["Path"];

    if (!Directory.Exists(path))
        Directory.CreateDirectory(path);

    options.LogPath = path;
});

// Increase the size limit for files received via form
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 25 * 1024 * 1024; // 25MB
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerSetup();
builder.Services.AddAutomapperSetup();
builder.Services.AddDIConfiguration();
builder.Services.AddHealthCheckSetup(configuration);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = ctx => new ValidationProblemDetailsResult();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger(c =>
{
    c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PPSRRegistrations API v1");
});

app.ConfigureExceptionHandler();
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseRouting();
app.UseElmah();
// app.UseAuthentication();
// app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthCheckSetup();
});

app.Run();