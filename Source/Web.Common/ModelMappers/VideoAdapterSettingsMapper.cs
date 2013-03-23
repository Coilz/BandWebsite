using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Domain.Dto;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.Web.Common.Models;
using Ewk.BandWebsite.Web.Common.Models.VideoAdapterSettings;

namespace Ewk.BandWebsite.Web.Common.ModelMappers
{
    public class VideoAdapterSettingsMapper : IVideoAdapterSettingsMapper
    {
        private readonly ICatalogsContainer _catalogsContainer;

        private IVideoProcess _process;

        public VideoAdapterSettingsMapper(ICatalogsContainer catalogsContainer)
        {
            _catalogsContainer = catalogsContainer;
        }

        #region Implementation of IVideoAdapterSettingsMapper

        public VideoDetailsModel Map(Video entity)
        {
            var url = string.Format((string) @"http://www.youtube.com/embed/{0}",
                    (object) HttpUtility.UrlEncode(entity.Id));

            return new VideoDetailsModel
            {
                Id = entity.Id,
                ArtworkUri = entity.ArtworkUri,
                Description = entity.Description,
                ResourceUri = url,
                StreamUri = entity.StreamUri,
                Title = entity.Title,
            };
        }

        public ItemListModel<VideoDetailsModel> Map(IEnumerable<Video> tracks)
        {
            return new ItemListModel<VideoDetailsModel>
            {
                Title = "Video",
                Items = tracks.Select(Map)
            };
        }

        public AdapterSettings Map(UpdateVideoAdapterSettingsModel model)
        {
            var entity = Process.GetAdapterSettings();

            entity.SetName = model.SetName;

            return entity;
        }

        public UpdateVideoAdapterSettingsModel MapToUpdate(AdapterSettings entity)
        {
            var model = new UpdateVideoAdapterSettingsModel
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

        public VideoAdapterSettingsDetailsModel MapToDetail(AdapterSettings entity)
        {
            var model = new VideoAdapterSettingsDetailsModel
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

        private IVideoProcess Process
        {
            get
            {
                return _process ?? (_process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IVideoProcess>(_catalogsContainer));
            }
        }
    }
}