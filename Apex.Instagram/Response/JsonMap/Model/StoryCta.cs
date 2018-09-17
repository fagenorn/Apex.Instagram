using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class StoryCta
    {
        [DataMember(Name = "links")]
        public AndroidLinks[] Links { get; set; }
    }
}