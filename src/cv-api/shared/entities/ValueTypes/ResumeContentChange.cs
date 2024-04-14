using Milochau.Core.Aws.DynamoDB.Abstractions;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContentChange
    {
        [DynamoDbAttribute("bu")]
        public bool? Busy { get; set; }
        [DynamoDbAttribute("it")]
        public bool? IsTrialPeriod { get; set; }
        [DynamoDbAttribute("id")]
        public bool? IsDepartureNotice { get; set; }
        [DynamoDbAttribute("cl")]
        public int? ChangeLikelihoodInPercent { get; set; }
        [DynamoDbAttribute("op")]
        public bool? OngoingProcess {  get; set; }
        [DynamoDbAttribute("np")]
        public int? NoticePeriodInDays { get; set; }
        [DynamoDbAttribute("npb")]
        public int? NoticePeriodBufferInDays { get; set; }
        [DynamoDbAttribute("ide")]
        public bool? IsDepartureEndOfMonth { get; set; }
    }
}
