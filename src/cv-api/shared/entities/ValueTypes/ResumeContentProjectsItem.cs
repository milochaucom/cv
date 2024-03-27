using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentProjectsItem : IDynamoDbEntity<ResumeContentProjectsItem>
    {
        public required string Title { get; set; }
        public required Icon Icon { get; set; }
        public string? Href { get; set; }
        public string? Badge { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("ti", Title)
                .Append("ic", Icon)
                .Append("hr", Href)
                .Append("ba", Badge)
                .ToDictionary();
        }

        public static ResumeContentProjectsItem ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentProjectsItem
            {
                Title = attributes.ReadString("ti"),
                Icon = attributes.ReadObject<Icon>("ic"),
                Href = attributes.ReadStringOptional("hr"),
                Badge = attributes.ReadStringOptional("ba"),
            };
        }
    }
}
