using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Experiment
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "group")]
        public string Group { get; set; }

        [DataMember(Name = "experiments")]
        public string Experiments { get; set; }

        [DataMember(Name = "additional_params")]
        public dynamic AdditionalParams { get; set; }

        [DataMember(Name = "params")]
        public Param[] Params { get; set; }

        [DataMember(Name = "logging_id")]
        public string LoggingId { get; set; }

        [DataMember(Name = "expired")]
        public bool Expired { get; set; }
    }
}