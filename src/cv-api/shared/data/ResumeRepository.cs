using Milochau.Core.Aws.DynamoDB;
using Milochau.Core.Aws.DynamoDB.Model;
using Milochau.Core.Aws.DynamoDB.Model.Expressions;
using Milochau.CV.Shared.Data.Models;
using Milochau.CV.Shared.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Milochau.CV.Shared.Data
{
    public class ResumeRepository(IAmazonDynamoDB amazonDynamoDB)
    {
        public async Task<CreateOrUpdateResumeResponse> CreateOrUpdateResumeAsync(CreateOrUpdateResumeRequest request, CancellationToken cancellationToken)
        {
            var resume = new Resume
            {
                Id = request.ResumeId,
                Lang = request.Lang,
                Creation = DateTimeOffset.Now,
                UserId = request.IdentityUser.UserId,
                Content = request.ResumeContent,
            };

            await amazonDynamoDB.PutItemAsync(new PutItemRequest<Resume>
            {
                UserId = request.IdentityUser.UserId,
                Entity = resume,
            }, cancellationToken);

            return new CreateOrUpdateResumeResponse(resume);
        }

        public async Task<ReadResumeResponse> ReadResumeAsync(ReadResumeRequest request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(request.Lang))
            {
                var dynamoDbResponse = await amazonDynamoDB.GetItemAsync(new GetItemRequest<Resume>
                {
                    UserId = request.IdentityUser?.UserId,
                    PartitionKey = request.ResumeId,
                    SortKey = request.Lang,
                }, cancellationToken);

                if (dynamoDbResponse.Entity != null)
                {
                    return new ReadResumeResponse(dynamoDbResponse.Entity);
                }
            }

            // Fallback : use the first resume we find
            var dynamoDbRequest = new QueryRequest<Resume>
            {
                UserId = request.IdentityUser?.UserId,
                PartitionKeyCondition = new EqualValueExpression(Resume.PartitionKey, request.ResumeId),
                Limit = Resume.MaxFetchItems,
                ScanIndexForward = false,
            };

            var dynamoDbQueryResponse = await amazonDynamoDB.QueryAsync(dynamoDbRequest, cancellationToken);

            // Take the first resume we find, as we don't know which language to choose
            return new ReadResumeResponse(dynamoDbQueryResponse.Entities?.FirstOrDefault());
        }
    }
}
