using System.ComponentModel.DataAnnotations;

namespace Ewk.BandWebsite.Web.Common.Models.PhotoAdapterSettings
{
    public class PhotoDetailsModel
    {
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Web uri")]
        public string WebUrl { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Large uri")]
        public string LargeUrl { get; set; }
        public int? LargeWidth { get; set; }
        public int? LargeHeight { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Medium uri")]
        public string MediumUrl { get; set; }
        public int? MediumWidth { get; set; }
        public int? MediumHeight { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Small uri")]
        public string SmallUrl { get; set; }
        public int? SmallWidth { get; set; }
        public int? SmallHeight { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Original uri")]
        public string OriginalUrl { get; set; }
        public int OriginalWidth { get; set; }
        public int OriginalHeigth { get; set; }
    }
}