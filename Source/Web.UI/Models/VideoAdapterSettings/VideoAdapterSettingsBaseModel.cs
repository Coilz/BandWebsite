using System.ComponentModel.DataAnnotations;

namespace Ewk.BandWebsite.Web.UI.Models.VideoAdapterSettings
{
    public abstract class VideoAdapterSettingsBaseModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "User id")]
        public string UserId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Set name")]
        public string SetName { get; set; }
    }
}