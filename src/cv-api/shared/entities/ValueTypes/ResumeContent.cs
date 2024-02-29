using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContent
    {
        public ResumeContentCall? Call { get; set; }
        public required ResumeContentPersona Persona { get; set; }
        public ResumeContentTopics? Topics { get; set; }
        public ResumeContentProjects? Projects { get; set; }
        public required ResumeContentExperiences Experiences { get; set; }
        public ResumeContentTrainings? Trainings { get; set; }
        public ResumeContentMetrics? Metrics { get; set; }
        public ResumeContentChange? Change { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("ca", Call?.FormatForDynamoDb())
                .Append("pe", Persona.FormatForDynamoDb())
                .Append("to", Topics?.FormatForDynamoDb())
                .Append("pr", Projects?.FormatForDynamoDb())
                .Append("ex", Experiences.FormatForDynamoDb())
                .Append("tr", Trainings?.FormatForDynamoDb())
                .Append("me", Metrics?.FormatForDynamoDb())
                .Append("ch", Change?.FormatForDynamoDb())
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static ResumeContent ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContent
            {
                Call = ResumeContentCall.ParseFromDynamoDb(attributes.ReadObjectOptional("ca")),
                Persona = ResumeContentPersona.ParseFromDynamoDb(attributes.ReadObject("pe")),
                Topics = ResumeContentTopics.ParseFromDynamoDb(attributes.ReadObjectOptional("to")),
                Projects = ResumeContentProjects.ParseFromDynamoDb(attributes.ReadObjectOptional("pr")),
                Experiences = ResumeContentExperiences.ParseFromDynamoDb(attributes.ReadObject("ex")),
                Trainings = ResumeContentTrainings.ParseFromDynamoDb(attributes.ReadObjectOptional("tr")),
                Metrics = ResumeContentMetrics.ParseFromDynamoDb(attributes.ReadObjectOptional("me")),
                Change = ResumeContentChange.ParseFromDynamoDb(attributes.ReadObjectOptional("ch")),
            };
        }
    }
}
