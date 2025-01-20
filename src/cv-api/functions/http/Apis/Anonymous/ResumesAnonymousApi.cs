using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Milochau.CV.Http.Models;
using Milochau.CV.Shared.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Milochau.CV.Http.Apis.Anonymous
{
    public static class ResumesAnonymousApi
    {
        public static IEndpointRouteBuilder MapResumesAnonymousApi(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("/resumes").WithTags("Resumes");

            group.MapGet("", static async Task<Results<NotFound, Ok<ResumesGetResponse>>> (HttpContext context,
                [FromQuery(Name = "origin")] string originUrl,
                [FromQuery(Name = "lang")] string? lang,
                [FromServices] OriginRepository originRepository,
                [FromServices] ResumeRepository resumeRepository,
                CancellationToken cancellationToken) =>
            {
                var origin = await originRepository.ReadOriginAsync(new(originUrl, null), cancellationToken);
                if (origin.Origin == null)
                {
                    return TypedResults.NotFound();
                }

                var resumeResponse = await resumeRepository.ReadResumeAsync(new(origin.Origin.ResumeId, lang, null), cancellationToken);
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


            return endpoints;
        }
    }
}
