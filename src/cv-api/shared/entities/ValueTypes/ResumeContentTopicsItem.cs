using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentTopicsItem
    {
        public required string Key { get; set; }
        public required string Title { get; set; }
        public string? Color { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("ke", Key)
                .Append("ti", Title)
                .Append("co", Color)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static ResumeContentTopicsItem ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentTopicsItem
            {
                Key = attributes.ReadString("ke"),
                Title = attributes.ReadString("ti"),
                Color = attributes.ReadStringOptional("co"),
            };
        }
    }
}
