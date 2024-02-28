using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContent
    {
        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static ResumeContent ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContent
            {
            };
        }
    }
}
