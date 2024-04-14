using Milochau.Core.Aws.DynamoDB.Abstractions;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContentTopicsItem
    {
        [DynamoDbAttribute("ke")]
        public required string Key { get; set; }
        [DynamoDbAttribute("ti")]
        public required string Title { get; set; }
        [DynamoDbAttribute("co")]
        public string? Color { get; set; }
    }
}
