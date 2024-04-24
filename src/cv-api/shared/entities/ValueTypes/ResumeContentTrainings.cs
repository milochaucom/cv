using Milochau.Core.Aws.DynamoDB.Abstractions;
using System.Collections.Generic;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContentTrainings
    {
        [DynamoDbAttribute("it")]
        public required List<ResumeContentTrainingsItem> InitialTraining { get; set; }
        [DynamoDbAttribute("ct")]
        public required List<ResumeContentTrainingsItem> ContinuousTraining { get; set; }
        [DynamoDbAttribute("al")]
        public string? Alumni { get; set; }
        [DynamoDbAttribute("la")]
        public required List<string> Languages { get; set; }
    }
}
