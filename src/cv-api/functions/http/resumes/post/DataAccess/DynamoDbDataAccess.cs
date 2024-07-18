using Milochau.Core.Aws.DynamoDB;
using Milochau.Core.Aws.DynamoDB.Model;
using Milochau.CV.Shared.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Milochau.CV.Http.Resumes.Post.DataAccess
{
    public class DynamoDbDataAccess(IAmazonDynamoDB amazonDynamoDB)
    {
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
