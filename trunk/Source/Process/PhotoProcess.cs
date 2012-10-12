using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ewk.BandWebsite.Adapters;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.Configuration;
using Ewk.Extensions;

namespace Ewk.BandWebsite.Process
{
    /// <summary>
    /// Provides methods to process photos.
    /// </summary>
    public class PhotoProcess : ProcessBase, IPhotoProcess
    {
        private IPhotoAdapter _photoAdapter;
        private const string AdapterName = "PhotoAdapter";

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="catalogsContainer">A container with all catalogs to access the store.</param>
        public PhotoProcess(ICatalogsContainer catalogsContainer)
            :base(catalogsContainer)
        {
        }

        #region Implementation of IPhotoProcess

        public Uri GetAuthenticationUri(Uri currentLocation)
        {
            var requestToken = PhotoAdapter.GetOAuthRequestToken(currentLocation);
            
            var entity = GetAdapterSettings();
            entity.OAuthRequestToken = requestToken;
            entity.OAuthAccessToken = null;
            UpdateAdapterSettings(entity);

            return PhotoAdapter.GetOAuthCalculatedAuthorizationUri(requestToken);
        }

        public void Authorize(Uri verificationUri)
        {
            if (verificationUri == null) throw new ArgumentNullException("verificationUri");

            var verifier = verificationUri.GetQueryParameters()["oauth_verifier"];
            var entity = GetAdapterSettings();

            if (string.IsNullOrEmpty(verifier) || entity.OAuthRequestToken == null)
            {
                throw new AuthorizationException();
            }

            var accessToken = PhotoAdapter.GetOAuthAccessToken(entity.OAuthRequestToken, verifier);
            entity.OAuthRequestToken = null;
            entity.OAuthAccessToken = accessToken;
            UpdateAdapterSettings(entity);
        }

        public IEnumerable<Uri> GetPhotos()
        {
            var adapterSettings = BandRepository.GetAdapterSettings(AdapterName);
            if (adapterSettings.OAuthAccessToken == null)
            {
                throw new AuthorizationException();
            }

            var urls = PhotoAdapter.GetItems(adapterSettings.SetName, adapterSettings.OAuthAccessToken);

            return urls
                .Select(url =>
                        new Uri(url));
        }

        public string AddPhoto(Stream photo, string fileName)
        {
            if (photo == null) throw new ArgumentNullException("photo");
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");

            var adapterSettings = BandRepository.GetAdapterSettings(AdapterName);
            if (adapterSettings.OAuthAccessToken == null)
            {
                throw new AuthorizationException();
            }

            return PhotoAdapter.UploadItem(photo, adapterSettings.SetName, fileName, adapterSettings.OAuthAccessToken);
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

        private IPhotoAdapter PhotoAdapter
        {
            get
            {
                return _photoAdapter ?? (_photoAdapter = DependencyConfiguration.DependencyResolver.Resolve<IPhotoAdapter>());
            }
        }
    }
}