using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class Icon
    {
        public required string Mdi { get; set; }
        public string? Unicode { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("md", Mdi)
                .Append("un", Unicode)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static Icon? ParseFromDynamoDb(Dictionary<string, AttributeValue>? attributes)
        {
            if (attributes == null)
            {
                return null;
            }

            return new Icon
            {
                Mdi = attributes.ReadString("md"),
                Unicode = attributes.ReadStringOptional("un"),
            };
        }
    }
}
