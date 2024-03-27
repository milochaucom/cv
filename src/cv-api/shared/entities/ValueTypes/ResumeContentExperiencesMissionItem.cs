using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentExperiencesMissionItem : IDynamoDbEntity<ResumeContentExperiencesMissionItem>
    {
        public required string Title { get; set; }
        public required Icon Icon { get; set; }
        public required List<ResumeContentExperiencesMissionItemLine> Items { get; set; }
        public bool? RemoveFromPrint { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("ti", Title)
                .Append("ic", Icon)
                .Append("it", Items)
                .Append("rp", RemoveFromPrint)
                .ToDictionary();
        }

        public static ResumeContentExperiencesMissionItem ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentExperiencesMissionItem
            {
                Title = attributes.ReadString("ti"),
                Icon = attributes.ReadObject<Icon>("ic"),
                Items = attributes.ReadList<ResumeContentExperiencesMissionItemLine>("it"),
                RemoveFromPrint = attributes.ReadBoolOptional("rp"),
            };
        }
    }
}
