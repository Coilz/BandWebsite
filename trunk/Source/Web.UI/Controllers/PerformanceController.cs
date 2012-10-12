using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.Resources;
using Ewk.BandWebsite.Web.UI.ModelMappers;
using Ewk.BandWebsite.Web.UI.Models.Performance;

namespace Ewk.BandWebsite.Web.UI.Controllers
{
    public class PerformanceController : ControllerBase
    {
        //
        // GET: /Performance/

        public async Task<ActionResult> Index()
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPerformanceProcess>(container);
                        var entities = process.GetPerformances()
                            .OrderBy(entity => entity.StartDateTime)
                            .ToList();

                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPerformanceMapper>(container);
                        var model = mapper.Map(entities);

                        return View(model);
                    });
        }

        //
        // GET: /Performance/Details/5

        public async Task<ActionResult> Details(Guid id)
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPerformanceProcess>(container);
                        var entity = process.GetPerformance(id);

                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPerformanceMapper>(container);
                        var model = mapper.MapToDetail(entity);

                        return View(model);
                    });
        }

        //
        // GET: /Performance/Create

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Performance/Create

        [HttpPost, Authorize]
        public async Task<ActionResult> Create(AddPerformanceModel model)
        {
            try
            {
                return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                    container =>
                        {
                            var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPerformanceMapper>(container);
                            var entity = mapper.Map(model);

                            var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPerformanceProcess>(container);
                            process.AddPerformance(entity);

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
        // GET: /Performance/Edit/5

        [Authorize]
        public async Task<ActionResult> Edit(Guid id)
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                {
                    var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPerformanceProcess>(container);
                    var entity = process.GetPerformance(id);

                    var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPerformanceMapper>(container);
                    var model = mapper.MapToUpdate(entity);

                    return View(model);
                });
        }

        //
        // POST: /Performance/Edit/5

        [HttpPost, Authorize]
        public async Task<ActionResult> Edit(Guid id, UpdatePerformanceModel model)
        {
            try
            {
                return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                    container =>
                        {
                            var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPerformanceMapper>(container);
                            var entity = mapper.Map(model, id);

                            var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPerformanceProcess>(container);
                            process.UpdatePerformance(entity);

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
        // GET: /Performance/Delete/5
 
        [Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                    container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPerformanceProcess>(container);
                        var entity = process.GetPerformance(id);
                        process.RemovePerformance(entity);

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
                // POST: /Performance/Delete/5

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