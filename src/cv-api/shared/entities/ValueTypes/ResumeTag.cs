using Milochau.Core.Aws.DynamoDB.Abstractions;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeTag
    {
        [DynamoDbAttribute("la")]
        public required string Label { get; set; }
        [DynamoDbAttribute("ke")]
        public required string Key { get; set; }
    }
}
