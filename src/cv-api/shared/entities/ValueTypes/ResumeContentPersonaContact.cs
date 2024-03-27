using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentPersonaContact : IDynamoDbEntity<ResumeContentPersonaContact>
    {
        public required string Text { get; set; }
        public required string Url { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("te", Text)
                .Append("ur", Url)
                .ToDictionary();
        }

        public static ResumeContentPersonaContact ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentPersonaContact
            {
                Text = attributes.ReadString("te"),
                Url = attributes.ReadString("ur"),
            };
        }
    }
}
