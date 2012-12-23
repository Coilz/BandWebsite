using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.Resources;
using Ewk.BandWebsite.Web.UI.ModelMappers;
using Ewk.BandWebsite.Web.UI.Models.VideoAdapterSettings;

namespace Ewk.BandWebsite.Web.UI.Controllers
{
    public class VideoAdapterSettingsController : ControllerBase
    {
        //
        // GET: /VideoAdapterSettings/

        [Authorize]
        public async Task<ActionResult> Index()
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                {
                    var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IVideoProcess>(container);
                    var entity = process.GetAdapterSettings();

                    var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IVideoAdapterSettingsMapper>(container);
                    var model = mapper.MapToDetail(entity);

                    return View(model);
                });
        }

        //
        // GET: /VideoAdapterSettings/Edit

        [Authorize]
        public async Task<ActionResult> Edit()
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync<ActionResult>(
                container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IVideoProcess>(container);
                        var entity = process.GetAdapterSettings();

                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IVideoAdapterSettingsMapper>(container);
                        var model = mapper.MapToUpdate(entity);

                        return View(model);
                    });
        }

        //
        // POST: /VideoAdapterSettings/Edit

        [Authorize, HttpPost]
        public async Task<ActionResult> Edit(UpdateVideoAdapterSettingsModel model)
        {
            try
            {
                return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                    container =>
                    {
                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IVideoAdapterSettingsMapper>(container);
                        var entity = mapper.Map(model);

                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IVideoProcess>(container);
                        process.UpdateAdapterSettings(entity);

                        return RedirectToAction("Index");
                    });
            }
            catch
            {
                ModelState.AddModelError("", ExceptionMessages.GenericExceptionMessage);
                return View(model);
            }
        }

        [Authorize]
        public async Task<ActionResult> Authorize()
        {
            try
            {
                return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync<ActionResult>(
                    container =>
                        {
                            var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IVideoProcess>(container);
                            var uri = new Uri(Request.Url.GetLeftPart(UriPartial.Authority));
                            var callbackUri = new Uri(uri, "VideoAdapterSettings/Authorized");
                            var authUri = process.GetAuthenticationUri(callbackUri);

                            return Redirect(authUri.AbsoluteUri);
                        });
            }
            catch
            {
                ModelState.AddModelError("", ExceptionMessages.GenericExceptionMessage);
                return View("Index");
            }
        }

        [Authorize]
        public async Task<ActionResult> Authorized()
        {
            try
            {
                return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync<ActionResult>(
                    container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IVideoProcess>(container);
                        process.Authorize(Request.Url);

                        return RedirectToAction("Index");
                    });
            }
            catch
            {
                ModelState.AddModelError("", ExceptionMessages.GenericExceptionMessage);
                return View("Index");
            }
        }

        [Authorize]
        public ActionResult UploadVideo()
        {
            return View();
        }

        [Authorize, HttpPost]
        public async Task<ActionResult> UploadVideo(string dummy)
        {
            try
            {
                return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                    container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IVideoProcess>(container);

                        foreach (var fileName in Request.Files.AllKeys)
                        {
                            var file = Request.Files[fileName];
                            if (file == null) continue;

                            process.AddVideo(file.InputStream, file.FileName);
                        }

                        return View();
                    });
            }
            catch
            {
                ModelState.AddModelError("", ExceptionMessages.GenericExceptionMessage);
                return View();
            }
        }
    }
}