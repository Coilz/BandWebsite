using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.Resources;
using Ewk.BandWebsite.Web.UI.ModelMappers;
using Ewk.BandWebsite.Web.UI.Models.AudioAdapterSettings;

namespace Ewk.BandWebsite.Web.UI.Controllers
{
    public class AudioAdapterSettingsController : ControllerBase
    {
        //
        // GET: /AudioAdapterSettings/

        [Authorize]
        public async Task<ActionResult> Index()
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                {
                    var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioProcess>(container);
                    var entity = process.GetAdapterSettings();

                    var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioAdapterSettingsMapper>(container);
                    var model = mapper.MapToDetail(entity);

                    return View(model);
                });
        }

        //
        // GET: /AudioAdapterSettings/Edit

        [Authorize]
        public async Task<ActionResult> Edit()
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync<ActionResult>(
                container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioProcess>(container);
                        var entity = process.GetAdapterSettings();

                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioAdapterSettingsMapper>(container);
                        var model = mapper.MapToUpdate(entity);

                        return View(model);
                    });
        }

        //
        // POST: /AudioAdapterSettings/Edit

        [Authorize, HttpPost]
        public async Task<ActionResult> Edit(UpdateAudioAdapterSettingsModel model)
        {
            try
            {
                return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                    container =>
                    {
                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioAdapterSettingsMapper>(container);
                        var entity = mapper.Map(model);

                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioProcess>(container);
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
                            var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioProcess>(container);
                            var uri = new Uri(Request.Url.GetLeftPart(UriPartial.Authority));
                            var callbackUri = new Uri(uri, "AudioAdapterSettings/Authorized");
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
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioProcess>(container);
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
        public ActionResult UploadAudio()
        {
            return View();
        }

        [Authorize, HttpPost]
        public async Task<ActionResult> UploadAudio(string dummy)
        {
            try
            {
                return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                    container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioProcess>(container);

                        foreach (var fileName in Request.Files.AllKeys)
                        {
                            var file = Request.Files[fileName];
                            if (file == null) continue;

                            process.AddAudio(file.InputStream, file.FileName);
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