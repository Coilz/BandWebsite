using System;
using System.Collections.Generic;
using System.IO;
using Ewk.BandWebsite.Adapters;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Domain.Dto;
using Ewk.Configuration;

namespace Ewk.BandWebsite.Process
{
    public class VideoProcess : ProcessBase, IVideoProcess
    {
        private IVideoAdapter _videoAdapter;
        private const string AdapterName = "VideoAdapter";

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="catalogsContainer">A container with all catalogs to access the store.</param>
        public VideoProcess(ICatalogsContainer catalogsContainer)
            :base(catalogsContainer)
        {
        }

        #region Implementation of IVideoAdapter

        public Uri GetAuthenticationUri(Uri callbackUri)
        {
            throw new NotImplementedException();
        }

        public void Authorize(Uri verificationUri)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Video> GetVideos()
        {
            var adapterSettings = GetAdapterSettings();
            if (adapterSettings.OAuthAccessToken == null)
            {
                throw new AuthorizationException();
            }

            return VideoAdapter.GetItems(adapterSettings.SetName, adapterSettings.OAuthAccessToken);
        }

        public IEnumerable<Video> GetVideos(int page, int pageSize)
        {
            var adapterSettings = GetAdapterSettings();
            if (adapterSettings.OAuthAccessToken == null)
            {
                throw new AuthorizationException();
            }

            return VideoAdapter.GetItems(adapterSettings.SetName, adapterSettings.OAuthAccessToken, page, pageSize);
        }

        public Video GetVideo(int id)
        {
            var adapterSettings = GetAdapterSettings();
            if (adapterSettings.OAuthAccessToken == null)
            {
                throw new AuthorizationException();
            }

            return VideoAdapter.GetItem(id, adapterSettings.OAuthAccessToken);
        }

        public string AddVideo(Stream video, string fileName)
        {
            if (video == null) throw new ArgumentNullException("video");
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");

            var adapterSettings = BandRepository.GetAdapterSettings(AdapterName);
            if (adapterSettings.OAuthAccessToken == null)
            {
                throw new AuthorizationException();
            }

            return VideoAdapter.UploadItem(video, adapterSettings.SetName, fileName, adapterSettings.OAuthAccessToken);
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

        private IVideoAdapter VideoAdapter
        {
            get
            {
                return _videoAdapter ?? (_videoAdapter = DependencyConfiguration.DependencyResolver.Resolve<IVideoAdapter>());
            }
        }
    }
}
