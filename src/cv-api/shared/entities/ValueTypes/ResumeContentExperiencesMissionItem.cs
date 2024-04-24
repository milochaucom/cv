using Milochau.Core.Aws.DynamoDB.Abstractions;
using System.Collections.Generic;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContentExperiencesMissionItem
    {
        [DynamoDbAttribute("ti")]
        public required string Title { get; set; }
        [DynamoDbAttribute("ic")]
        public required Icon Icon { get; set; }
        [DynamoDbAttribute("it")]
        public required List<ResumeContentExperiencesMissionItemLine> Items { get; set; }
        [DynamoDbAttribute("rp")]
        public bool? RemoveFromPrint { get; set; }
    }
}
