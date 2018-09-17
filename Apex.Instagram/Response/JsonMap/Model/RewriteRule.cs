using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class RewriteRule
    {
        [DataMember(Name = "matcher")]
        public string Matcher { get; set; }

        [DataMember(Name = "replacer")]
        public string Replacer { get; set; }
    }
}