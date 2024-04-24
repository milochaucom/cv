using Milochau.Core.Aws.DynamoDB.Abstractions;
using System;

namespace Milochau.CV.Shared.Entities
{
    [DynamoDbTable("origins")]
    public partial class Origin
    {
        [DynamoDbPartitionKeyAttribute("or")]
        public required string OriginUrl { get; set; }

        [DynamoDbAttribute("resume_id")]
        public required Guid ResumeId { get; set; }
    }
}
