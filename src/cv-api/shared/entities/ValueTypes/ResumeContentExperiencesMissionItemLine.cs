using Milochau.Core.Aws.DynamoDB.Abstractions;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContentExperiencesMissionItemLine
    {
        [DynamoDbAttribute("ti")]
        public required string Title { get; set; }
        [DynamoDbAttribute("rp")]
        public bool? RemoveFromPrint { get; set; }
    }
}
