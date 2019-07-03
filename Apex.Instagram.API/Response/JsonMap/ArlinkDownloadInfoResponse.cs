using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap
{
    public class ArlinkDownloadInfoResponse : Response
    {
        [DataMember(Name = "checksum")]
        public string Checksum { get; set; }

        [DataMember(Name = "download_url")]
        public string DownloadUrl { get; set; }

        [DataMember(Name = "file_size")]
        public ulong FileSize { get; set; }

        [DataMember(Name = "version")]
        public string Version { get; set; }
    }
}