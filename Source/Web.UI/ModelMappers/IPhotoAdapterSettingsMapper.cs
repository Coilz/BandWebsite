using System;
using System.Collections.Generic;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Web.UI.Models;
using Ewk.BandWebsite.Web.UI.Models.PhotoAdapterSettings;

namespace Ewk.BandWebsite.Web.UI.ModelMappers
{
    public interface IPhotoAdapterSettingsMapper
    {
        PhotoDetailsModel Map(Uri photo);
        ItemListModel<PhotoDetailsModel> Map(IEnumerable<Uri> photos);

        AdapterSettings Map(UpdatePhotoAdapterSettingsModel model);
        UpdatePhotoAdapterSettingsModel MapToUpdate(AdapterSettings entity);
        PhotoAdapterSettingsDetailsModel MapToDetail(AdapterSettings entity);
    }
}