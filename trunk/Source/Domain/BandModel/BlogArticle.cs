using System;
using Ewk.BandWebsite.Common;

namespace Ewk.BandWebsite.Domain.BandModel
{
    /// <summary>
    /// A representation of an article that was posted in a blog.
    /// </summary>
    public class BlogArticle : BandEntity
    {
        /// <summary>
        /// The title of the blog article
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The content of the blog article
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The author of the article which is a <see cref="User"/>.
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// The publish date of the article.
        /// </summary>
        public DateTime PublishDate { get; set; }
    }
}