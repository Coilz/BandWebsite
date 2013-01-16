using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Domain.Dto;
using Google.GData.Client;
using Google.GData.YouTube;

namespace Ewk.BandWebsite.Adapters.Youtube
{
    public class VideoAdapter : IVideoAdapter
    {
        private readonly string _apiKey;

        public VideoAdapter()
        {
            _apiKey = ConfigurationManager.AppSettings["VideoApiKey"] ?? "978012847617.apps.googleusercontent.com";

            AuthSubUtil.getRequestUrl("RedirectUrl", "scope", true, true);

            var request = GetRequest();
            var videoStreams = request.GetVideoFeed("UserName").Entries.Select(video => video.MediaSource.GetDataStream());
        }

        private Google.YouTube.YouTubeRequest GetRequest()
        {
            var settings = new Google.YouTube.YouTubeRequestSettings("BandApp", _apiKey)
                {
                    AutoPaging = true
                };

            return new Google.YouTube.YouTubeRequest(settings);
        }

        public IEnumerable<Google.YouTube.Playlist> PlayLists()
        {
            try
            {
                var request = GetRequest();
                var feed = request.GetPlaylistsFeed(null);

                return feed.Entries;
            }
            catch (GDataRequestException gdre)
            {
                var response = (HttpWebResponse)gdre.Response;
                return null;
            }
        }

        private IEnumerable<Google.YouTube.Video> GetVideos(string videofeed)
        {
            // YouTubeQuery.CreatePlaylistsUri()
            var query = new YouTubeQuery(videofeed);

            return GetVideos(query);
        }

        private IEnumerable<Google.YouTube.Video> GetVideos(FeedQuery q)
        {
            try
            {
                var request = GetRequest();
                var feed = request.Get<Google.YouTube.Video>(q);

                return feed.Entries;
            }
            catch (GDataRequestException gdre)
            {
                var response = (HttpWebResponse)gdre.Response;
                return null;
            }
        }

        #region Implementation of IVideoAdapter

        public Uri GetOAuthCalculatedAuthorizationUri(Uri callbackUri)
        {
            var apiSecret = ConfigurationManager.AppSettings["VideoApiSecret"] ?? "nnb4e_-c3TSKzf8kmwrgp5wr";

            var parameters = new OAuth2Parameters
                {
                    ClientId = _apiKey,
                    ClientSecret = apiSecret,
                    Scope = "http://gdata.youtube.com",
                };

            var authenticator = new OAuth2Authenticator("BandApp", parameters);

            return authenticator.ApplyAuthenticationToUri(callbackUri);
        }

        public OAuthAccessToken GetOAuthAccessToken(Uri currentUri)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Video> GetItems(string setName, OAuthAccessToken accessToken)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Video> GetItems(string setName, OAuthAccessToken accessToken, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Video GetItem(int id, OAuthAccessToken accessToken)
        {
            throw new NotImplementedException();
        }

        public string UploadItem(Stream data, string setName, string title, OAuthAccessToken accessToken)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}