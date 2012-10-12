using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.Resources;
using Ewk.BandWebsite.Web.UI.ModelMappers;
using Ewk.BandWebsite.Web.UI.Models;
using Ewk.BandWebsite.Web.UI.Models.PhotoAdapterSettings;

namespace Ewk.BandWebsite.Web.UI.Controllers
{
    public class PhotoAdapterSettingsController : ControllerBase
    {
        //
        // GET: /PhotoAdapterSettings/

        [Authorize]
        public async Task<ActionResult> Index()
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                {
                    var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPhotoProcess>(container);
                    var entity = process.GetAdapterSettings();

                    var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPhotoAdapterSettingsMapper>(container);
                    var model = mapper.MapToDetail(entity);

                    return View(model);
                });
        }

        //
        // GET: /PhotoAdapterSettings/Edit

        [Authorize]
        public async Task<ActionResult> Edit()
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync<ActionResult>(
                container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPhotoProcess>(container);
                        var entity = process.GetAdapterSettings();

                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPhotoAdapterSettingsMapper>(container);
                        var model = mapper.MapToUpdate(entity);

                        return View(model);
                    });
        }

        //
        // POST: /PhotoAdapterSettings/Edit

        [Authorize, HttpPost]
        public async Task<ActionResult> Edit(UpdatePhotoAdapterSettingsModel model)
        {
            try
            {
                return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                    container =>
                    {
                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPhotoAdapterSettingsMapper>(container);
                        var entity = mapper.Map(model);

                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPhotoProcess>(container);
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
                            var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPhotoProcess>(container);
                            var uri = new Uri(Request.Url.GetLeftPart(UriPartial.Authority));
                            var callbackUri = new Uri(uri, "PhotoAdapterSettings/Authorized");
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
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPhotoProcess>(container);
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

        public async Task<ActionResult> Photos()
        {
            try
            {
                return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                    container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPhotoProcess>(container);
                        var entities = process.GetPhotos()
                            .ToList();

                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPhotoAdapterSettingsMapper>(container);
                        var model = mapper.Map(entities);

                        return PartialView("_Photos", model);
                    });
            }
            catch
            {
                ModelState.AddModelError("", ExceptionMessages.GenericExceptionMessage);
                return PartialView("_Photos", new ItemListModel<PhotoDetailsModel> {Title = "Photos", Items = new List<PhotoDetailsModel>()});
            }
        }

        [Authorize]
        public ActionResult UploadPhoto()
        {
            return View();
        }

        [Authorize, HttpPost]
        public async Task<ActionResult> UploadPhoto(string dummy)
        {
            try
            {
                return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                    container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPhotoProcess>(container);

                        foreach (var fileName in Request.Files.AllKeys)
                        {
                            var file = Request.Files[fileName];
                            if (file == null) continue;

                            process.AddPhoto(file.InputStream, file.FileName);
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