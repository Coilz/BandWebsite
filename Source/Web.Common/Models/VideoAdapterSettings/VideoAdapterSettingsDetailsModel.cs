using System;
using System.ComponentModel.DataAnnotations;

namespace Ewk.BandWebsite.Web.Common.Models.VideoAdapterSettings
{
    public class VideoAdapterSettingsDetailsModel : VideoAdapterSettingsBaseModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Date created")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Date modified")]
        public DateTime ModificationDate { get; set; }
    }
}