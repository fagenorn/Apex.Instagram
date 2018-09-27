using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Product
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "price")]
        public string Price { get; set; }

        [DataMember(Name = "current_price")]
        public string CurrentPrice { get; set; }

        [DataMember(Name = "full_price")]
        public string FullPrice { get; set; }

        [DataMember(Name = "product_id")]
        public ulong? ProductId { get; set; }

        [DataMember(Name = "has_viewer_saved")]
        public bool HasViewerSaved { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "main_image")]
        public ProductImage MainImage { get; set; }

        [DataMember(Name = "thumbnail_image")]
        public ProductImage ThumbnailImage { get; set; }

        [DataMember(Name = "product_images")]
        public ProductImage[] ProductImages { get; set; }

        [DataMember(Name = "external_url")]
        public string ExternalUrl { get; set; }

        [DataMember(Name = "checkout_style")]
        public string CheckoutStyle { get; set; }

        [DataMember(Name = "review_status")]
        public string ReviewStatus { get; set; }
    }
}