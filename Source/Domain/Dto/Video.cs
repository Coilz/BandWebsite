using System.Collections.Generic;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Domain.Dto
{
    public class Video : ICommentable
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Resource Url.
        /// </summary>
        public string ResourceUri { get; set; }

        /// <summary>
        /// Stream Url.
        /// </summary>
        public string StreamUri { get; set; }

        /// <summary>
        /// Artwork Url.
        /// </summary>
        public string ArtworkUri { get; set; }

        #region Implementation of ICommentable

        public IEnumerable<Comment> Comments { get; set; }

        #endregion
    }
}