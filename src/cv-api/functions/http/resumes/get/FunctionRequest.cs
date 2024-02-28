using Microsoft.Extensions.Options;
using Milochau.Core.Aws.Abstractions;
using Milochau.Core.Aws.ApiGateway;
using Milochau.Core.Aws.Core.Lambda.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Milochau.CV.Http.Resumes.Get
{
    public class FunctionRequest(IdentityUser? user) : MaybeAuthenticatedRequest(user), IParsableAndValidatable<FunctionRequest>
    {
        [Required]
        public required string OriginUrl { get; set; }

        [StringLength(16)]
        public string? Lang { get; set; }

        public static bool TryParse(APIGatewayHttpApiV2ProxyRequest request, [NotNullWhen(true)] out FunctionRequest? result)
        {
            result = null;

            if (!request.TryGetHeader("origin", out var originUrl))
            {
                return false;
            }

            if (!request.TryGetIdentityUser(out var user))
            {
                user = null;
            }

            if (!request.TryGetQueryStringParameter("lang", out var lang))
            {
                lang = null; // Nothing here as lang is optional
            }

            result = new FunctionRequest(user)
            {
                OriginUrl = originUrl,
                Lang = lang,
            };

            return true;
        }

        public void Validate(Dictionary<string, Collection<string>> modelStateDictionary)
        {
            modelStateDictionary.UseValidator<FunctionRequestOptionsValidator, FunctionRequest>(this);
        }
    }

    [OptionsValidator]
    public sealed partial class FunctionRequestOptionsValidator : IValidateOptions<FunctionRequest>
    {
    }
}
