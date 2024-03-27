using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentTrainings : IDynamoDbEntity<ResumeContentTrainings>
    {
        public required List<ResumeContentTrainingsItem> InitialTraining { get; set; }
        public required List<ResumeContentTrainingsItem> ContinuousTraining { get; set; }
        public string? Alumni { get; set; }
        public required List<string> Languages { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("it", InitialTraining)
                .Append("ct", ContinuousTraining)
                .Append("al", Alumni)
                .Append("la", Languages, preserveOrder: true)
                .ToDictionary();
        }

        public static ResumeContentTrainings ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentTrainings
            {
                InitialTraining = attributes.ReadList<ResumeContentTrainingsItem>("it"),
                ContinuousTraining = attributes.ReadList<ResumeContentTrainingsItem>("ct"),
                Alumni = attributes.ReadStringOptional("al"),
                Languages = attributes.ReadListString("la"),
            };
        }
    }
}
