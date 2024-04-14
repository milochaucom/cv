using Milochau.Core.Aws.DynamoDB.Abstractions;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContentProjectsItem
    {
        [DynamoDbAttribute("ti")]
        public required string Title { get; set; }
        [DynamoDbAttribute("ic")]
        public required Icon Icon { get; set; }
        [DynamoDbAttribute("hr")]
        public string? Href { get; set; }
        [DynamoDbAttribute("ba")]
        public string? Badge { get; set; }
    }
}
