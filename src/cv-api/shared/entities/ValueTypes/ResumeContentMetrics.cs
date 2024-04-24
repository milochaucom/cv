using Milochau.Core.Aws.DynamoDB.Abstractions;
using System.Collections.Generic;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContentMetrics
    {
        [DynamoDbAttribute("it")]
        public required List<ResumeContentMetricsItem> Items { get; set; }
    }
}
