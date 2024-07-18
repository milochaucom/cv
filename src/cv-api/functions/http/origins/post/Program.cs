using Milochau.Core.Aws.ApiGateway;
using Milochau.Core.Aws.Core.Lambda.Core;
using Milochau.Core.Aws.Core.Lambda.Events;
using Milochau.Core.Aws.Core.Lambda.RuntimeSupport.Bootstrap;
using Milochau.Core.Aws.Core.Runtime.Credentials;
using Milochau.Core.Aws.DynamoDB;
using Milochau.CV.Http.Origins.Post.DataAccess;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Milochau.Core.Aws.Core.JsonConverters;
using Milochau.CV.Shared.Data;

namespace Milochau.CV.Http.Origins.Post
{
    public class FunctionHandler
    {
        private static readonly EnvironmentVariablesAWSCredentials credentials = new();
        private static readonly Function function = new(new AmazonDynamoDBClient(credentials));

        private static Task Main()
        {
            return LambdaBootstrap.RunAsync(function.DoAsync, ApplicationJsonSerializerContext.Default.APIGatewayHttpApiV2ProxyRequest, ApplicationJsonSerializerContext.Default.APIGatewayHttpApiV2ProxyResponse);
        }
    }

    public class Function(IAmazonDynamoDB amazonDynamoDB)
    {
        private readonly AccessRepository accessRepository = new(amazonDynamoDB);
        private readonly DynamoDbDataAccess dynamoDbDataAccess = new(amazonDynamoDB);

        public async Task<APIGatewayHttpApiV2ProxyResponse> DoAsync(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context, CancellationToken cancellationToken)
        {
            if (!request.TryParseAndValidate<FunctionRequest>(new ValidationOptions(), out var proxyResponse, out var requestData))
            {
                return proxyResponse;
            }

            var accessResult = await accessRepository.GetAccessAsync(new(requestData.Body.ResumeId, requestData.User), cancellationToken);
            if (accessResult.Access == null)
            {
                return HttpResponse.NotFound();
            }

            await dynamoDbDataAccess.CreateOrUpdateOriginAsync(requestData, cancellationToken);

            return HttpResponse.NoContent();
        }
    }

    [JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, Converters = [typeof(GuidConverter)])]
    [JsonSerializable(typeof(APIGatewayHttpApiV2ProxyRequest))]
    [JsonSerializable(typeof(APIGatewayHttpApiV2ProxyResponse))]
    [JsonSerializable(typeof(FunctionRequestBody))]
    public partial class ApplicationJsonSerializerContext : JsonSerializerContext
    {
    }
}
