using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentChange : IDynamoDbEntity<ResumeContentChange>
    {
        public bool? Busy { get; set; }
        public bool? IsTrialPeriod { get; set; }
        public bool? IsDepartureNotice { get; set; }
        public int? ChangeLikelihoodInPercent { get; set; }
        public bool? OngoingProcess {  get; set; }
        public int? NoticePeriodInDays { get; set; }
        public int? NoticePeriodBufferInDays { get; set; }
        public bool? IsDepartureEndOfMonth { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("bu", Busy)
                .Append("it", IsTrialPeriod)
                .Append("id", IsDepartureNotice)
                .Append("cl", ChangeLikelihoodInPercent)
                .Append("op", OngoingProcess)
                .Append("np", NoticePeriodInDays)
                .Append("npb", NoticePeriodBufferInDays)
                .Append("ide", IsDepartureEndOfMonth)
                .ToDictionary();
        }

        public static ResumeContentChange ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentChange
            {
                Busy = attributes.ReadBoolOptional("bu"),
                IsTrialPeriod = attributes.ReadBoolOptional("it"),
                IsDepartureNotice = attributes.ReadBoolOptional("id"),
                ChangeLikelihoodInPercent = attributes.ReadIntOptional("cl"),
                OngoingProcess = attributes.ReadBoolOptional("op"),
                NoticePeriodInDays = attributes.ReadIntOptional("np"),
                NoticePeriodBufferInDays = attributes.ReadIntOptional("npb"),
                IsDepartureEndOfMonth = attributes.ReadBoolOptional("ide"),
            };
        }
    }
}
