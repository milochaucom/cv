using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentProjectsCategory
    {
        public required string Title { get; set; }
        public required List<ResumeContentProjectsItem> Items { get; set; }
        public bool? RemoveFromPrint { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("ti", Title)
                .Append("it", Items.Select(x => x.FormatForDynamoDb()))
                .Append("rp", RemoveFromPrint)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static ResumeContentProjectsCategory ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentProjectsCategory
            {
                Title = attributes.ReadString("ti"),
                Items = attributes.ReadList("it").Select(x => ResumeContentProjectsItem.ParseFromDynamoDb(x)).ToList(),
                RemoveFromPrint = attributes.ReadBoolOptional("rp"),
            };
        }
    }
}
