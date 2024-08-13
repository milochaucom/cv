using Milochau.Core.Aws.Abstractions;
using Milochau.CV.Shared.Entities;
using System;

namespace Milochau.CV.Shared.Data.Models
{
    public class CreateOrUpdateOriginRequest(string originUrl, Guid resumeId, IdentityUser identityUser)
    {
        public string OriginUrl { get; } = originUrl;
        public Guid ResumeId { get; } = resumeId;
        public IdentityUser IdentityUser { get; } = identityUser;
    }
    public class CreateOrUpdateOriginResponse(Origin origin)
    {
        public Origin Origin { get; } = origin;
    }

    public class ReadOriginRequest(string originUrl, IdentityUser? identityUser)
    {
        public string OriginUrl { get; } = originUrl;
        public IdentityUser? IdentityUser { get; } = identityUser;
    }
    public class ReadOriginResponse(Origin? origin)
    {
        public Origin? Origin { get; } = origin;
    }
}
