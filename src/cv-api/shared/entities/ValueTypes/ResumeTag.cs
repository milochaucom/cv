using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeTag : IDynamoDbEntity<ResumeTag>
    {
        public required string Label { get; set; }
        public required string Key { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("la", Label)
                .Append("ke", Key)
                .ToDictionary();
        }

        public static ResumeTag ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeTag
            {
                Label = attributes.ReadString("la"),
                Key = attributes.ReadString("ke"),
            };
        }
    }
}
