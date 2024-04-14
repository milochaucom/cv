using Milochau.Core.Aws.DynamoDB.Abstractions;
using System.Collections.Generic;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContentExperiences
    {
        [DynamoDbAttribute("it")]
        public required List<ResumeContentExperiencesItem> Items { get; set; }
    }
}
