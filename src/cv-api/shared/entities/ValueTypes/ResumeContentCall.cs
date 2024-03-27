using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentCall : IDynamoDbEntity<ResumeContentCall>
    {
        public Icon? Icon { get; set; }
        public string? Color { get; set; }
        public required string Message { get; set; }
        public string? Description { get; set; }
        public string? LastUpdate { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("ic", Icon)
                .Append("co", Color)
                .Append("me", Message)
                .Append("de", Description)
                .Append("lu", LastUpdate)
                .ToDictionary();
        }

        public static ResumeContentCall ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentCall
            {
                Icon = attributes.ReadObjectOptional<Icon>("ic"),
                Color = attributes.ReadStringOptional("co"),
                Message = attributes.ReadString("me"),
                Description = attributes.ReadStringOptional("de"),
                LastUpdate = attributes.ReadStringOptional("lu"),
            };
        }
    }
}
