using System;
using System.Collections.Generic;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Process
{
    /// <summary>
    /// Provides methods to process blog articles.
    /// </summary>
    public class BlogProcess : ProcessBase, IBlogProcess
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="catalogsContainer">A container with all catalogs to access the store.</param>
        public BlogProcess(ICatalogsContainer catalogsContainer)
            :base(catalogsContainer)
        {
        }

        #region Implementation of IBlogProcess

        public IEnumerable<BlogArticle> GetBlogArticles()
        {
            return BandRepository.GetAllBlogArticles();
        }

        public IEnumerable<BlogArticle> GetBlogArticles(int page, int pageSize)
        {
            return BandRepository.GetBlogArticles(page, pageSize);
        }

        public BlogArticle GetBlogArticle(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException("id");

            return BandRepository.GetBlogArticle(id);
        }

        public BlogArticle AddBlogArticle(BlogArticle article)
        {
            if (article == null) throw new ArgumentNullException("article");

            return BandRepository.AddBlogArticle(article);
        }

        public BlogArticle UpdateBlogArticle(BlogArticle article)
        {
            if (article == null) throw new ArgumentNullException("article");

            return BandRepository.UpdateBlogArticle(article);
        }

        public void RemoveBlogArticle(BlogArticle article)
        {
            if (article == null) throw new ArgumentNullException("article");

            BandRepository.RemoveBlogArticle(article);
        }

        #endregion
    }
}