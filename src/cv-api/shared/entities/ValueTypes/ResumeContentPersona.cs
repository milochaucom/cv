using Milochau.Core.Aws.DynamoDB.Abstractions;
using System.Collections.Generic;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContentPersona
    {
        [DynamoDbAttribute("na")]
        public required string Name { get; set; }
        [DynamoDbAttribute("fn")]
        public string? FirstName { get; set; }
        [DynamoDbAttribute("ln")]
        public string? LastName { get; set; }
        [DynamoDbAttribute("nl")]
        public string? Nationality { get; set; }
        [DynamoDbAttribute("jo")]
        public string? Job { get; set; }
        [DynamoDbAttribute("de")]
        public string? Description { get; set; }
        [DynamoDbAttribute("lo")]
        public string? Location { get; set; }
        [DynamoDbAttribute("ac")]
        public List<ResumeContentPersonaAction>? Actions { get; set; }
        [DynamoDbAttribute("co")]
        public ResumeContentPersonaContact? Contact { get; set; }
    }
}
