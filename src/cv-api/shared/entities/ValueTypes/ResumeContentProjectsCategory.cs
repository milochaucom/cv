using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentProjectsCategory : IDynamoDbEntity<ResumeContentProjectsCategory>
    {
        public required string Title { get; set; }
        public required List<ResumeContentProjectsItem> Items { get; set; }
        public bool? RemoveFromPrint { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("ti", Title)
                .Append("it", Items)
                .Append("rp", RemoveFromPrint)
                .ToDictionary();
        }

        public static ResumeContentProjectsCategory ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentProjectsCategory
            {
                Title = attributes.ReadString("ti"),
                Items = attributes.ReadList<ResumeContentProjectsItem>("it"),
                RemoveFromPrint = attributes.ReadBoolOptional("rp"),
            };
        }
    }
}
