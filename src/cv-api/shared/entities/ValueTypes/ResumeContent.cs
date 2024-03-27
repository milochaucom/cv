using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContent : IDynamoDbEntity<ResumeContent>
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
                .Append("ca", Call)
                .Append("pe", Persona)
                .Append("to", Topics)
                .Append("pr", Projects)
                .Append("ex", Experiences)
                .Append("tr", Trainings)
                .Append("me", Metrics)
                .Append("ch", Change)
                .ToDictionary();
        }

        public static ResumeContent ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContent
            {
                Call = attributes.ReadObjectOptional<ResumeContentCall>("ca"),
                Persona = attributes.ReadObject<ResumeContentPersona>("pe"),
                Topics = attributes.ReadObjectOptional<ResumeContentTopics>("to"),
                Projects = attributes.ReadObjectOptional<ResumeContentProjects>("pr"),
                Experiences = attributes.ReadObject<ResumeContentExperiences>("ex"),
                Trainings = attributes.ReadObjectOptional<ResumeContentTrainings>("tr"),
                Metrics = attributes.ReadObjectOptional<ResumeContentMetrics>("me"),
                Change = attributes.ReadObjectOptional<ResumeContentChange>("ch"),
            };
        }
    }
}
