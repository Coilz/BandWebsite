using System;
using System.ComponentModel.DataAnnotations;

namespace Ewk.BandWebsite.Web.UI.Models.AudioAdapterSettings
{
    public class AudioAdapterSettingsDetailsModel : AudioAdapterSettingsBaseModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Date created")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Date modified")]
        public DateTime ModificationDate { get; set; }
    }
}