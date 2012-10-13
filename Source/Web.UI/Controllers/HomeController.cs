using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.Resources;
using Ewk.BandWebsite.Web.UI.ModelMappers;
using Ewk.BandWebsite.Web.UI.Models;
using Ewk.BandWebsite.Web.UI.Models.AudioAdapterSettings;
using Ewk.BandWebsite.Web.UI.Models.Home;

namespace Ewk.BandWebsite.Web.UI.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                {
                    var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBandProcess>(container);
                    var entity = process.GetBand();

                    var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBandMapper>(container);
                    var model = mapper.MapToAboutModel(entity);

                    return View(model);
                });
        }

        [Authorize]
        public async Task<ActionResult> EditAbout()
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                {
                    var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBandProcess>(container);
                    var entity = process.GetBand();

                    var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBandMapper>(container);
                    var model = mapper.MapToAboutUpdateModel(entity);

                    return View(model);
                });
        }

        [HttpPost, Authorize]
        public async Task<ActionResult> EditAbout(AboutUpdateModel model)
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                {
                    var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBandMapper>(container);
                    var entity = mapper.Map(model);

                    var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBandProcess>(container);
                    process.UpdateBand(entity);

                    return RedirectToAction("About");
                });
        }

        public ActionResult Shop(int count)
        {
            return PartialView("_ShopSummary");
        }

        public ActionResult Video(int count)
        {
            return PartialView("_VideoSummary");
        }

        public async Task<ActionResult> Music(int count)
        {
            try
            {
                return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                    container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioProcess>(container);
                        var entities = process.GetAudioTracks(0, count)
                            .ToList();

                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioAdapterSettingsMapper>(container);
                        var model = mapper.Map(entities);

                        return PartialView("_AudioSummary", model);
                    });
            }
            catch
            {
                ModelState.AddModelError("", ExceptionMessages.GenericExceptionMessage);
                return PartialView("_AudioSummary", new ItemListModel<AudioDetailsModel> {Title = "Music", Items = new List<AudioDetailsModel>() });
            }
        }

        public async Task<ActionResult> Blog(int count)
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                    {
                        var blogProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogProcess>(container);
                        var blogArticles = blogProcess.GetBlogArticles(0, count)
                            .ToList();

                        var authorIds = blogArticles
                            .Select(article => article.AuthorId)
                            .Distinct();

                        var userProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IUserProcess>(container);
                        var users = authorIds.Select(userProcess.GetUser);

                        var blogArticleMapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBlogArticleMapper>(container);
                        var model = blogArticleMapper.Map(blogArticles, users);

                        return PartialView("_BlogArticleSummary", model);
                    });
        }

        public async Task<ActionResult> Performance(int count)
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                {
                    var performanceProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPerformanceProcess>(container);
                    var performances = performanceProcess.GetPerformances(0, count)
                        .ToList();

                    var performanceMapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPerformanceMapper>(container);
                    var model = performanceMapper.Map(performances);

                    return PartialView("_PerformanceSummary", model);
                });
        }
    }
}