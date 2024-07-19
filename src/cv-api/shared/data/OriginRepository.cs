using Milochau.Core.Aws.DynamoDB;
using Milochau.Core.Aws.DynamoDB.Model;
using Milochau.CV.Shared.Data.Models;
using Milochau.CV.Shared.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Milochau.CV.Shared.Data
{
    public class OriginRepository(IAmazonDynamoDB amazonDynamoDB)
    {
        public async Task<CreateOrUpdateOriginResponse> CreateOrUpdateOriginAsync(CreateOrUpdateOriginRequest request, CancellationToken cancellationToken)
        {
            var origin = new Origin
            {
                OriginUrl = request.OriginUrl,
                ResumeId = request.ResumeId,
            };

            await amazonDynamoDB.PutItemAsync(new PutItemRequest<Origin>
            {
                UserId = request.IdentityUser.UserId,
                Entity = origin,
            }, cancellationToken);

            return new CreateOrUpdateOriginResponse(origin);
        }

        public async Task<GetOriginResponse> GetOriginAsync(GetOriginRequest request, CancellationToken cancellationToken)
        {
            var response = await amazonDynamoDB.GetItemAsync(new GetItemRequest<Origin>
            {
                UserId = request.IdentityUser?.UserId,
                PartitionKey = request.OriginUrl,
                SortKey = null,
            }, cancellationToken);

            return new GetOriginResponse(response.Entity);
        }
    }
}
