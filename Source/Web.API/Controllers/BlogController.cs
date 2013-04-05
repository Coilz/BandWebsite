using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.Web.Common.ModelMappers;
using Ewk.BandWebsite.Web.Common.Models.Blog;

namespace Ewk.BandWebsite.Web.API.Controllers
{
    public class BlogController : ApiController
    {
        [BandIdFilter]
        public async Task<IQueryable<BlogArticleDetailsModel>> GetAsync(Guid bandId)
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                    {
                        var blogProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogProcess>(container);
                        var blogArticles = blogProcess.GetBlogArticles()
                            .ToList();

                        var authorIds = blogArticles
                            .Select(article => article.AuthorId)
                            .Distinct();

                        var userProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IUserProcess>(container);
                        var users = authorIds.Select(userProcess.GetUser);

                        var blogArticleMapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogArticleMapper>(container);
                        return blogArticleMapper.Map(blogArticles, users).Items.AsQueryable();
                    });
        }

        [BandIdFilter]
        public async Task<IEnumerable<BlogArticleDetailsModel>> GetAsync(Guid bandId, int page, int pageSize)
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                    {
                        var blogProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogProcess>(container);
                        var blogArticles = blogProcess.GetBlogArticles(page, pageSize)
                            .ToList();

                        var authorIds = blogArticles
                            .Select(article => article.AuthorId)
                            .Distinct();

                        var userProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IUserProcess>(container);
                        var users = authorIds.Select(userProcess.GetUser);

                        var blogArticleMapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogArticleMapper>(container);
                        return blogArticleMapper.Map(blogArticles, users).Items;
                    });
        }

        [BandIdFilter]
        public async Task<BlogArticleDetailsModel> GetAsync(Guid bandId, Guid id)
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                    {
                        var blogProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogProcess>(container);
                        var blogArticle = blogProcess.GetBlogArticle(id);

                        var userProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IUserProcess>(container);
                        var user = userProcess.GetUser(blogArticle.AuthorId);

                        var blogArticleMapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogArticleMapper>(container);
                        return blogArticleMapper.MapToDetail(blogArticle, user);
                    });
        }

        /// <remarks>
        /// Check http://stackoverflow.com/questions/11986974/how-to-do-role-based-authorization-for-asp-net-mvc-4-web-api for info on authorization using tokens.
        /// Als check: http://www.asp.net/web-api/overview/working-with-http/http-message-handlers
        /// </remarks>
        [Authorize, BandIdFilter]
        public void Post(Guid bandId, [FromBody]AddBlogArticleModel model)
        {
            CatalogsConsumerHelper.ExecuteWithCatalogScope(
                container =>
                    {
                        var membershipUser = Membership.GetUser();
                        var userId = (Guid) membershipUser.ProviderUserKey;

                        var blogArticleMapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogArticleMapper>(container);
                        var blogArticle = blogArticleMapper.Map(model, userId);

                        var blogProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogProcess>(container);
                        blogProcess.AddBlogArticle(blogArticle);
                    });
        }

        [Authorize, BandIdFilter]
        public async Task Put(Guid bandId, Guid id, [FromBody] UpdateBlogArticleModel model)
        {
            await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                    {
                        var blogArticleMapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogArticleMapper>(container);
                        var blogArticle = blogArticleMapper.Map(model, id);

                        var blogProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogProcess>(container);
                        blogProcess.UpdateBlogArticle(blogArticle);
                    });
        }

        [Authorize, BandIdFilter]
        public async Task Delete(Guid bandId, Guid id)
        {
            await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                    {
                        var blogProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogProcess>(container);
                        var blogArticle = blogProcess.GetBlogArticle(id);

                        blogProcess.RemoveBlogArticle(blogArticle);
                    });
        }
    }
}