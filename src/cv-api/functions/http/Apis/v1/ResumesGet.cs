using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Milochau.Core.Aws.Abstractions;
using Milochau.Core.Aws.ApiGateway;
using Milochau.CV.Shared.Data;
using Milochau.CV.Shared.Entities.ValueTypes;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Milochau.CV.Http.Apis.v1
{
    public static class ResumesGet
    {
        public static async Task<Results<NotFound, BadRequest<ValidationProblemDetails>, Ok<ResumesGetResponse>>> DelegateAnonymous(
            [AsParameters] ResumesGetParameters parameters,
            [FromServices] OriginRepository originRepository,
            [FromServices] ResumeRepository resumeRepository,
            CancellationToken cancellationToken)
        {
            if (!ValidationExtensions.TryValidateModel<ResumesGetParameters, ResumesGetParametersOptionsValidator>(parameters, out var validationProblemDetails))
            {
                return TypedResults.BadRequest(validationProblemDetails);
            }

            var origin = await originRepository.ReadOriginAsync(new(parameters.OriginUrl, null), cancellationToken);
            if (origin.Origin == null)
            {
                return TypedResults.NotFound();
            }

            var resumeResponse = await resumeRepository.ReadResumeAsync(new(origin.Origin.ResumeId, parameters.Lang, null), cancellationToken);
            if (resumeResponse.Resume == null)
            {
                return TypedResults.NotFound();
            }

            var response = new ResumesGetResponse
            {
                Content = resumeResponse.Resume.Content,
            };

            return TypedResults.Ok(response);
        }


        public static async Task<Results<NotFound, UnauthorizedHttpResult, BadRequest<ValidationProblemDetails>, Ok<ResumesGetResponse>>> DelegateAuthenticated(
            [AsParameters] ResumesGetParameters parameters,
            [FromServices] OriginRepository originRepository,
            [FromServices] ResumeRepository resumeRepository,
            [FromServices] IdentityUser identityUser,
            CancellationToken cancellationToken)
        {
            if (!ValidationExtensions.TryValidateModel<ResumesGetParameters, ResumesGetParametersOptionsValidator>(parameters, out var validationProblemDetails))
            {
                return TypedResults.BadRequest(validationProblemDetails);
            }

            var origin = await originRepository.ReadOriginAsync(new(parameters.OriginUrl, identityUser), cancellationToken);
            if (origin.Origin == null)
            {
                return TypedResults.NotFound();
            }

            var resumeResponse = await resumeRepository.ReadResumeAsync(new(origin.Origin.ResumeId, parameters.Lang, identityUser), cancellationToken);
            if (resumeResponse.Resume == null)
            {
                return TypedResults.NotFound();
            }

            var response = new ResumesGetResponse
            {
                Content = resumeResponse.Resume.Content,
            };
            return TypedResults.Ok(response);
        }
    }

    [OptionsValidator] public sealed partial class ResumesGetParametersOptionsValidator : IValidateOptions<ResumesGetParameters> { }
    public class ResumesGetParameters
    {
        [FromQuery(Name = "origin")]
        [Required]
        public required string OriginUrl { get; set; }

        [FromQuery(Name = "lang")]
        public string? Lang { get; set; }
    }

    public class ResumesGetResponse
    {
        public required ResumeContent Content { get; set; }
    }
}
