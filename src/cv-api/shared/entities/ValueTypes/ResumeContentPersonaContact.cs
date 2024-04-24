using Milochau.Core.Aws.DynamoDB.Abstractions;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContentPersonaContact
    {
        [DynamoDbAttribute("te")]
        public required string Text { get; set; }
        [DynamoDbAttribute("ur")]
        public required string Url { get; set; }
    }
}
