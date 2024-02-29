using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentMetrics
    {
        public required List<ResumeContentMetricsItem> Items { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("it", Items.Select(x => x.FormatForDynamoDb()))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static ResumeContentMetrics? ParseFromDynamoDb(Dictionary<string, AttributeValue>? attributes)
        {
            if (attributes == null)
            {
                return null;
            }

            return new ResumeContentMetrics
            {
                Items = attributes.ReadList("it").Select(x => ResumeContentMetricsItem.ParseFromDynamoDb(x)).ToList(),
            };
        }
    }
}
