using Milochau.Core.Aws.DynamoDB.Helpers;
using Milochau.Core.Aws.DynamoDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace Milochau.CV.Shared.Entities.ValueTypes
{
    public class ResumeContentExperiencesItem
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string Company { get; set; }
        public string? Client { get; set; }
        public string? Place { get; set; }
        public string? PlaceUri { get; set; }
        public required string StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Lang { get; set; }
        public List<ResumeContentExperiencesMissionItem>? Missions { get; set; }
        public List<ResumeTag>? Tags { get; set; }

        public Dictionary<string, AttributeValue> FormatForDynamoDb()
        {
            return new Dictionary<string, AttributeValue>()
                .Append("ti", Title)
                .Append("de", Description)
                .Append("co", Company)
                .Append("cl", Client)
                .Append("pl", Place)
                .Append("pu", PlaceUri)
                .Append("sd", StartDate)
                .Append("ed", EndDate)
                .Append("la", Lang)
                .Append("mi", Missions?.Select(x => x.FormatForDynamoDb()))
                .Append("ta", Tags?.Select(x => x.FormatForDynamoDb()))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static ResumeContentExperiencesItem ParseFromDynamoDb(Dictionary<string, AttributeValue> attributes)
        {
            return new ResumeContentExperiencesItem
            {
                Title = attributes.ReadString("ti"),
                Description = attributes.ReadStringOptional("de"),
                Company = attributes.ReadString("co"),
                Client = attributes.ReadStringOptional("cl"),
                Place = attributes.ReadStringOptional("pl"),
                PlaceUri = attributes.ReadStringOptional("pu"),
                StartDate = attributes.ReadString("sd"),
                EndDate = attributes.ReadStringOptional("ed"),
                Lang = attributes.ReadStringOptional("la"),
                Missions = attributes.ReadListOptional("mi")?.Select(x => ResumeContentExperiencesMissionItem.ParseFromDynamoDb(x)).ToList(),
                Tags = attributes.ReadListOptional("ta")?.Select(x => ResumeTag.ParseFromDynamoDb(x)).ToList(),
            };
        }
    }
}
