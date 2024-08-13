using Milochau.Core.Aws.ApiGateway;
using Milochau.Core.Aws.Core.JsonConverters;
using Milochau.Core.Aws.Core.Lambda.Core;
using Milochau.Core.Aws.Core.Lambda.Events;
using Milochau.Core.Aws.Core.Lambda.RuntimeSupport.Bootstrap;
using Milochau.Core.Aws.Core.Runtime.Credentials;
using Milochau.Core.Aws.DynamoDB;
using Milochau.CV.Shared.Data;
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
        private readonly OriginRepository originRepository = new(amazonDynamoDB);
        private readonly ResumeRepository resumeRepository = new(amazonDynamoDB);

        public async Task<APIGatewayHttpApiV2ProxyResponse> DoAsync(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context, CancellationToken cancellationToken)
        {
            if (!request.TryParseAndValidate<FunctionRequest>(new ValidationOptions { AuthenticationRequired = false }, out var proxyResponse, out var requestData))
            {
                return proxyResponse;
            }

            var origin = await originRepository.ReadOriginAsync(new(requestData.OriginUrl, requestData.User), cancellationToken);
            if (origin.Origin == null)
            {
                return HttpResponse.NotFound();
            }

            var resumeResponse = await resumeRepository.ReadResumeAsync(new(origin.Origin.ResumeId, requestData.Lang, requestData.User), cancellationToken);
            if (resumeResponse.Resume == null)
            {
                return HttpResponse.NotFound();
            }

            var response = new FunctionResponse
            {
                Content = resumeResponse.Resume.Content,
            };
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
