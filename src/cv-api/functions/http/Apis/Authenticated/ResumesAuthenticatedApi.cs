using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Milochau.Core.Aws.ApiGateway;
using Milochau.CV.Http.Models;
using Milochau.CV.Shared.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Milochau.CV.Http.Apis.Authenticated
{
    public static class ResumesAuthenticatedApi
    {
        public static IEndpointRouteBuilder MapResumesAuthenticatedApi(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("/resumes").WithTags("Resumes");

            group.MapGet("", static async Task<Results<NotFound, UnauthorizedHttpResult, Ok<ResumesGetResponse>>> (HttpContext context,
                [FromQuery(Name = "origin")] string originUrl,
                [FromQuery(Name = "lang")] string? lang,
                [FromServices] OriginRepository originRepository,
                [FromServices] ResumeRepository resumeRepository,
                CancellationToken cancellationToken) =>
            {
                var identityUser = context.CreateIdentityUser();
                if (identityUser == null)
                {
                    return TypedResults.Unauthorized();
                }

                var origin = await originRepository.ReadOriginAsync(new(originUrl, identityUser), cancellationToken);
                if (origin.Origin == null)
                {
                    return TypedResults.NotFound();
                }

                var resumeResponse = await resumeRepository.ReadResumeAsync(new(origin.Origin.ResumeId, lang, identityUser), cancellationToken);
                if (resumeResponse.Resume == null)
                {
                    return TypedResults.NotFound();
                }

                var response = new ResumesGetResponse
                {
                    Content = resumeResponse.Resume.Content,
                };
                return TypedResults.Ok(response);
            });

            group.MapPost("/{resumeId}", static async Task<Results<NotFound, UnauthorizedHttpResult, BadRequest<ValidationProblemDetails>, NoContent>> (HttpContext context,
                [FromRoute] Guid resumeId,
                [FromQuery(Name = "lang")] string? lang,
                [FromBody] ResumesPostRequest request,
                [FromServices] AccessRepository accessRepository,
                [FromServices] ResumeRepository resumeRepository,
                CancellationToken cancellationToken) =>
            {
                if (!ValidationExtensions.TryValidateModel<ResumesPostRequest, ResumesPostRequestOptionsValidator>(request, out var validationProblemDetails))
                {
                    return TypedResults.BadRequest(validationProblemDetails);
                }

                var identityUser = context.CreateIdentityUser();
                if (identityUser == null)
                {
                    return TypedResults.Unauthorized();
                }

                var accessResult = await accessRepository.ReadAccessAsync(new(resumeId, identityUser), cancellationToken);
                if (accessResult.Access == null)
                {
                    return TypedResults.NotFound();
                }

                await resumeRepository.CreateOrUpdateResumeAsync(new(resumeId, lang, request.Content, identityUser), cancellationToken);

                return TypedResults.NoContent();
            });

            return endpoints;
        }
    }
}
