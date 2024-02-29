using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentProjectsItem
    {
        public required string Title { get; set; }
        public required Icon Icon { get; set; }
        public string? Href { get; set; }
        public string? Badge { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("ti", Title)
                .Append("ic", Icon.FormatForDynamoDb())
                .Append("hr", Href)
                .Append("ba", Badge)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static ResumeContentProjectsItem ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentProjectsItem
            {
                Title = attributes.ReadString("ti"),
                Icon = Icon.ParseFromDynamoDb(attributes.ReadObject("ic"))!,
                Href = attributes.ReadStringOptional("hr"),
                Badge = attributes.ReadStringOptional("ba"),
            };
        }
    }
}
