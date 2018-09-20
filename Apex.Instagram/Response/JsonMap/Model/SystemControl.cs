using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class SystemControl
    {
        [DataMember(Name = "upload_max_bytes")]
        public int? UploadMaxBytes { get; set; }

        [DataMember(Name = "upload_time_period_sec")]
        public int? UploadTimePeriodSec { get; set; }

        [DataMember(Name = "upload_bytes_per_update")]
        public int? UploadBytesPerUpdate { get; set; }
    }
}