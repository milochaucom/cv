using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentTrainingsItem
    {
        public required string Title { get; set; }
        public string? Subtitle { get; set; }
        public Icon? Icon { get; set; }
        public string? Date { get; set; }
        public string? Href { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("ti", Title)
                .Append("st", Subtitle)
                .Append("ic", Icon?.FormatForDynamoDb())
                .Append("da", Date)
                .Append("hr", Href)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static ResumeContentTrainingsItem ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentTrainingsItem
            {
                Title = attributes.ReadString("ti"),
                Subtitle = attributes.ReadStringOptional("st"),
                Icon = Icon.ParseFromDynamoDb(attributes.ReadObjectOptional("ic")),
                Date = attributes.ReadStringOptional("da"),
                Href = attributes.ReadStringOptional("hr"),
            };
        }
    }
}
