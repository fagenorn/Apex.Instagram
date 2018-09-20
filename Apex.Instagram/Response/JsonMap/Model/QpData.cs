using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class QpData
    {
        [DataMember(Name = "surface")]
        public string Surface { get; set; }

        [DataMember(Name = "data")]
        public QpViewerData Data { get; set; }
    }
}