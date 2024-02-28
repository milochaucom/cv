using Milochau.Core.Aws.Core.References;
using Milochau.Core.Aws.DynamoDB;
using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using Milochau.CV.Shared.DynamoDB;
using Milochau.CV.Shared.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Milochau.CV.Http.Resumes.Get.DataAccess
{
    public interface IDynamoDbDataAccess
    {
        Task<Origin?> GetOriginAsync(FunctionRequest request, CancellationToken cancellationToken);
        Task<FunctionResponse?> GetResumeAsync(FunctionRequest request, Origin origin, CancellationToken cancellationToken);
    }

    public class DynamoDbDataAccess : BaseDynamoDbDataAccess, IDynamoDbDataAccess
    {
        public DynamoDbDataAccess(IAmazonDynamoDB amazonDynamoDB) : base(amazonDynamoDB)
        {
        }

        public async Task<Origin?> GetOriginAsync(FunctionRequest request, CancellationToken cancellationToken)
        {
            var response = await amazonDynamoDB.GetItemAsync(new GetItemRequest(request.User?.UserId)
            {
                TableName = $"{EnvironmentVariables.ConventionPrefix}-table-{Origin.TableNameSuffix}",
                Key = new Dictionary<string, AttributeValue>()
                    .Append(Origin.K_OriginUrl, request.OriginUrl)
                    .ToDictionary(x => x.Key, x => x.Value),
            }, cancellationToken);

            if (response.Item == null)
            {
                return null;
            }

            return Origin.ParseFromDynamoDb(response.Item);
        }

        public async Task<FunctionResponse?> GetResumeAsync(FunctionRequest request, Origin origin, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(request.Lang))
            {
                var response = await amazonDynamoDB.GetItemAsync(new GetItemRequest(request.User?.UserId)
                {
                    TableName = $"{EnvironmentVariables.ConventionPrefix}-table-{Resume.TableNameSuffix}",
                    Key = new Dictionary<string, AttributeValue>()
                        .Append(Resume.K_Id, origin.ResumeId)
                        .Append(Resume.K_Lang, request.Lang)
                        .ToDictionary(x => x.Key, x => x.Value),
                }, cancellationToken);

                if (response.Item == null)
                {
                    return null;
                }

                var resume = Resume.ParseFromDynamoDb(response.Item);
                return new FunctionResponse
                {
                    Content = resume.Content,
                };
            }
            else
            {
                var keyConditionExpressionBuilder = new DynamoDbKeyConditionExpressionBuilder();
                keyConditionExpressionBuilder.Equal.Add(Resume.K_Id);

                var dynamoDbRequest = new QueryRequest(request.User?.UserId)
                {
                    TableName = $"{EnvironmentVariables.ConventionPrefix}-table-{Resume.TableNameSuffix}",
                    KeyConditionExpression = keyConditionExpressionBuilder.Build(),
                    ExpressionAttributeNames = keyConditionExpressionBuilder.GetExpressionAttributeNames(),
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                        .AppendValue(Resume.K_Id, origin.ResumeId)
                        .ToDictionary(x => x.Key, x => x.Value),
                    Limit = Resume.MaxFetchItems,
                    ScanIndexForward = false,
                };

                var dynamoDbResponse = await amazonDynamoDB.QueryAsync(dynamoDbRequest, cancellationToken);
                if (dynamoDbResponse.Items == null)
                {
                    return null;
                }

                // Take the first resume we find, as we don't know which language to choose
                var resume = dynamoDbResponse.Items.Select(Resume.ParseFromDynamoDb).First();
                return new FunctionResponse
                {
                    Content = resume.Content,
                };
            }
        }
    }
}
