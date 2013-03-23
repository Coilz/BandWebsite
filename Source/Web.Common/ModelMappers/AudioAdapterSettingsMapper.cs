using System.Collections.Generic;
using System.Linq;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Domain.Dto;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.Web.Common.Models;
using Ewk.BandWebsite.Web.Common.Models.AudioAdapterSettings;

namespace Ewk.BandWebsite.Web.Common.ModelMappers
{
    public class AudioAdapterSettingsMapper : IAudioAdapterSettingsMapper
    {
        private readonly ICatalogsContainer _catalogsContainer;

        private IAudioProcess _process;

        public AudioAdapterSettingsMapper(ICatalogsContainer catalogsContainer)
        {
            _catalogsContainer = catalogsContainer;
        }

        #region Implementation of IAudioAdapterSettingsMapper

        public AudioDetailsModel Map(AudioTrack track)
        {
            return new AudioDetailsModel
                {
                    Id = track.Id,
                    ArtworkUri = track.ArtworkUri,
                    Description = track.Description,
                    ResourceUri = track.ResourceUri,
                    StreamUri = track.StreamUri,
                    Title = track.Title,
                    WaveformUri = track.WaveformUri,
                };
        }

        public ItemListModel<AudioDetailsModel> Map(IEnumerable<AudioTrack> tracks)
        {
            return new ItemListModel<AudioDetailsModel>
                {
                    Title = "Music",
                    Items = tracks.Select(Map)
                };
        }

        public AdapterSettings Map(UpdateAudioAdapterSettingsModel model)
        {
            var entity = Process.GetAdapterSettings();

            entity.SetName = model.SetName;

            return entity;
        }

        public UpdateAudioAdapterSettingsModel MapToUpdate(AdapterSettings entity)
        {
            var model = new UpdateAudioAdapterSettingsModel
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

        public AudioAdapterSettingsDetailsModel MapToDetail(AdapterSettings entity)
        {
            var model = new AudioAdapterSettingsDetailsModel
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

        private IAudioProcess Process
        {
            get
            {
                return _process ?? (_process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAudioProcess>(_catalogsContainer));
            }
        }
    }
}