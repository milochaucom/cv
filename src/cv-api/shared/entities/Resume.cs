using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using Milochau.CV.Shared.Entities.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities
{
    public class Resume : IDynamoDbEntity<Resume>
    {
        public const string TableNameSuffix = "resumes";
        public const int MaxFetchItems = 100;

        public const string K_Id = "id";
        public const string K_Lang = "la";
        public const string K_UserId = "ui";
        public const string K_Creation = "cd";
        public const string K_Content = "co";

        public required Guid Id { get; set; }
        public string? Lang { get; set; }

        public required Guid UserId { get; set; }
        public DateTimeOffset Creation { get; set; }

        public required ResumeContent Content { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append(K_Id, Id)
                .Append(K_Lang, Lang)
                .Append(K_UserId, UserId)
                .Append(K_Creation, Creation)
                .Append(K_Content, Content)
                .ToDictionary();
        }

        public static Resume ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new Resume
            {
                Id = attributes.ReadGuid(K_Id),
                Lang = attributes.ReadStringOptional(K_Lang),
                UserId = attributes.ReadGuid(K_UserId),
                Creation = attributes.ReadDateTimeOffset(K_Creation),
                Content = attributes.ReadObject<ResumeContent>(K_Content),
            };
        }
    }
}
