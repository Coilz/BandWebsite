using System;
using System.ComponentModel.DataAnnotations;

namespace Ewk.BandWebsite.Web.Common.Models.PhotoAdapterSettings
{
    public class PhotoAdapterSettingsDetailsModel : PhotoAdapterSettingsBaseModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Date created")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Date modified")]
        public DateTime ModificationDate { get; set; }
    }
}