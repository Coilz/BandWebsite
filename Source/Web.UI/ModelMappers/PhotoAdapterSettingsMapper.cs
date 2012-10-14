using System.Linq;
using System.Collections.Generic;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Domain.Dto;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.Web.UI.Models;
using Ewk.BandWebsite.Web.UI.Models.PhotoAdapterSettings;

namespace Ewk.BandWebsite.Web.UI.ModelMappers
{
    public class PhotoAdapterSettingsMapper : IPhotoAdapterSettingsMapper
    {
        private readonly ICatalogsContainer _catalogsContainer;

        private IPhotoProcess _process;

        public PhotoAdapterSettingsMapper(ICatalogsContainer catalogsContainer)
        {
            _catalogsContainer = catalogsContainer;
        }

        #region Implementation of IPhotoAdapterSettingsMapper

        public PhotoDetailsModel Map(Photo photo)
        {
            return new PhotoDetailsModel
                       {
                           Id = photo.Id,
                           Title = photo.Title,
                           Description = photo.Description,
                           LargeUrl = photo.LargeUrl,
                           LargeHeight = photo.LargeHeight,
                           LargeWidth = photo.LargeWidth,
                           MediumUrl = photo.MediumUrl,
                           MediumHeight = photo.MediumHeight,
                           MediumWidth = photo.MediumWidth,
                           OriginalUrl = photo.OriginalUrl,
                           OriginalHeigth = photo.OriginalHeigth,
                           OriginalWidth = photo.OriginalWidth,
                           SmallUrl = photo.SmallUrl,
                           SmallHeight = photo.SmallHeight,
                           SmallWidth = photo.SmallWidth,
                           WebUrl = photo.WebUrl,
                       };
        }

        public ItemListModel<PhotoDetailsModel> Map(IEnumerable<Photo> photos)
        {
            return new ItemListModel<PhotoDetailsModel>
                       {
                           Title = "Photos",
                           Items = photos.Select(Map)
                       };
        }

        public AdapterSettings Map(UpdatePhotoAdapterSettingsModel model)
        {
            var entity = Process.GetAdapterSettings();

            entity.SetName = model.SetName;

            return entity;
        }

        public UpdatePhotoAdapterSettingsModel MapToUpdate(AdapterSettings entity)
        {
            var model = new UpdatePhotoAdapterSettingsModel
                       {
                           SetName = entity.SetName,
                       };

            var oAuthAccesToken = entity.OAuthAccessToken;
            if (oAuthAccesToken == null) return model;

            model.FullName = oAuthAccesToken.FullName;
            model.UserId = oAuthAccesToken.UserId;
            model.UserName = oAuthAccesToken.Username;

            return model;
        }

        public PhotoAdapterSettingsDetailsModel MapToDetail(AdapterSettings entity)
        {
            var model = new PhotoAdapterSettingsDetailsModel
                       {
                           Id = entity.Id,
                           CreationDate = entity.CreationDate,
                           ModificationDate = entity.ModificationDate,

                           SetName = entity.SetName,
                       };

            var oAuthAccesToken = entity.OAuthAccessToken;
            if (oAuthAccesToken == null) return model;

            model.FullName = oAuthAccesToken.FullName;
            model.UserId = oAuthAccesToken.UserId;
            model.UserName = oAuthAccesToken.Username;

            return model;
        }

        #endregion

        private IPhotoProcess Process
        {
            get
            {
                return _process ?? (_process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IPhotoProcess>(_catalogsContainer));
            }
        }
    }
}