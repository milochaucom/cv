using Milochau.Core.Aws.DynamoDB.Abstractions;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContentCall
    {
        [DynamoDbAttribute("ic")]
        public Icon? Icon { get; set; }
        [DynamoDbAttribute("co")]
        public string? Color { get; set; }
        [DynamoDbAttribute("me")]
        public required string Message { get; set; }
        [DynamoDbAttribute("de")]
        public string? Description { get; set; }
        [DynamoDbAttribute("lu")]
        public string? LastUpdate { get; set; }
    }
}
