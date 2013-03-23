using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Common;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.Web.Common.ModelMappers;
using Ewk.BandWebsite.Web.Common.Models;
using Ewk.BandWebsite.Web.Common.Models.AudioAdapterSettings;
using Ewk.Configuration;

namespace Ewk.BandWebsite.Web.UI.Controllers
{
    public class AudioApiController : ApiController
    {
        public async Task<ItemListModel<AudioDetailsModel>> Get(Guid bandId)
        {
            var bandIdInstaller = DependencyConfiguration.DependencyResolver.Resolve<IBandIdInstaller>();
            bandIdInstaller.SetBandId(bandId);

            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioProcess>(container);
                        var entities = process.GetAudioTracks()
                                              .ToList();

                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioAdapterSettingsMapper>(container);
                        var model = mapper.Map(entities);

                        return model;
                    });
        }

        public async Task<AudioDetailsModel> Get(Guid bandId, int id)
        {
            var bandIdInstaller = DependencyConfiguration.DependencyResolver.Resolve<IBandIdInstaller>();
            bandIdInstaller.SetBandId(bandId);

            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioProcess>(container);
                        var entity = process.GetAudioTrack(id);

                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioAdapterSettingsMapper>(container);
                        var model = mapper.Map(entity);

                        return model;
                    });
        }

        public async Task<ItemListModel<AudioDetailsModel>> Get(Guid bandId, int page, int pageSize)
        {
            var bandIdInstaller = DependencyConfiguration.DependencyResolver.Resolve<IBandIdInstaller>();
            bandIdInstaller.SetBandId(bandId);

            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioProcess>(container);
                        var entities = process.GetAudioTracks(page, pageSize)
                                              .ToList();

                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioAdapterSettingsMapper>(container);
                        return mapper.Map(entities);
                    });
        }
    }
}