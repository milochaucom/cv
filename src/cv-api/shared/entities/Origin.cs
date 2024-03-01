using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities
{
    public class Origin : IDynamoDbEntity<Origin>
    {
        public const string TableNameSuffix = "origins";

        public const string K_OriginUrl = "or";
        public const string K_ResumeId = "resume_id";

        public required string OriginUrl { get; set; }
        public required Guid ResumeId { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append(K_OriginUrl, OriginUrl)
                .Append(K_ResumeId, ResumeId)
                .ToDictionary();
        }

        public static Origin ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new Origin
            {
                OriginUrl = attributes.ReadString(K_OriginUrl),
                ResumeId = attributes.ReadGuid(K_ResumeId),
            };
        }
    }
}
