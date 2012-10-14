using System;
using System.Collections.Generic;

namespace Ewk.BandWebsite.Domain.BandModel
{
    public class Performance : BandEntity, ICommentable
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public string VenueName { get; set; }
        public Uri VenueUri { get; set; }
        public string City { get; set; }
        public string Info { get; set; }
        public decimal Price { get; set; }

        #region Implementation of ICommentable

        public IEnumerable<Comment> Comments { get; set; }

        #endregion
    }
}