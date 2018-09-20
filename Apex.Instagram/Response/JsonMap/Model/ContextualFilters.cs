using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class ContextualFilters
    {
        [DataMember(Name = "clause_type")]
        public string ClauseType { get; set; }

        [DataMember(Name = "filters")]
        public dynamic Filters { get; set; }

        [DataMember(Name = "clauses")]
        public dynamic Clauses { get; set; }
    }
}