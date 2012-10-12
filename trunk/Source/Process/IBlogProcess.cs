using System;
using System.Collections.Generic;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Process
{
    /// <summary>
    /// Provides methods to process blog articles.
    /// </summary>
    public interface IBlogProcess
    {
        /// <summary>
        /// Gets all <see cref="BlogArticle"/> instances.
        /// </summary>
        /// <returns>A list of <see cref="BlogArticle"/> instances.</returns>
        IEnumerable<BlogArticle> GetBlogArticles();

        /// <summary>
        /// Gets some of the <see cref="BlogArticle"/> instances.
        /// </summary>
        /// <param name="page">The page number (zero based).</param>
        /// <param name="pageSize">The size of a page.</param>
        /// <returns>A list of <see cref="BlogArticle"/> instances.</returns>
        IEnumerable<BlogArticle> GetBlogArticles(int page, int pageSize);

        /// <summary>
        /// Gets a specific <see cref="BlogArticle"/>.
        /// </summary>
        /// <param name="id">A value that uniquely identifies a <see cref="BlogArticle"/>.</param>
        /// <returns>The requested <see cref="BlogArticle"/></returns>
        BlogArticle GetBlogArticle(Guid id);

        /// <summary>
        /// Adds the specified <see cref="BlogArticle"/> to the store.
        /// </summary>
        /// <param name="article">The <see cref="BlogArticle"/> to store.</param>
        /// <returns>The persisted <see cref="BlogArticle"/></returns>
        BlogArticle AddBlogArticle(BlogArticle article);

        /// <summary>
        /// Updates the specified <see cref="BlogArticle"/> in the store.
        /// </summary>
        /// <param name="article">The <see cref="BlogArticle"/> to update in the store.</param>
        /// <returns>The persisted <see cref="BlogArticle"/></returns>
        BlogArticle UpdateBlogArticle(BlogArticle article);

        /// <summary>
        /// Removes the specified <see cref="BlogArticle"/> from the store.
        /// </summary>
        /// <param name="article">The <see cref="BlogArticle"/> to update in the store.</param>
        /// <returns>The persisted <see cref="BlogArticle"/></returns>
        void RemoveBlogArticle(BlogArticle article);
    }
}