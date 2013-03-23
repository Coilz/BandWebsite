using System;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Domain.AppModel;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.Web.Common.Models.Home;

namespace Ewk.BandWebsite.Web.Common.ModelMappers
{
    public class BandMapper : IBandMapper
    {
        private readonly ICatalogsContainer _catalogsContainer;
        private IBandProcess _process;

        public BandMapper(ICatalogsContainer catalogsContainer)
        {
            _catalogsContainer = catalogsContainer;
        }

        #region Implementation of IBandMapper

        public AboutModel MapToAboutModel(Band entity)
        {
            return new AboutModel
                       {
                           DateFounded = entity.Founded.ToLocalTime(),
                           Info = entity.Description == null
                                      ? string.Empty
                                      : entity.Description.Replace(Environment.NewLine, "<br />"),
                       };
        }

        public Band Map(AboutUpdateModel model)
        {
            var entity = Process.GetBand();

            entity.Description = string.IsNullOrEmpty(model.Info) ? null : model.Info;
            entity.Founded = model.DateFounded.ToUniversalTime();

            return entity;
        }

        public AboutUpdateModel MapToAboutUpdateModel(Band entity)
        {
            return new AboutUpdateModel
                       {
                           DateFounded = entity.Founded.ToLocalTime(),
                           Info = entity.Description ?? string.Empty,
                       };
        }

        #endregion

        private IBandProcess Process
        {
            get
            {
                return _process ?? (_process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBandProcess>(_catalogsContainer));
            }
        }
    }
}