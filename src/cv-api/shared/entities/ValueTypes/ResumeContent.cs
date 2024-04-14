using Milochau.Core.Aws.DynamoDB.Abstractions;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContent
    {
        [DynamoDbAttribute("ca")]
        public ResumeContentCall? Call { get; set; }
        [DynamoDbAttribute("pe")]
        public required ResumeContentPersona Persona { get; set; }
        [DynamoDbAttribute("to")]
        public ResumeContentTopics? Topics { get; set; }
        [DynamoDbAttribute("pr")]
        public ResumeContentProjects? Projects { get; set; }
        [DynamoDbAttribute("ex")]
        public required ResumeContentExperiences Experiences { get; set; }
        [DynamoDbAttribute("tr")]
        public ResumeContentTrainings? Trainings { get; set; }
        [DynamoDbAttribute("me")]
        public ResumeContentMetrics? Metrics { get; set; }
        [DynamoDbAttribute("ch")]
        public ResumeContentChange? Change { get; set; }
    }
}
