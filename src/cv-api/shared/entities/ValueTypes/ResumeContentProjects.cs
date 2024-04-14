using Milochau.Core.Aws.DynamoDB.Abstractions;
using System.Collections.Generic;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContentProjects
    {
        [DynamoDbAttribute("it")]
        public required List<ResumeContentProjectsCategory> Items { get; set; }
        [DynamoDbAttribute("ta")]
        public List<ResumeTag>? Tags { get; set; }
    }
}
