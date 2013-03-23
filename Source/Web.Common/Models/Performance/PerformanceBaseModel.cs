using System;
using System.ComponentModel.DataAnnotations;

namespace Ewk.BandWebsite.Web.Common.Models.Performance
{
    public abstract class PerformanceBaseModel
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy (ddd)}")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "From")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime StartTime { get; set; }

        [Display(Name = "To")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime EndTime { get; set; }

        [Required]
        [Display(Name = "Name of the venue")]
        public string VenueName { get; set; }

        [Display(Name = "Url of the venue")]
        public string VenueUrl { get; set; }

        [Required]
        public string City { get; set; }

        [Display(Name = "Additional info")]
        [DataType(DataType.MultilineText)]
        public string Info { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }
    }
}