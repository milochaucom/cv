using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Milochau.Core.Aws.Abstractions;
using Milochau.Core.Aws.ApiGateway;
using Milochau.CV.Shared.Data;
using System.ComponentModel.DataAnnotations;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Milochau.CV.Http.Apis
{
    public static class OriginsPost
    {
        public static async Task<Results<NotFound, BadRequest<ValidationProblemDetails>, NoContent>> DelegateAuthenticated(
            [AsParameters] OriginsPostParameters parameters,
            [FromServices] AccessRepository accessRepository,
            [FromServices] OriginRepository originRepository,
            [FromServices] IdentityUser identityUser,
            CancellationToken cancellationToken)
        {
            if (!ValidationExtensions.TryValidateModel<OriginsPostParameters, OriginsPostParametersOptionsValidator>(parameters, out var validationProblemDetails))
            {
                return TypedResults.BadRequest(validationProblemDetails);
            }

            var accessResult = await accessRepository.ReadAccessAsync(new(parameters.Body.ResumeId, identityUser), cancellationToken);
            if (accessResult.Access == null)
            {
                return TypedResults.NotFound();
            }

            await originRepository.CreateOrUpdateOriginAsync(new(parameters.Body.OriginUrl, parameters.Body.ResumeId, identityUser), cancellationToken);

            return TypedResults.NoContent();
        }
    }

    [OptionsValidator] public sealed partial class OriginsPostParametersOptionsValidator : IValidateOptions<OriginsPostParameters> { }
    public class OriginsPostParameters
    {
        [FromBody]
        [NotNull]
        [ValidateObjectMembers]
        [Required]
        public OriginsPostBody? Body { get; set; }
    }

    public class OriginsPostBody
    {
        [Required]
        public required Guid ResumeId { get; set; }

        [Required]
        public required string OriginUrl { get; set; }
    }
}
