using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentProjects : IDynamoDbEntity<ResumeContentProjects>
    {
        public required List<ResumeContentProjectsCategory> Items { get; set; }
        public List<ResumeTag>? Tags { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("it", Items)
                .Append("ta", Tags)
                .ToDictionary();
        }

        public static ResumeContentProjects ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentProjects
            {
                Items = attributes.ReadList<ResumeContentProjectsCategory>("it"),
                Tags = attributes.ReadListOptional<ResumeTag>("ta"),
            };
        }
    }
}
