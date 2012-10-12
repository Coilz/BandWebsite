using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Adapters.Flickr
{
    public class PhotoAdapter : IPhotoAdapter
    {
        private readonly FlickrNet.Flickr _flickr;

        public PhotoAdapter()
        {
            var apiKey = ConfigurationManager.AppSettings["PhotoApiKey"] ?? "b2247ed8cfe512cbbf381a5c0907c8cb";
            var apiSecret = ConfigurationManager.AppSettings["PhotoApiSecret"] ?? "ce369460c3b90d24";

            _flickr = new FlickrNet.Flickr(apiKey, apiSecret);
        }

        #region Implementation of IOAuth2

        public OAuthRequestToken GetOAuthRequestToken(Uri requestUri)
        {
            var requestToken = _flickr.OAuthGetRequestToken(requestUri.AbsoluteUri);

            return new OAuthRequestToken
                       {
                           Token = requestToken.Token,
                           TokenSecret = requestToken.TokenSecret,
                       };
        }

        public Uri GetOAuthCalculatedAuthorizationUri(OAuthRequestToken requestToken)
        {
            if (requestToken == null) throw new ArgumentNullException("requestToken");
            if (string.IsNullOrEmpty(requestToken.Token)) throw new InvalidOperationException();

            var oAuthUrl = _flickr.OAuthCalculateAuthorizationUrl(requestToken.Token, FlickrNet.AuthLevel.Write);

            return new Uri(oAuthUrl);
        }

        public OAuthAccessToken GetOAuthAccessToken(OAuthRequestToken requestToken, string verifier)
        {
            if (requestToken == null) throw new ArgumentNullException("requestToken");

            var accessToken = _flickr.OAuthGetAccessToken(requestToken.Token, requestToken.TokenSecret, verifier);

            return new OAuthAccessToken
                       {
                           FullName = accessToken.FullName,
                           Token = accessToken.Token,
                           TokenSecret = accessToken.TokenSecret,
                           UserId = accessToken.UserId,
                           Username = accessToken.Username,
                       };
        }

        #endregion

        #region Implementation of IPhotoAdapter

        public IEnumerable<string> GetItems(string setName, OAuthAccessToken accessToken)
        {
            if (accessToken == null) throw new ArgumentNullException("accessToken");

            _flickr.Authorize(accessToken);

            if (string.IsNullOrEmpty(setName))
            {
                var publicPhotos = _flickr.PeopleGetPublicPhotos(accessToken.UserId);

                return publicPhotos
                    .Select(photo =>
                            photo.LargeUrl);
            }

            var photoSets = _flickr.PhotosetsGetList();
            var photoSet = photoSets
                .SingleOrDefault(p => p.Title == setName);

            if (photoSet == null)
            {
                return new List<string>();
            }
            
            var photos = _flickr.PhotosetsGetPhotos(photoSet.PhotosetId);
            
            return photos
                .Select(photo =>
                        photo.LargeUrl);
        }

        public string UploadItem(Stream data, string setName, string fileName, OAuthAccessToken accessToken)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (accessToken == null) throw new ArgumentNullException("accessToken");

            _flickr.Authorize(accessToken);

            var photoId = _flickr.UploadPicture(
                data,
                fileName,
                Path.GetFileNameWithoutExtension(fileName),
                description: "",
                tags: "music band",
                isPublic: true,
                isFamily: false,
                isFriend: false,
                contentType: FlickrNet.ContentType.Photo,
                safetyLevel: FlickrNet.SafetyLevel.Safe,
                hiddenFromSearch: FlickrNet.HiddenFromSearch.Visible);

            if (string.IsNullOrEmpty(setName)) return photoId;

            var photoSets = _flickr.PhotosetsGetList();
            var photoSet = photoSets
                .SingleOrDefault(p => p.Title == setName);

            if (photoSet == null)
            {
                _flickr.PhotosetsCreate(setName, photoId);
            }
            else
            {
                _flickr.PhotosetsAddPhoto(photoSet.PhotosetId, photoId);
            }

            return photoId;
        }

        #endregion
    }
}