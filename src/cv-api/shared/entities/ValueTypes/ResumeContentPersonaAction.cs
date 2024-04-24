using Milochau.Core.Aws.DynamoDB.Abstractions;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContentPersonaAction
    {
        [DynamoDbAttribute("ti")]
        public required ResumeContentPersonaActionTitle Title { get; set; }
        [DynamoDbAttribute("ic")]
        public required Icon Icon { get; set; }
        [DynamoDbAttribute("hr")]
        public string? Href { get; set; }
    }
}
