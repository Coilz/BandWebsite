using System.ComponentModel.DataAnnotations;

namespace Ewk.BandWebsite.Web.UI.Models.VideoAdapterSettings
{
    public class VideoDetailsModel
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
        [Display(Name = "Resource uri")]
        public string ResourceUri { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Stream uri")]
        public string StreamUri { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Artwork uri")]
        public string ArtworkUri { get; set; }
    }
}