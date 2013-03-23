using System.ComponentModel.DataAnnotations;

namespace Ewk.BandWebsite.Web.Common.Models.PhotoAdapterSettings
{
    public abstract class PhotoAdapterSettingsBaseModel
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