using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Models;
using Milochau.Core.Aws.DynamoDB;
using Milochau.Core.Aws.Integration;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Milochau.CV.Tests.Integration.Apis
{
    public static class ResumesApis
    {
        public static IEndpointRouteBuilder MapResumesApis(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/resumes/{resumeId}", async (HttpContext httpContext, string resumeId, CancellationToken cancellationToken) =>
            {
                var proxyRequest = await ApiGatewayHelpers.BuildProxyRequestAsync(httpContext, new ProxyRequestOptions
                {
                    UserId = "65a133d934f948f7b27eb55c803e2ea5",
                    PathParameters = new Dictionary<string, string>
                    {
                        { "resumeId", resumeId },
                    },
                }, cancellationToken);
                var credentials = new AssumeRoleAWSCredentials(Environment.GetEnvironmentVariable("AWS_ROLE_ARN")!);
                var lambdaFunction = new Http.Resumes.Post.Function(new AmazonDynamoDBClient(credentials));
                var proxyResponse = await lambdaFunction.DoAsync(proxyRequest, new TestLambdaContext(), cancellationToken);
                return ApiGatewayHelpers.BuildResult(proxyResponse);
            })
            .Accepts<Http.Resumes.Post.FunctionRequestBody>("application/json")
            .WithOpenApi(x =>
            {
                x.Parameters.Add(new OpenApiParameter { In = ParameterLocation.Query, Name = "lang" });
                return x;
            })
            .Produces(204)
            .WithTags("Resumes")
            .WithSummary("Create a resume")
            .WithOpenApi();

            endpoints.MapGet("/resumes", async (HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                var proxyRequest = await ApiGatewayHelpers.BuildProxyRequestAsync(httpContext, new ProxyRequestOptions(), cancellationToken);
                var credentials = new AssumeRoleAWSCredentials(Environment.GetEnvironmentVariable("AWS_ROLE_ARN")!);
                var lambdaFunction = new Http.Resumes.Get.Function(new AmazonDynamoDBClient(credentials));
                var proxyResponse = await lambdaFunction.DoAsync(proxyRequest, new TestLambdaContext(), cancellationToken);
                return ApiGatewayHelpers.BuildResult(proxyResponse);
            })
            .WithOpenApi(x =>
            {
                x.Parameters.Add(new OpenApiParameter { In = ParameterLocation.Query, Name = "origin", Required = true });
                x.Parameters.Add(new OpenApiParameter { In = ParameterLocation.Query, Name = "lang" });
                return x;
            })
            .Produces<Http.Resumes.Get.FunctionResponse>(200, "application/json")
            .Produces(400)
            .WithTags("Resumes")
            .WithSummary("Get a resume")
            .WithOpenApi();

            endpoints.MapGet("/a/resumes", async (HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                var proxyRequest = await ApiGatewayHelpers.BuildProxyRequestAsync(httpContext, new ProxyRequestOptions
                {
                    AnonymousRequest = true,
                }, cancellationToken);
                var credentials = new AssumeRoleAWSCredentials(Environment.GetEnvironmentVariable("AWS_ROLE_ARN")!);
                var lambdaFunction = new Http.Resumes.Get.Function(new AmazonDynamoDBClient(credentials));
                var proxyResponse = await lambdaFunction.DoAsync(proxyRequest, new TestLambdaContext(), cancellationToken);
                return ApiGatewayHelpers.BuildResult(proxyResponse);
            })
            .WithOpenApi(x =>
            {
                x.Parameters.Add(new OpenApiParameter { In = ParameterLocation.Query, Name = "origin", Required = true });
                x.Parameters.Add(new OpenApiParameter { In = ParameterLocation.Query, Name = "lang" });
                return x;
            })
            .Produces<Http.Resumes.Get.FunctionResponse>(200, "application/json")
            .Produces(400)
            .WithTags("Resumes")
            .WithSummary("Get a resume")
            .WithOpenApi();

            return endpoints;
        }
    }
}
