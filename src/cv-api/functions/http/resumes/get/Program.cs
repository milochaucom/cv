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
        private static readonly Function function = new Function(new EnvironmentVariablesAWSCredentials());

        private static Task Main()
        {
            return LambdaBootstrap.RunAsync(function.DoAsync, ApplicationJsonSerializerContext.Default.APIGatewayHttpApiV2ProxyRequest, ApplicationJsonSerializerContext.Default.APIGatewayHttpApiV2ProxyResponse);
        }
    }

    public class Function
    {
        private readonly IDynamoDbDataAccess dynamoDbDataAccess;

        public Function(IAWSCredentials credentials)
            : this(new DynamoDbDataAccess(new AmazonDynamoDBClient(credentials)))
        { }

        public Function(IDynamoDbDataAccess dynamoDbDataAccess)
        {
            this.dynamoDbDataAccess = dynamoDbDataAccess;
        }

        public async Task<APIGatewayHttpApiV2ProxyResponse> DoAsync(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context, CancellationToken cancellationToken)
        {
            context.Logger.LogLine(Microsoft.Extensions.Logging.LogLevel.Information, $"DomainName: {request.RequestContext.DomainName}");
            context.Logger.LogLine(Microsoft.Extensions.Logging.LogLevel.Information, $"SourceIp: {request.RequestContext.Http?.SourceIp}");

            if (!request.TryParseAndValidate<FunctionRequest>(new ValidationOptions { AuthenticationRequired = false }, out var proxyResponse, out var requestData))
            {
                return proxyResponse;
            }

            var origin = await dynamoDbDataAccess.GetOriginAsync(requestData, cancellationToken);
            if (origin == null)
            {
                return HttpResponse.NotFound();
            }

            var response = await dynamoDbDataAccess.GetResumeAsync(requestData, origin, cancellationToken);
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
