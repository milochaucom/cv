using Milochau.Core.Aws.DynamoDB.Abstractions;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class Icon
    {
        [DynamoDbAttribute("md")]
        public required string Mdi { get; set; }
        [DynamoDbAttribute("un")]
        public string? Unicode { get; set; }
    }
}
