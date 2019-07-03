using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class ExploreItemInfo
    {
        [DataMember(Name = "num_columns")]
        public int? NumColumns { get; set; }

        [DataMember(Name = "total_num_columns")]
        public int? TotalNumColumns { get; set; }

        [DataMember(Name = "aspect_ratio")]
        public double? AspectRatio { get; set; }

        [DataMember(Name = "autoplay")]
        public bool? Autoplay { get; set; }

        [DataMember(Name = "destination_view")]
        public string DestinationView { get; set; }
    }
}