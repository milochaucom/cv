using Milochau.Core.Aws.Abstractions;
using Milochau.Core.Aws.DynamoDB;
using Milochau.Core.Aws.DynamoDB.Model;
using Milochau.Core.Aws.DynamoDB.Model.Expressions;
using Milochau.CV.Shared.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Milochau.CV.Http.Resumes.Get.DataAccess
{
    public class DynamoDbDataAccess(IAmazonDynamoDB amazonDynamoDB)
    {
        public async Task<Origin?> GetOriginAsync(FunctionRequest request, CancellationToken cancellationToken)
        {
            var response = await amazonDynamoDB.GetItemAsync(new GetItemRequest<Origin>
            {
                UserId = request.User?.UserId,
                PartitionKey = request.OriginUrl,
                SortKey = null,
            }, cancellationToken);

            return response.Entity;
        }

        public async Task<FunctionResponse?> GetResumeAsync(IdentityUser? user, Guid resumeId, string lang, CancellationToken cancellationToken)
        {
            var response = await amazonDynamoDB.GetItemAsync(new GetItemRequest<Resume>
            {
                UserId = user?.UserId,
                PartitionKey = resumeId,
                SortKey = lang,
            }, cancellationToken);

            if (response.Entity == null)
            {
                return null;
            }

            return new FunctionResponse
            {
                Content = response.Entity.Content,
            };
        }

        public async Task<FunctionResponse?> GetResumeFallbackAsync(IdentityUser? user, Guid resumeId, CancellationToken cancellationToken)
        {
            var dynamoDbRequest = new QueryRequest<Resume>
            {
                UserId = user?.UserId,
                PartitionKeyCondition = new EqualValueExpression(Resume.PartitionKey, resumeId),
                Limit = Resume.MaxFetchItems,
                ScanIndexForward = false,
            };

            var dynamoDbResponse = await amazonDynamoDB.QueryAsync(dynamoDbRequest, cancellationToken);
            if (dynamoDbResponse.Entities == null)
            {
                return null;
            }

            // Take the first resume we find, as we don't know which language to choose
            var resume = dynamoDbResponse.Entities.First();
            return new FunctionResponse
            {
                Content = resume.Content,
            };
        }
    }
}
