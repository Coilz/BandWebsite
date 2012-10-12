using System;

namespace Ewk.BandWebsite.Domain.BandModel
{
    public class Performance : BandEntity
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public string VenueName { get; set; }
        public Uri VenueUri { get; set; }
        public string City { get; set; }
        public string Info { get; set; }
        public decimal Price { get; set; }
    }
}