using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentExperiencesMissionItem
    {
        public required string Title { get; set; }
        public required Icon Icon { get; set; }
        public required List<ResumeContentExperiencesMissionItemLine> Items { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("ti", Title)
                .Append("ic", Icon.FormatForDynamoDb())
                .Append("it", Items.Select(x => x.FormatForDynamoDb()))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static ResumeContentExperiencesMissionItem ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentExperiencesMissionItem
            {
                Title = attributes.ReadString("ti"),
                Icon = Icon.ParseFromDynamoDb(attributes.ReadObject("ic"))!,
                Items = attributes.ReadList("it").Select(x => ResumeContentExperiencesMissionItemLine.ParseFromDynamoDb(x)).ToList(),
            };
        }
    }
}
