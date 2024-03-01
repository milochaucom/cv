using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentExperiencesMissionItemLine : IDynamoDbEntity<ResumeContentExperiencesMissionItemLine>
    {
        public required string Title { get; set; }
        public bool? RemoveFromPrint { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("ti", Title)
                .Append("rp", RemoveFromPrint)
                .ToDictionary();
        }

        public static ResumeContentExperiencesMissionItemLine ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentExperiencesMissionItemLine
            {
                Title = attributes.ReadString("ti"),
                RemoveFromPrint = attributes.ReadBoolOptional("rp"),
            };
        }
    }
}
