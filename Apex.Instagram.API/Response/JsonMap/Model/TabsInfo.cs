using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class TabsInfo
    {
        [DataMember(Name = "tabs")]
        public Tab[] Tabs { get; set; }

        [DataMember(Name = "selected")]
        public string Selected { get; set; }
    }
}