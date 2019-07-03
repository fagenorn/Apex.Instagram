using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class QpViewerData
    {
        [DataMember(Name = "viewer")]
        public Viewer Viewer { get; set; }
    }
}