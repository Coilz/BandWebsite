using System.Collections.Generic;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Domain.Dto;
using Ewk.BandWebsite.Web.UI.Models;
using Ewk.BandWebsite.Web.UI.Models.VideoAdapterSettings;

namespace Ewk.BandWebsite.Web.UI.ModelMappers
{
    public interface IVideoAdapterSettingsMapper
    {
        VideoDetailsModel Map(Video video);
        ItemListModel<VideoDetailsModel> Map(IEnumerable<Video> videos);
        AdapterSettings Map(UpdateVideoAdapterSettingsModel model);
        UpdateVideoAdapterSettingsModel MapToUpdate(AdapterSettings entity);
        VideoAdapterSettingsDetailsModel MapToDetail(AdapterSettings entity);
    }
}