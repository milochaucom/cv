using Milochau.Core.Aws.Abstractions;
using Milochau.Core.Aws.DynamoDB;
using Milochau.Core.Aws.DynamoDB.Model;
using Milochau.CV.Shared.Entities;
using System.Threading.Tasks;
using System.Threading;
using System;

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

            var response = await amazonDynamoDB.GetItemAsync(new GetItemRequest<Access>
            {
                UserId = user.UserId,
                PartitionKey = user.UserId,
                SortKey = resumeId,
            }, cancellationToken);

            if (response.Entity == null)
            {
                return result;
            }

            result.Allowed = true;
            return result;
        }
    }
}
