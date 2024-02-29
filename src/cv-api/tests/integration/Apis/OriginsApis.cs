using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Milochau.Core.Aws.Integration;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Milochau.CV.Tests.Integration.Apis
{
    public static class OriginsApis
    {
        public static IEndpointRouteBuilder MapOriginsApis(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/origins", async (HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                var proxyRequest = await ApiGatewayHelpers.BuildProxyRequestAsync(httpContext, new ProxyRequestOptions(), cancellationToken);
                var credentials = new AssumeRoleAWSCredentials(Environment.GetEnvironmentVariable("AWS_ROLE_ARN")!);
                var lambdaFunction = new Http.Origins.Post.Function(credentials);
                var proxyResponse = await lambdaFunction.DoAsync(proxyRequest, new TestLambdaContext(), cancellationToken);
                return ApiGatewayHelpers.BuildResult(proxyResponse);
            })
            .Accepts<Http.Origins.Post.FunctionRequestBody>("application/json")
            .Produces(204)
            .WithTags("Origins")
            .WithSummary("Create an origin")
            .WithOpenApi();

            return endpoints;
        }
    }
}
