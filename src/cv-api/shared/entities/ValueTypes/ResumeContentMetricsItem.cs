using Milochau.Core.Aws.DynamoDB.Abstractions;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContentMetricsItem
    {
        [DynamoDbAttribute("ti")]
        public required string Title { get; set; }
        [DynamoDbAttribute("nu")]
        public required double Number { get; set; }
    }
}
