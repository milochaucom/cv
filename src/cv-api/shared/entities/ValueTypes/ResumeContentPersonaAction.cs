using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentPersonaAction
    {
        public required ResumeContentPersonaActionTitle Title { get; set; }
        public required Icon Icon { get; set; }
        public string? Href { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("ti", Title.FormatForDynamoDb())
                .Append("ic", Icon.FormatForDynamoDb())
                .Append("hr", Href)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static ResumeContentPersonaAction ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentPersonaAction
            {
                Title = ResumeContentPersonaActionTitle.ParseFromDynamoDb(attributes.ReadObject("ti")),
                Icon = Icon.ParseFromDynamoDb(attributes.ReadObject("ic"))!,
                Href = attributes.ReadStringOptional("hr")
            };
        }
    }
}
