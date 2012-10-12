using System;
using System.Collections.Generic;
using System.Linq;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.Web.UI.Models;
using Ewk.BandWebsite.Web.UI.Models.Performance;

namespace Ewk.BandWebsite.Web.UI.ModelMappers
{
    public class PerformanceMapper : IPerformanceMapper
    {
        private readonly ICatalogsContainer _catalogsContainer;

        private IPerformanceProcess _process;

        public PerformanceMapper(ICatalogsContainer catalogsContainer)
        {
            _catalogsContainer = catalogsContainer;
        }

        #region Implementation of IPerformanceMapper

        public Performance Map(AddPerformanceModel model)
        {
            return new Performance
                       {
                           StartDateTime = new DateTime(model.Date.Year, model.Date.Month, model.Date.Day,
                                                        model.StartTime.Hour, model.StartTime.Minute,
                                                        model.StartTime.Second, model.StartTime.Millisecond),
                           EndDateTime = new DateTime(model.EndTime.Year, model.EndTime.Month, model.EndTime.Day,
                                                      model.EndTime.Hour, model.EndTime.Minute,
                                                      model.EndTime.Second, model.EndTime.Millisecond),

                           City = model.City,
                           VenueName = model.VenueName,
                           VenueUri = GetUri(model.VenueUrl),
                           Info = model.Info,
                           Price = model.Price,
                       };
        }

        public Performance Map(UpdatePerformanceModel model, Guid performanceId)
        {
            var entity = Process.GetPerformance(performanceId);

            entity.StartDateTime = new DateTime(model.Date.Year, model.Date.Month, model.Date.Day,
                                                     model.StartTime.Hour, model.StartTime.Minute,
                                                     model.StartTime.Second, model.StartTime.Millisecond);
            entity.EndDateTime = new DateTime(model.EndTime.Year, model.EndTime.Month, model.EndTime.Day,
                                                   model.EndTime.Hour, model.EndTime.Minute,
                                                   model.EndTime.Second, model.EndTime.Millisecond);
            entity.City = model.City;
            entity.Info = model.Info;
            entity.Price = model.Price;
            entity.VenueName = model.VenueName;
            entity.VenueUri = GetUri(model.VenueUrl);

            return entity;
        }

        public UpdatePerformanceModel MapToUpdate(Performance entity)
        {
            return new UpdatePerformanceModel
                       {
                           Id = entity.Id,
                           Date = entity.StartDateTime.Date.ToLocalTime(),
                           StartTime = entity.StartDateTime.ToLocalTime(),
                           EndTime = entity.EndDateTime.ToLocalTime(),
                           City = entity.City,
                           Info = entity.Info,
                           Price = entity.Price,
                           VenueName = entity.VenueName,
                           VenueUrl = GetUrl(entity.VenueUri),
                       };
        }

        public PerformanceDetailsModel MapToDetail(Performance entity)
        {
            return new PerformanceDetailsModel
                       {
                           Id = entity.Id,
                           CreationDate = entity.CreationDate,
                           ModificationDate = entity.ModificationDate,

                           Date = entity.StartDateTime.Date.ToLocalTime(),
                           StartTime = entity.StartDateTime.ToLocalTime(),
                           EndTime = entity.EndDateTime.ToLocalTime(),
                           City = entity.City,
                           Info = entity.Info == null ? null : entity.Info.Replace(Environment.NewLine, "<br />"),
                           Price = entity.Price,
                           VenueName = entity.VenueName,
                           VenueUrl = GetUrl(entity.VenueUri),
                       };
        }

        public ItemListModel<PerformanceDetailsModel> Map(IEnumerable<Performance> entities)
        {
            return new ItemListModel<PerformanceDetailsModel>
                       {
                           Title = "Gigs",
                           Items = entities
                               .Select(MapToDetail),
                       };
        }

        #endregion

        private static Uri GetUri(string url)
        {
            return string.IsNullOrEmpty(url)
                       ? null
                       : new Uri(url);
        }

        private static string GetUrl(Uri uri)
        {
            return uri == null
                       ? string.Empty
                       : uri.OriginalString;
        }

        private IPerformanceProcess Process
        {
            get
            {
                return _process ?? (_process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPerformanceProcess>(_catalogsContainer));
            }
        }
    }
}