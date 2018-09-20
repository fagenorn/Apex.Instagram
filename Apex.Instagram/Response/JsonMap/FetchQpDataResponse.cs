using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class FetchQpDataResponse : Response
    {
        [DataMember(Name = "request_status")]
        public string RequestStatus { get; set; }

        [DataMember(Name = "extra_info")]
        public QpExtraInfo[] ExtraInfo { get; set; }

        [DataMember(Name = "qp_data")]
        public QpData[] QpData { get; set; }

        [DataMember(Name = "error_msg")]
        public dynamic ErrorMsg { get; set; }
    }
}