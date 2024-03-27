using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentPersonaAction : IDynamoDbEntity<ResumeContentPersonaAction>
    {
        public required ResumeContentPersonaActionTitle Title { get; set; }
        public required Icon Icon { get; set; }
        public string? Href { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("ti", Title)
                .Append("ic", Icon)
                .Append("hr", Href)
                .ToDictionary();
        }

        public static ResumeContentPersonaAction ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentPersonaAction
            {
                Title = attributes.ReadObject<ResumeContentPersonaActionTitle>("ti"),
                Icon = attributes.ReadObject<Icon>("ic"),
                Href = attributes.ReadStringOptional("hr")
            };
        }
    }
}
