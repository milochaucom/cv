using Milochau.Core.Aws.Abstractions;
using Milochau.CV.Shared.Entities;
using Milochau.CV.Shared.Entities.ValueTypes;
using System;

namespace Milochau.CV.Shared.Data.Models
{
    public class CreateOrUpdateResumeRequest(Guid resumeId, string? lang, ResumeContent resumeContent, IdentityUser identityUser)
    {
        public Guid ResumeId { get; } = resumeId;
        public string? Lang { get; } = lang;
        public ResumeContent ResumeContent { get; } = resumeContent;
        public IdentityUser IdentityUser { get; } = identityUser;
    }
    public class CreateOrUpdateResumeResponse(Resume resume)
    {
        public Resume Resume { get; } = resume;
    }

    public class GetResumeRequest(Guid resumeId, string? lang, IdentityUser? identityUser)
    {
        public Guid ResumeId { get; } = resumeId;
        public string? Lang { get; } = lang;
        public IdentityUser? IdentityUser { get; } = identityUser;
    }
    public class GetResumeResponse(Resume? resume)
    {
        public Resume? Resume { get; } = resume;
    }
}
