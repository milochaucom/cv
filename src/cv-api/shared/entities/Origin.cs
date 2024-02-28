using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities
{
    public class Origin
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
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static Origin ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new Origin
            {
                OriginUrl = attributes.ReadString(K_OriginUrl),
                ResumeId = attributes.ReadGuid(K_ResumeId),
            };
        }

        // @todo Is this required?
        /*public class Origin__Gsi_By_ResumeId_ThenBy_Origin
        {
            public const string IndexName = "by_resume_id_thenby_or";

            public required Guid ResumeId { get; set; }
            public required string OriginUrl { get; set; }

            public static Origin__Gsi_By_ResumeId_ThenBy_Origin ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
            {
                return new Origin__Gsi_By_ResumeId_ThenBy_Origin
                {
                    ResumeId = attributes.ReadGuid(K_ResumeId),
                    OriginUrl = attributes.ReadString(K_OriginUrl),
                };
            }
        }*/
    }
}
