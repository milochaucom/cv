using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Milochau.Core.Aws.DynamoDB;
using Milochau.CV.Http.Apis.v1;
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
            var v1 = app.MapGroup("/api/v1");

            var anonymousV1 = v1.MapGroup("/a").AllowAnonymous();
            var anonymousV1Resumes = anonymousV1.MapGroup("/resumes").WithTags("Resumes");
            anonymousV1Resumes.MapGet("", ResumesGet.DelegateAnonymous);

            var authenticatedV1 = v1.MapGroup("");
            var authenticatedV1Origins = authenticatedV1.MapGroup("/origins").WithTags("Origins");
            authenticatedV1Origins.MapPost("", OriginsPost.DelegateAuthenticated);

            var authenticatedV1Resumes = authenticatedV1.MapGroup("/resumes").WithTags("Resumes");
            authenticatedV1Resumes.MapGet("", ResumesGet.DelegateAuthenticated);
            authenticatedV1Resumes.MapPost("/{resumeId}", ResumesPost.DelegateAuthenticated);

            return app;
        }
    }

    [JsonSerializable(typeof(ValidationProblemDetails))]
    [JsonSerializable(typeof(OriginsPostBody))]
    [JsonSerializable(typeof(ResumesGetResponse))]
    [JsonSerializable(typeof(ResumesPostBody))]
    public partial class ApiPayloadJsonSerializerContext : JsonSerializerContext
    {
    }
}
