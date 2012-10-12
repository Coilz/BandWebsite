using System.Collections.Generic;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Domain.Dto;
using Ewk.BandWebsite.Web.UI.Models;
using Ewk.BandWebsite.Web.UI.Models.AudioAdapterSettings;

namespace Ewk.BandWebsite.Web.UI.ModelMappers
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