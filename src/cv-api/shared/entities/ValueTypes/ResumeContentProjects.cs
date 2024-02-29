using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentProjects
    {
        public required List<ResumeContentProjectsCategory> Items { get; set; }
        public List<ResumeTag>? Tags { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("it", Items.Select(x => x.FormatForDynamoDb()))
                .Append("ta", Tags?.Select(x => x.FormatForDynamoDb()))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static ResumeContentProjects? ParseFromDynamoDb(Dictionary<string, AttributeValue>? attributes)
        {
            if (attributes == null)
            {
                return null;
            }

            return new ResumeContentProjects
            {
                Items = attributes.ReadList("it").Select(x => ResumeContentProjectsCategory.ParseFromDynamoDb(x)).ToList(),
                Tags = attributes.ReadListOptional("ta")?.Select(x => ResumeTag.ParseFromDynamoDb(x)).ToList(),
            };
        }
    }
}
