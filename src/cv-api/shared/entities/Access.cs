using Milochau.Core.Aws.DynamoDB.Abstractions;
using System;

namespace Milochau.CV.Shared.Entities
{
    [DynamoDbTable("accesses")]
    public partial class Access
    {
        [DynamoDbPartitionKeyAttribute("user_id")]
        public required Guid UserId { get; set; }

        [DynamoDbSortKeyAttribute("resume_id")]
        public required Guid ResumeId { get; set; }
    }
}
