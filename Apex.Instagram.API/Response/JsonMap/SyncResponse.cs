using System.Runtime.Serialization;

using Apex.Instagram.API.Response.JsonMap.Model;

namespace Apex.Instagram.API.Response.JsonMap
{
    public class SyncResponse : Response
    {
        [DataMember(Name = "experiments")]
        public Experiment[] Experiments { get; set; }
    }
}