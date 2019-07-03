using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class ProductImage
    {
        [DataMember(Name = "image_versions2")]
        public ImageVersions2 ImageVersions2 { get; set; }
    }
}