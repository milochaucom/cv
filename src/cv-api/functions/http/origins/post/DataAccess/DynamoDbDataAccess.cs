using Milochau.Core.Aws.Abstractions;
using Milochau.Core.Aws.DynamoDB;
using Milochau.Core.Aws.DynamoDB.Model;
using Milochau.CV.Shared.DynamoDB;
using Milochau.CV.Shared.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Milochau.CV.Http.Origins.Post.DataAccess
{
    public interface IDynamoDbDataAccess
    {
        Task<AccessResult> CheckAccessAsync(IdentityUser user, Guid resumeId, CancellationToken cancellationToken);
        Task CreateOrUpdateOriginAsync(FunctionRequest request, CancellationToken cancellationToken);
    }

    public class DynamoDbDataAccess : BaseDynamoDbDataAccess, IDynamoDbDataAccess
    {
        public DynamoDbDataAccess(IAmazonDynamoDB amazonDynamoDB) : base(amazonDynamoDB)
        {
        }

        public async Task CreateOrUpdateOriginAsync(FunctionRequest request, CancellationToken cancellationToken)
        {
            var origin = new Origin
            {
                OriginUrl = request.Body.OriginUrl,
                ResumeId = request.Body.ResumeId,
            };

            await amazonDynamoDB.PutItemAsync(new PutItemRequest<Origin>
            {
                UserId = request.User.UserId,
                Entity = origin,
            }, cancellationToken);
        }
    }
}
