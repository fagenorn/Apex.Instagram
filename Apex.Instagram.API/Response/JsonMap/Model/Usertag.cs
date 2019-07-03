using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Usertag
    {
        [DataMember(Name = "in")]
        public In[] In { get; set; }

        [DataMember(Name = "photo_of_you")]
        public bool? PhotoOfYou { get; set; }
    }
}