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
using Ewk.BandWebsite.Web.UI.Models.AudioAdapterSettings;

namespace Ewk.BandWebsite.Web.UI.Controllers
{
    public class AudioController : ControllerBase
    {
        //
        // GET: /News/

        public async Task<ActionResult> Index()
        {
            try
            {
                return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                    container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioProcess>(container);
                        var entities = process.GetAudioTracks()
                            .ToList();

                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioAdapterSettingsMapper>(container);
                        var model = mapper.Map(entities);

                        return View(model);
                    });
            }
            catch
            {
                ModelState.AddModelError("", ExceptionMessages.GenericExceptionMessage);
                return View(new ItemListModel<AudioDetailsModel> { Items = new List<AudioDetailsModel>() });
            }
        }

        //
        // GET: /News/Details/5

        public async Task<ActionResult> Details(int id)
        {
            try
            {
                return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                    container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioProcess>(container);
                        var entity = process.GetAudioTrack(id);

                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioAdapterSettingsMapper>(container);
                        var model = mapper.Map(entity);

                        return View(model);
                    });
            }
            catch
            {
                ModelState.AddModelError("", ExceptionMessages.GenericExceptionMessage);
                return View();
            }
        }
/*
        //
        // GET: /News/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /News/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /News/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /News/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /News/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /News/Delete/5

        [HttpPost]
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
