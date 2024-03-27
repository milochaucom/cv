using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentPersona : IDynamoDbEntity<ResumeContentPersona>
    {
        public required string Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Nationality { get; set; }
        public string? Job { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public List<ResumeContentPersonaAction>? Actions { get; set; }
        public ResumeContentPersonaContact? Contact { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("na", Name)
                .Append("fn", FirstName)
                .Append("ln", LastName)
                .Append("nl", Nationality)
                .Append("jo", Job)
                .Append("de", Description)
                .Append("lo", Location)
                .Append("ac", Actions)
                .Append("co", Contact)
                .ToDictionary();
        }

        public static ResumeContentPersona ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentPersona
            {
                Name = attributes.ReadString("na"),
                FirstName = attributes.ReadStringOptional("fn"),
                LastName = attributes.ReadStringOptional("ln"),
                Nationality = attributes.ReadStringOptional("nl"),
                Job = attributes.ReadStringOptional("jo"),
                Description = attributes.ReadStringOptional("de"),
                Location = attributes.ReadStringOptional("lo"),
                Actions = attributes.ReadList<ResumeContentPersonaAction>("ac"),
                Contact = attributes.ReadObjectOptional<ResumeContentPersonaContact>("co"),
            };
        }
    }
}
