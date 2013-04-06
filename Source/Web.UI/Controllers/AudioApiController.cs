using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.Web.Common;
using Ewk.BandWebsite.Web.Common.ModelMappers;
using Ewk.BandWebsite.Web.Common.Models;
using Ewk.BandWebsite.Web.Common.Models.AudioAdapterSettings;

namespace Ewk.BandWebsite.Web.UI.Controllers
{
    public class AudioApiController : ApiController
    {
        [BandIdFilter]
        public async Task<IQueryable<AudioDetailsModel>> GetAsync(Guid bandId)
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioProcess>(container);
                        var entities = process.GetAudioTracks()
                                              .ToList();

                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioAdapterSettingsMapper>(container);
                        return mapper.Map(entities).Items.AsQueryable();
                    });
        }

        [BandIdFilter]
        public async Task<IEnumerable<AudioDetailsModel>> GetAsync(Guid bandId, int page, int pageSize)
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioProcess>(container);
                        var entities = process.GetAudioTracks(page, pageSize)
                                              .ToList();

                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioAdapterSettingsMapper>(container);
                        return mapper.Map(entities).Items;
                    });
        }

        [BandIdFilter]
        public async Task<AudioDetailsModel> GetAsync(Guid bandId, int id)
        {
            return await CatalogsConsumerHelper.ExecuteWithCatalogScopeAsync(
                container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioProcess>(container);
                        var entity = process.GetAudioTrack(id);

                        var mapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioAdapterSettingsMapper>(container);
                        return mapper.Map(entity);
                    });
        }
    }
}