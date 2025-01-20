using Microsoft.Extensions.Options;
using System;
using System.ComponentModel.DataAnnotations;

namespace Milochau.CV.Http.Models
{
    [OptionsValidator] public sealed partial class OriginsPostRequestOptionsValidator : IValidateOptions<OriginsPostRequest> { }
    public class OriginsPostRequest
    {
        [Required]
        public required Guid ResumeId { get; set; }

        [Required]
        public required string OriginUrl { get; set; }
    }
}
