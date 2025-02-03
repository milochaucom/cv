using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Milochau.Core.Aws.ApiGateway;
using Milochau.CV.Shared.Data;
using System.Threading.Tasks;
using System.Threading;
using Milochau.Core.Aws.Abstractions;
using System;
using Microsoft.Extensions.Options;
using Milochau.CV.Shared.Entities.ValueTypes;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Milochau.CV.Http.Apis.v1
{
    public static class ResumesPost
    {
        public static async Task<Results<NotFound, BadRequest<ValidationProblemDetails>, NoContent>> DelegateAuthenticated(
            [AsParameters] ResumesPostParameters parameters,
            [FromServices] AccessRepository accessRepository,
            [FromServices] ResumeRepository resumeRepository,
            [FromServices] IdentityUser identityUser,
            CancellationToken cancellationToken)
        {
            if (!ValidationExtensions.TryValidateModel<ResumesPostParameters, ResumesPostParametersOptionsValidator>(parameters, out var validationProblemDetails))
            {
                return TypedResults.BadRequest(validationProblemDetails);
            }

            var accessResult = await accessRepository.ReadAccessAsync(new(parameters.ResumeId, identityUser), cancellationToken);
            if (accessResult.Access == null)
            {
                return TypedResults.NotFound();
            }

            await resumeRepository.CreateOrUpdateResumeAsync(new(parameters.ResumeId, parameters.Lang, parameters.Body.Content, identityUser), cancellationToken);

            return TypedResults.NoContent();
        }
    }

    [OptionsValidator] public sealed partial class ResumesPostParametersOptionsValidator : IValidateOptions<ResumesPostParameters> { }
    public class ResumesPostParameters
    {
        [FromRoute(Name = "resumeId")]
        [Required]
        public required Guid ResumeId { get; set; }

        [FromQuery(Name = "lang")]
        public string? Lang { get; set; }

        [FromBody]
        [NotNull]
        [ValidateObjectMembers]
        [Required]
        public ResumesPostBody? Body { get; set; }
    }

    public class ResumesPostBody
    {
        [Required]
        public required ResumeContent Content { get; set; }
    }
}
