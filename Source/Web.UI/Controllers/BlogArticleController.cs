using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.Resources;
using Ewk.BandWebsite.Web.UI.ModelMappers;
using Ewk.BandWebsite.Web.UI.Models.Blog;

namespace Ewk.BandWebsite.Web.UI.Controllers
{
    public class BlogArticleController : ControllerBase
    {
        //
        // GET: /BlogArticle/

        public async Task<ActionResult> Index()
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
                        var model = blogArticleMapper.Map(blogArticles, users);

                        return View(model);
                    });
        }

        //
        // GET: /BlogArticle/Details/5

        public async Task<ActionResult> Details(Guid id)
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                    {
                        var blogProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogProcess>(container);
                        var blogArticle = blogProcess.GetBlogArticle(id);

                        var userProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IUserProcess>(container);
                        var user = userProcess.GetUser(blogArticle.AuthorId);

                        var blogArticleMapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogArticleMapper>(container);
                        var model = blogArticleMapper.MapToDetail(blogArticle, user);

                        return View(model);
                    });
        }

        //
        // GET: /BlogArticle/Create

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BlogArticle/Create

        [HttpPost, Authorize]
        public ActionResult Create(AddBlogArticleModel model)
        {
            try
            {
                return CatalogsConsumerHelper.ExecuteWithCatalogScope(
                    container =>
                        {
                            var membershipUser = Membership.GetUser();
                            var userId = (Guid) membershipUser.ProviderUserKey;

                            var blogArticleMapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogArticleMapper>(container);
                            var blogArticle = blogArticleMapper.Map(model, userId);

                            var blogProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogProcess>(container);
                            blogProcess.AddBlogArticle(blogArticle);

                            return RedirectToAction("Index");
                        });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", ExceptionMessages.GenericExceptionMessage);
                return View(model);
            }
        }

        //
        // GET: /BlogArticle/Edit/5

        [Authorize]
        public async Task<ActionResult> Edit(Guid id)
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                {
//                    var membershipUser = Membership.GetUser();
//                    var userId = (Guid)membershipUser.ProviderUserKey;

                    var blogProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogProcess>(container);
                    var blogArticle = blogProcess.GetBlogArticle(id);

                    var blogArticleMapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogArticleMapper>(container);
                    var model = blogArticleMapper.MapToUpdate(blogArticle);

                    return View(model);
                });
        }

        //
        // POST: /BlogArticle/Edit/5

        [HttpPost, Authorize]
        public ActionResult Edit(Guid id, UpdateBlogArticleModel model)
        {
            try
            {
                return CatalogsConsumerHelper.ExecuteWithCatalogScope(
                    container =>
                        {
                            var blogArticleMapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogArticleMapper>(container);
                            var blogArticle = blogArticleMapper.Map(model, id);

                            var blogProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogProcess>(container);
                            blogProcess.UpdateBlogArticle(blogArticle);

                            return RedirectToAction("Index");
                        });
            }
            catch
            {
                ModelState.AddModelError("", ExceptionMessages.GenericExceptionMessage);
                return View(model);
            }
        }

        //
        // GET: /BlogArticle/Delete/5
 
        [Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                    container =>
                        {
                            var blogProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogProcess>(container);
                            var blogArticle = blogProcess.GetBlogArticle(id);

                            blogProcess.RemoveBlogArticle(blogArticle);

                            return RedirectToAction("Index");
                        });
            }
            catch
            {
                ModelState.AddModelError("", ExceptionMessages.GenericExceptionMessage);
                return View("Edit");
            }
        }

/*
        //
        // POST: /BlogArticle/Delete/5

        [HttpPost, Authorize]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
*/
    }
}