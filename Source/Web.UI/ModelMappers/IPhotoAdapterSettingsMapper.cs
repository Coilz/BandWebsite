using System.Collections.Generic;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Domain.Dto;
using Ewk.BandWebsite.Web.UI.Models;
using Ewk.BandWebsite.Web.UI.Models.PhotoAdapterSettings;

namespace Ewk.BandWebsite.Web.UI.ModelMappers
{
    public interface IPhotoAdapterSettingsMapper
    {
        PhotoDetailsModel Map(Photo photo);
        ItemListModel<PhotoDetailsModel> Map(IEnumerable<Photo> photos);

        AdapterSettings Map(UpdatePhotoAdapterSettingsModel model);
        UpdatePhotoAdapterSettingsModel MapToUpdate(AdapterSettings entity);
        PhotoAdapterSettingsDetailsModel MapToDetail(AdapterSettings entity);
    }
}