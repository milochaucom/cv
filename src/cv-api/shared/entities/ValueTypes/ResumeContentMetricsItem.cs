using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentMetricsItem
    {
        public required string Title { get; set; }
        public required double Number { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("ti", Title)
                .Append("nu", Number)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static ResumeContentMetricsItem ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentMetricsItem
            {
                Title = attributes.ReadString("ti"),
                Number = attributes.ReadDouble("nu"),
            };
        }
    }
}
