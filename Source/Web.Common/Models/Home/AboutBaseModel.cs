using System;
using System.ComponentModel.DataAnnotations;

namespace Ewk.BandWebsite.Web.Common.Models.Home
{
    public abstract class AboutBaseModel
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        [Display(Name = "Date founded")]
        public DateTime DateFounded { get; set; }

        [DataType(DataType.MultilineText)]
        public string Info { get; set; }
    }
}