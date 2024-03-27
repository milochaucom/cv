using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentMetrics : IDynamoDbEntity<ResumeContentMetrics>
    {
        public required List<ResumeContentMetricsItem> Items { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("it", Items)
                .ToDictionary();
        }

        public static ResumeContentMetrics ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentMetrics
            {
                Items = attributes.ReadList<ResumeContentMetricsItem>("it"),
            };
        }
    }
}
