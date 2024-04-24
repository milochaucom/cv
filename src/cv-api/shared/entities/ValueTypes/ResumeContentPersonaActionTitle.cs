using Milochau.Core.Aws.DynamoDB.Abstractions;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContentPersonaActionTitle
    {
        [DynamoDbAttribute("te")]
        public required string Text { get; set; }
        [DynamoDbAttribute("re")]
        public string? Replacement { get; set; }
    }
}
