using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Milochau.Core.Aws.ApiGateway;
using Milochau.CV.Http.Models;
using Milochau.CV.Shared.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Milochau.CV.Http.Apis.Authenticated
{
    public static class OriginsAuthenticatedApi
    {
        public static IEndpointRouteBuilder MapOriginsAuthenticatedApi(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("/origins").WithTags("Origins");

            group.MapPost("", static async Task<Results<NotFound, UnauthorizedHttpResult, BadRequest<ValidationProblemDetails>, NoContent>> (HttpContext context,
                [FromBody] OriginsPostRequest request,
                [FromServices] AccessRepository accessRepository,
                [FromServices] OriginRepository originRepository,
                CancellationToken cancellationToken) =>
            {
                if (!ValidationExtensions.TryValidateModel<OriginsPostRequest, OriginsPostRequestOptionsValidator>(request, out var validationProblemDetails))
                {
                    return TypedResults.BadRequest(validationProblemDetails);
                }

                var identityUser = context.CreateIdentityUser();
                if (identityUser == null)
                {
                    return TypedResults.Unauthorized();
                }

                var accessResult = await accessRepository.ReadAccessAsync(new(request.ResumeId, identityUser), cancellationToken);
                if (accessResult.Access == null)
                {
                    return TypedResults.NotFound();
                }

                await originRepository.CreateOrUpdateOriginAsync(new(request.OriginUrl, request.ResumeId, identityUser), cancellationToken);

                return TypedResults.NoContent();
            });

            return endpoints;
        }
    }
}
