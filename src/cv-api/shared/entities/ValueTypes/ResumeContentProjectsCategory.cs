using Milochau.Core.Aws.DynamoDB.Abstractions;
using System.Collections.Generic;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContentProjectsCategory
    {
        [DynamoDbAttribute("ti")]
        public required string Title { get; set; }
        [DynamoDbAttribute("it")]
        public required List<ResumeContentProjectsItem> Items { get; set; }
        [DynamoDbAttribute("rp")]
        public bool? RemoveFromPrint { get; set; }
    }
}
