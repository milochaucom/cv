using Milochau.Core.Aws.Abstractions;
using Milochau.Core.Aws.Core.References;
using Milochau.Core.Aws.DynamoDB;
using Milochau.Core.Aws.DynamoDB.Model;
using Milochau.CV.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using Milochau.Core.Aws.DynamoDB.Helpers;
using System.Linq;

namespace Milochau.CV.Shared.DynamoDB
{
    public abstract class BaseDynamoDbDataAccess
    {
        protected readonly IAmazonDynamoDB amazonDynamoDB;

        public BaseDynamoDbDataAccess(IAmazonDynamoDB amazonDynamoDB)
        {
            this.amazonDynamoDB = amazonDynamoDB;
        }

        public async Task<AccessResult> CheckAccessAsync(IdentityUser user, Guid resumeId, CancellationToken cancellationToken)
        {
            var result = new AccessResult();

            var response = await amazonDynamoDB.GetItemAsync(new GetItemRequest(user.UserId)
            {
                TableName = $"{EnvironmentVariables.ConventionPrefix}-table-{Access.TableNameSuffix}",
                Key = new Dictionary<string, AttributeValue>()
                    .Append(Access.K_UserId, user.UserId)
                    .Append(Access.K_ResumeId, resumeId)
                    .ToDictionary(x => x.Key, x => x.Value),
            }, cancellationToken);

            if (response.Item == null || response.Item.Count == 0)
            {
                return result;
            }

            result.Allowed = true;
            return result;
        }
    }
}
