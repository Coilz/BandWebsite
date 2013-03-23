using System.Collections.Generic;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Domain.Dto;
using Ewk.BandWebsite.Web.Common.Models;
using Ewk.BandWebsite.Web.Common.Models.VideoAdapterSettings;

namespace Ewk.BandWebsite.Web.Common.ModelMappers
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