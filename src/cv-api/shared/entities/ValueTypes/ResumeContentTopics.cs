using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentTopics
    {
        public List<ResumeContentTopicsItem>? Items { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("it", Items?.Select(x => x.FormatForDynamoDb()))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static ResumeContentTopics? ParseFromDynamoDb(Dictionary<string, AttributeValue>? attributes)
        {
            if (attributes == null)
            {
                return null;
            }

            return new ResumeContentTopics
            {
                Items = attributes.ReadListOptional("it")?.Select(x => ResumeContentTopicsItem.ParseFromDynamoDb(x)).ToList(),
            };
        }
    }
}
