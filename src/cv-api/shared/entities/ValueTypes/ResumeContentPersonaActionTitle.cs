using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentPersonaActionTitle
    {
        public required string Text { get; set; }
        public string? Replacement { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("te", Text)
                .Append("re", Replacement)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static ResumeContentPersonaActionTitle ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentPersonaActionTitle
            {
                Text = attributes.ReadString("te"),
                Replacement = attributes.ReadStringOptional("re"),
            };
        }
    }
}
