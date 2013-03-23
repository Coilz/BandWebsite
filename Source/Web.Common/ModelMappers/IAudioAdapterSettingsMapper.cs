using System.Collections.Generic;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Domain.Dto;
using Ewk.BandWebsite.Web.Common.Models;
using Ewk.BandWebsite.Web.Common.Models.AudioAdapterSettings;

namespace Ewk.BandWebsite.Web.Common.ModelMappers
{
    public interface IAudioAdapterSettingsMapper
    {
        AudioDetailsModel Map(AudioTrack track);
        ItemListModel<AudioDetailsModel> Map(IEnumerable<AudioTrack> tracks);
        AdapterSettings Map(UpdateAudioAdapterSettingsModel model);
        UpdateAudioAdapterSettingsModel MapToUpdate(AdapterSettings entity);
        AudioAdapterSettingsDetailsModel MapToDetail(AdapterSettings entity);
    }
}