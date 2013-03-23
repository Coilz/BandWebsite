using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.Resources;
using Ewk.BandWebsite.Web.Common.ModelMappers;
using Ewk.BandWebsite.Web.Common.Models;
using Ewk.BandWebsite.Web.Common.Models.VideoAdapterSettings;

namespace Ewk.BandWebsite.Web.UI.Controllers
{
    public class VideoController : Controller
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
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IVideoProcess>(container);
                        var entities = process.GetVideos()
                            .ToList();

                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IVideoAdapterSettingsMapper>(container);
                        var model = mapper.Map(entities);

                        return View(model);
                    });
            }
            catch
            {
                ModelState.AddModelError("", ExceptionMessages.GenericExceptionMessage);
                return View(new ItemListModel<VideoDetailsModel> { Title = "Video", Items = new List<VideoDetailsModel>() });
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
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IVideoProcess>(container);
                        var entity = process.GetVideo(id);

                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IVideoAdapterSettingsMapper>(container);
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
