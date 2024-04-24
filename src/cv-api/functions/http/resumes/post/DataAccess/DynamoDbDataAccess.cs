using Milochau.Core.Aws.Abstractions;
using Milochau.Core.Aws.DynamoDB;
using Milochau.Core.Aws.DynamoDB.Model;
using Milochau.CV.Shared.DynamoDB;
using Milochau.CV.Shared.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Milochau.CV.Http.Resumes.Post.DataAccess
{
    public interface IDynamoDbDataAccess
    {
        Task<AccessResult> CheckAccessAsync(IdentityUser user, Guid resumeId, CancellationToken cancellationToken);
        Task CreateOrUpdateResumeAsync(FunctionRequest request, CancellationToken cancellationToken);
    }

    public class DynamoDbDataAccess : BaseDynamoDbDataAccess, IDynamoDbDataAccess
    {
        public DynamoDbDataAccess(IAmazonDynamoDB amazonDynamoDB) : base(amazonDynamoDB)
        {
        }

        public async Task CreateOrUpdateResumeAsync(FunctionRequest request, CancellationToken cancellationToken)
        {
            var resume = new Resume
            {
                Id = request.ResumeId,
                Lang = request.Lang,
                Creation = DateTimeOffset.Now,
                UserId = request.User.UserId,
                Content = request.Body.Content,
            };

            await amazonDynamoDB.PutItemAsync(new PutItemRequest<Resume>
            {
                UserId = request.User.UserId,
                Entity = resume,
            }, cancellationToken);
        }
    }
}
