using Microsoft.AspNetCore.Builder;
using Milochau.Core.Aws.Core.JsonConverters;
using Milochau.CV.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

var options = new IntegrationWebApplicationOptions
{
    Args = args,
    CorsOrigins = ["http://localhost:3000", "http://localhost:4173"],
    JsonOptions = options =>
    {
        options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.SerializerOptions.Converters.Add(new GuidConverter());
        options.SerializerOptions.TypeInfoResolver = ApiPayloadJsonSerializerContext.Default;
    },
};

var builder = IntegrationWebApplication.CreateBuilder(options);

builder.AddApplicationDependencies();

var app = builder.Build();

app.UseIntegrationMiddlewares(options);

app.UseApplicationMiddlewares();

app.Run();
