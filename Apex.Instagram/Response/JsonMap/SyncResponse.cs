using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class SyncResponse : Response
    {
        [DataMember(Name = "experiments")]
        public Experiment[] Experiments { get; set; }
    }
}