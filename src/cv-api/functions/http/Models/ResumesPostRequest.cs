using Microsoft.Extensions.Options;
using Milochau.CV.Shared.Entities.ValueTypes;
using System.ComponentModel.DataAnnotations;

namespace Milochau.CV.Http.Models
{
    [OptionsValidator] public sealed partial class ResumesPostRequestOptionsValidator : IValidateOptions<ResumesPostRequest> { }
    public class ResumesPostRequest
    {
        [Required]
        public required ResumeContent Content { get; set; }
    }
}
