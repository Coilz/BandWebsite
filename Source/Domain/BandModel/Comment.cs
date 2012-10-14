using System;

namespace Ewk.BandWebsite.Domain.BandModel
{
    public class Comment : BandEntity
    {
        /// <summary>
        /// The content of the comment
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The author of the comment which is a registered visitor.
        /// </summary>
        public Guid AuthorId { get; set; }
    }
}