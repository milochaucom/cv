using Microsoft.Extensions.Options;
using Milochau.Core.Aws.Abstractions;
using Milochau.Core.Aws.ApiGateway;
using Milochau.Core.Aws.Core.Lambda.Events;
using Milochau.CV.Shared.Entities.ValueTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Milochau.CV.Http.Resumes.Post
{
    public class FunctionRequestBody
    {
        [Required]
        public required ResumeContent Content { get; set; }
    }

    public class FunctionRequest(IdentityUser user) : AuthenticatedRequest(user), IParsableAndValidatable<FunctionRequest>
    {
        [Required]
        public required Guid ResumeId { get; set; }

        [StringLength(16)]
        public string? Lang { get; set; }

        [ValidateObjectMembers]
        public required FunctionRequestBody Body { get; set; }

        public static bool TryParse(APIGatewayHttpApiV2ProxyRequest request, [NotNullWhen(true)] out FunctionRequest? result)
        {
            result = null;

            if (!request.TryGetIdentityUser(out var user)
                || !request.TryGetPathParameter("resumeId", out var rawResumeId) || !Guid.TryParse(rawResumeId, out var resumeId)
                || !request.TryDeserializeBody(out var body, ApplicationJsonSerializerContext.Default.FunctionRequestBody))
            {
                return false;
            }

            if (!request.TryGetQueryStringParameter("lang", out var lang))
            {
                lang = null; // Nothing here as lang is optional
            }

            result = new FunctionRequest(user)
            {
                User = user,
                Body = body,
                ResumeId = resumeId,
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
