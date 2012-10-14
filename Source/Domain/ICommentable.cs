using System.Collections.Generic;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Domain
{
    public interface ICommentable
    {
        /// <summary>
        /// The comments on this entry.
        /// </summary>
        IEnumerable<Comment> Comments { get; set; }
    }
}