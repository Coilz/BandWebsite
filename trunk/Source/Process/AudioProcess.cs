using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ewk.BandWebsite.Adapters;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Domain.Dto;
using Ewk.Configuration;

namespace Ewk.BandWebsite.Process
{
    /// <summary>
    /// Provides methods to process audio.
    /// </summary>
    public class AudioProcess : ProcessBase, IAudioProcess
    {
        private IAudioAdapter _audioAdapter;
        private const string AdapterName = "AudioAdapter";

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="catalogsContainer">A container with all catalogs to access the store.</param>
        public AudioProcess(ICatalogsContainer catalogsContainer)
            :base(catalogsContainer)
        {
        }

        #region Implementation of IAudioProcess

        public Uri GetAuthenticationUri(Uri callbackUrl)
        {
            return AudioAdapter.GetOAuthCalculatedAuthorizationUri(callbackUrl);
        }

        public void Authorize(Uri verificationUri)
        {
            if (verificationUri == null) throw new ArgumentNullException("verificationUri");

            var entity = GetAdapterSettings();

            var accessToken = AudioAdapter.GetOAuthAccessToken(verificationUri);
            entity.OAuthRequestToken = null;
            entity.OAuthAccessToken = accessToken;
            UpdateAdapterSettings(entity);
        }

        public IEnumerable<AudioTrack> GetAudioTracks()
        {
            var adapterSettings = BandRepository.GetAdapterSettings(AdapterName);
            if (adapterSettings.OAuthAccessToken == null)
            {
                throw new AuthorizationException();
            }

            return AudioAdapter.GetItems(adapterSettings.SetName, adapterSettings.OAuthAccessToken);
        }

        public IEnumerable<AudioTrack> GetAudioTracks(int page, int pageSize)
        {
            var adapterSettings = BandRepository.GetAdapterSettings(AdapterName);
            if (adapterSettings.OAuthAccessToken == null)
            {
                throw new AuthorizationException();
            }

            return AudioAdapter.GetItems(adapterSettings.SetName, adapterSettings.OAuthAccessToken, page, pageSize);
        }

        public AudioTrack GetAudioTrack(int id)
        {
            var adapterSettings = BandRepository.GetAdapterSettings(AdapterName);
            if (adapterSettings.OAuthAccessToken == null)
            {
                throw new AuthorizationException();
            }

            return AudioAdapter.GetItem(id, adapterSettings.OAuthAccessToken);
        }

        public string AddAudio(Stream audio, string fileName)
        {
            if (audio == null) throw new ArgumentNullException("audio");
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");

            var adapterSettings = BandRepository.GetAdapterSettings(AdapterName);
            if (adapterSettings.OAuthAccessToken == null)
            {
                throw new AuthorizationException();
            }

            return AudioAdapter.UploadItem(audio, adapterSettings.SetName, fileName, adapterSettings.OAuthAccessToken);
        }

        public AdapterSettings GetAdapterSettings()
        {
            try
            {
                return BandRepository.GetAdapterSettings(AdapterName);
            }
            catch (InvalidOperationException)
            {
                return BandRepository.AddAdapterSettings(new AdapterSettings { AdapterName = AdapterName });
            }
        }

        public AdapterSettings UpdateAdapterSettings(AdapterSettings settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");

            return BandRepository.UpdateAdapterSettings(settings);
        }

        #endregion

        private IAudioAdapter AudioAdapter
        {
            get
            {
                return _audioAdapter ?? (_audioAdapter = DependencyConfiguration.DependencyResolver.Resolve<IAudioAdapter>());
            }
        }
    }
}