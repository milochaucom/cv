using Milochau.Core.Aws.ApiGateway;
using Milochau.Core.Aws.Core.JsonConverters;
using Milochau.Core.Aws.Core.Lambda.Core;
using Milochau.Core.Aws.Core.Lambda.Events;
using Milochau.Core.Aws.Core.Lambda.RuntimeSupport.Bootstrap;
using Milochau.Core.Aws.Core.Runtime.Credentials;
using Milochau.Core.Aws.DynamoDB;
using Milochau.CV.Http.Resumes.Get.DataAccess;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Milochau.CV.Http.Resumes.Get
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
        private readonly DynamoDbDataAccess dynamoDbDataAccess = new(amazonDynamoDB);

        public async Task<APIGatewayHttpApiV2ProxyResponse> DoAsync(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context, CancellationToken cancellationToken)
        {
            if (!request.TryParseAndValidate<FunctionRequest>(new ValidationOptions { AuthenticationRequired = false }, out var proxyResponse, out var requestData))
            {
                return proxyResponse;
            }

            var origin = await dynamoDbDataAccess.GetOriginAsync(requestData, cancellationToken);
            if (origin == null)
            {
                return HttpResponse.NotFound();
            }

            FunctionResponse? response = null;
            if (!string.IsNullOrWhiteSpace(requestData.Lang))
            {
                response = await dynamoDbDataAccess.GetResumeAsync(requestData.User, origin.ResumeId, requestData.Lang, cancellationToken);
            }
            if (response == null)
            {
                response = await dynamoDbDataAccess.GetResumeFallbackAsync(requestData.User, origin.ResumeId, cancellationToken);
            }
            if (response == null)
            {
                return HttpResponse.NotFound();
            }

            return HttpResponse.Ok(response, ApplicationJsonSerializerContext.Default.FunctionResponse);
        }
    }

    [JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, Converters = [typeof(GuidConverter)])]
    [JsonSerializable(typeof(APIGatewayHttpApiV2ProxyRequest))]
    [JsonSerializable(typeof(APIGatewayHttpApiV2ProxyResponse))]
    [JsonSerializable(typeof(FunctionResponse))]
    public partial class ApplicationJsonSerializerContext : JsonSerializerContext
    {
    }
}
