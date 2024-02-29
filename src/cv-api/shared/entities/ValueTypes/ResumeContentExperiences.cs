using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentExperiences
    {
        public required List<ResumeContentExperiencesItem> Items { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("it", Items.Select(x => x.FormatForDynamoDb()))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static ResumeContentExperiences ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentExperiences
            {
                Items = attributes.ReadList("it").Select(x => ResumeContentExperiencesItem.ParseFromDynamoDb(x)).ToList(),
            };
        }
    }
}
