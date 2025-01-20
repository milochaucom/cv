using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Milochau.Core.Aws.DynamoDB;
using Milochau.CV.Http.Apis.Anonymous;
using Milochau.CV.Http.Apis.Authenticated;
using Milochau.CV.Http.Models;
using Milochau.CV.Shared.Data;
using System.Text.Json.Serialization;

namespace Milochau.CV.Http
{
    public static class Registration
    {
        public static WebApplicationBuilder AddApplicationDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();

            builder.Services.AddSingleton<AccessRepository>();
            builder.Services.AddSingleton<OriginRepository>();
            builder.Services.AddSingleton<ResumeRepository>();

            return builder;
        }

        public static WebApplication UseApplicationMiddlewares(this WebApplication app)
        {
            var anonymousApisGroup = app.MapGroup("/api/a").AllowAnonymous();
            anonymousApisGroup.MapResumesAnonymousApi();

            var authenticatedApisGroup = app.MapGroup("/api");
            authenticatedApisGroup.MapResumesAuthenticatedApi();
            authenticatedApisGroup.MapOriginsAuthenticatedApi();

            return app;
        }
    }

    [JsonSerializable(typeof(ValidationProblemDetails))]
    [JsonSerializable(typeof(OriginsPostRequest))]
    [JsonSerializable(typeof(ResumesGetResponse))]
    [JsonSerializable(typeof(ResumesPostRequest))]
    public partial class ApiPayloadJsonSerializerContext : JsonSerializerContext
    {
    }
}
