using Milochau.Core.Aws.DynamoDB;
using Milochau.Core.Aws.DynamoDB.Model;
using Milochau.CV.Shared.Data.Models;
using Milochau.CV.Shared.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Milochau.CV.Shared.Data
{
    public class AccessRepository(IAmazonDynamoDB amazonDynamoDB)
    {

        public async Task<ReadAccessResponse> ReadAccessAsync(ReadAccessRequest request, CancellationToken cancellationToken)
        {
            var response = await amazonDynamoDB.GetItemAsync(new GetItemRequest<Access>
            {
                UserId = request.IdentityUser.UserId,
                PartitionKey = request.IdentityUser.UserId,
                SortKey = request.ResumeId,
            }, cancellationToken);

            return new ReadAccessResponse(response.Entity);
        }
    }
}
