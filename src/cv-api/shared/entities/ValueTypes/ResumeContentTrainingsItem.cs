using Milochau.Core.Aws.DynamoDB.Abstractions;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContentTrainingsItem
    {
        [DynamoDbAttribute("ti")]
        public required string Title { get; set; }
        [DynamoDbAttribute("st")]
        public string? Subtitle { get; set; }
        [DynamoDbAttribute("ic")]
        public Icon? Icon { get; set; }
        [DynamoDbAttribute("da")]
        public string? Date { get; set; }
        [DynamoDbAttribute("hr")]
        public string? Href { get; set; }
    }
}
