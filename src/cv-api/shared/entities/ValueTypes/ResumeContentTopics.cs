using Milochau.Core.Aws.DynamoDB.Abstractions;
using System.Collections.Generic;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContentTopics
    {
        [DynamoDbAttribute("it")]
        public List<ResumeContentTopicsItem>? Items { get; set; }
    }
}
