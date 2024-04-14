using Milochau.Core.Aws.DynamoDB.Abstractions;
using System.Collections.Generic;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    [DynamoDbNested]
    public partial class ResumeContentExperiencesItem
    {
        [DynamoDbAttribute("ti")]
        public required string Title { get; set; }
        [DynamoDbAttribute("de")]
        public string? Description { get; set; }
        [DynamoDbAttribute("co")]
        public required string Company { get; set; }
        [DynamoDbAttribute("cl")]
        public string? Client { get; set; }
        [DynamoDbAttribute("pl")]
        public string? Place { get; set; }
        [DynamoDbAttribute("pu")]
        public string? PlaceUri { get; set; }
        [DynamoDbAttribute("sd")]
        public required string StartDate { get; set; }
        [DynamoDbAttribute("ed")]
        public string? EndDate { get; set; }
        [DynamoDbAttribute("la")]
        public string? Lang { get; set; }
        [DynamoDbAttribute("mi")]
        public List<ResumeContentExperiencesMissionItem>? Missions { get; set; }
        [DynamoDbAttribute("ta")]
        public List<ResumeTag>? Tags { get; set; }
    }
}
