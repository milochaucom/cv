using Milochau.Core.Aws.DynamoDB.Abstractions;
using Milochau.CV.Shared.Entities.ValueTypes;
using System;

namespace Milochau.CV.Shared.Entities
{
    [DynamoDbTable("resumes")]
    public partial class Resume
    {
        public const int MaxFetchItems = 100;

        [DynamoDbPartitionKeyAttribute("id")]
        public required Guid Id { get; set; }
        [DynamoDbSortKeyAttribute("la")]
        public string? Lang { get; set; }

        [DynamoDbAttribute("ui")]
        public required Guid UserId { get; set; }
        [DynamoDbAttribute("cd")]
        public DateTimeOffset Creation { get; set; }

        [DynamoDbAttribute("co")]
        public required ResumeContent Content { get; set; }
    }
}
