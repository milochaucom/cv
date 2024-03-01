using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities
{
    public class Access : IDynamoDbEntity<Access>
    {
        public const string TableNameSuffix = "accesses";

        public const string K_UserId = "user_id";
        public const string K_ResumeId = "resume_id";

        public required Guid UserId { get; set; }
        public required Guid ResumeId { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append(K_UserId, UserId)
                .Append(K_ResumeId, ResumeId)
                .ToDictionary();
        }

        public static Access ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new Access
            {
                UserId = attributes.ReadGuid(K_UserId),
                ResumeId = attributes.ReadGuid(K_ResumeId),
            };
        }
    }
}
