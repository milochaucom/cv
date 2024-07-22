using Milochau.Core.Aws.Abstractions;
using Milochau.CV.Shared.Entities;
using System;

namespace Milochau.CV.Shared.Data.Models
{
    public class GetAccessRequest(Guid resumeId, IdentityUser identityUser)
    {
        public Guid ResumeId { get; } = resumeId;
        public IdentityUser IdentityUser { get; } = identityUser;
    }
    public class GetAccessResponse(Access? access)
    {
        public Access? Access { get; } = access;
    }
}
