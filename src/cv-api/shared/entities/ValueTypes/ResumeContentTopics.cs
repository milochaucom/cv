using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentTopics : IDynamoDbEntity<ResumeContentTopics>
    {
        public List<ResumeContentTopicsItem>? Items { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("it", Items)
                .ToDictionary();
        }

        public static ResumeContentTopics ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentTopics
            {
                Items = attributes.ReadListOptional<ResumeContentTopicsItem>("it"),
            };
        }
    }
}
