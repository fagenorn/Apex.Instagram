using System.Runtime.Serialization;

using Apex.Instagram.Request.Instagram.Paginate;
using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class FollowerAndFollowingResponse : Response, IPaginate
    {
        [DataMember(Name = "users")]
        public User[] Users { get; set; }

        [DataMember(Name = "next_max_id")]
        public string NextMaxId { get; set; }

        [DataMember(Name = "page_size")]
        public dynamic PageSize { get; set; }

        [DataMember(Name = "big_list")]
        public dynamic BigList { get; set; }
    }
}