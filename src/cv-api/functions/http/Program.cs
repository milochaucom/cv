using Microsoft.AspNetCore.Builder;
using Milochau.Core.Aws.Core.JsonConverters;
using Milochau.CV.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = AwsLambdaWebApplication.CreateBuilder(new AwsLambdaWebApplicationOptions
{
    Args = args,
    ApplicationName = "CV",
    EnvironmentName = "Production",
    JsonOptions = options =>
    {
        options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.SerializerOptions.Converters.Add(new GuidConverter());
        options.SerializerOptions.TypeInfoResolver = ApiPayloadJsonSerializerContext.Default;
    }
});

builder.AddApplicationDependencies();

var app = builder.Build();

app.UseAwsLambdaMiddlewares();

app.UseApplicationMiddlewares();

app.Run();
