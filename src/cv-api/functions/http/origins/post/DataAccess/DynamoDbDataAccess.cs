using Milochau.Core.Aws.DynamoDB;
using Milochau.Core.Aws.DynamoDB.Model;
using Milochau.CV.Shared.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Milochau.CV.Http.Origins.Post.DataAccess
{
    public class DynamoDbDataAccess(IAmazonDynamoDB amazonDynamoDB)
    {
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
