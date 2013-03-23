using System;
using System.Collections.Generic;
using System.Linq;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.Web.Common.Models;
using Ewk.BandWebsite.Web.Common.Models.Blog;

namespace Ewk.BandWebsite.Web.Common.ModelMappers
{
    public class BlogArticleMapper : IBlogArticleMapper
    {
        private readonly ICatalogsContainer _catalogsContainer;
        private IBlogProcess _blogProcess;

        public BlogArticleMapper(ICatalogsContainer catalogsContainer)
        {
            _catalogsContainer = catalogsContainer;
        }

        #region Implementation of IBlogArticleMapper

        public BlogArticle Map(AddBlogArticleModel model, Guid userId)
        {
            return new BlogArticle
                       {
                           AuthorId = userId,
                           Content = model.Content,
                           Title = model.Title
                       };
        }

        public BlogArticle Map(UpdateBlogArticleModel model, Guid blogArticleId)
        {
            var blogArticle = BlogProcess.GetBlogArticle(blogArticleId);

            blogArticle.Title = model.Title;
            blogArticle.Content = model.Content;

            return blogArticle;
        }

        public UpdateBlogArticleModel MapToUpdate(BlogArticle entity)
        {
            return new UpdateBlogArticleModel
                       {
                           Id = entity.Id,
                           Title = entity.Title,
                           Content = entity.Content,
                       };
        }

        public BlogArticleDetailsModel MapToDetail(BlogArticle blogArticle, User author)
        {
            return new BlogArticleDetailsModel
                       {
                           Id = blogArticle.Id,
                           Title = blogArticle.Title,
                           Content = blogArticle.Content.Replace(Environment.NewLine, "<br />"),

                           CreationDate = blogArticle.CreationDate,
                           PublishDate = blogArticle.PublishDate,
                           ModificationDate = blogArticle.ModificationDate,

                           AuthorName = author.Login.LoginName,
                       };
        }

        public ItemListModel<BlogArticleDetailsModel> Map(IEnumerable<BlogArticle> blogArticles, IEnumerable<User> authors)
        {
            return new ItemListModel<BlogArticleDetailsModel>
                       {
                           Title = "Blog",
                           Items = blogArticles
                               .Select(blogArticle =>
                                       MapToDetail(blogArticle, authors
                                                                    .Single(author =>
                                                                            author.Id ==
                                                                            blogArticle.AuthorId))),
                       };
        }

        #endregion

        private IBlogProcess BlogProcess
        {
            get
            {
                return _blogProcess ?? (_blogProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogProcess>(_catalogsContainer));
            }
        }
    }
}