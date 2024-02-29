using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentTrainings
    {
        public required List<ResumeContentTrainingsItem> InitialTraining { get; set; }
        public required List<ResumeContentTrainingsItem> ContinuousTraining { get; set; }
        public string? Alumni { get; set; }
        public required List<string> Languages { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("it", InitialTraining.Select(x => x.FormatForDynamoDb()))
                .Append("ct", ContinuousTraining.Select(x => x.FormatForDynamoDb()))
                .Append("al", Alumni)
                .Append("la", Languages, preserveOrder: true)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static ResumeContentTrainings? ParseFromDynamoDb(Dictionary<string, AttributeValue>? attributes)
        {
            if (attributes == null)
            {
                return null;
            }

            return new ResumeContentTrainings
            {
                InitialTraining = attributes.ReadList("it").Select(x => ResumeContentTrainingsItem.ParseFromDynamoDb(x)).ToList(),
                ContinuousTraining = attributes.ReadList("ct").Select(x => ResumeContentTrainingsItem.ParseFromDynamoDb(x)).ToList(),
                Alumni = attributes.ReadStringOptional("al"),
                Languages = attributes.ReadListString("la"),
            };
        }
    }
}
