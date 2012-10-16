using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Google.GData.Client;
using Google.GData.YouTube;
using Google.YouTube;

namespace Ewk.BandWebsite.Adapters.Youtube
{
    public class VideoAdapter
    {
        private readonly string _apiKey;

        public VideoAdapter()
        {
            _apiKey = ConfigurationManager.AppSettings["VideoApiKey"] ?? "AI39si6Hg6vvtVi5Q9BOxXK35kGSRvQrle9twwIfjNPtkvdL-awrCH5Ti3YWOHyHDPjNxK0yvlBor-sRNcwSXnnG5q1BdzkJ0A";

            AuthSubUtil.getRequestUrl("RedirectUrl", "scope", true, true);

            var request = GetRequest();
            var videoStreams = request.GetVideoFeed("UserName").Entries.Select(video => video.MediaSource.GetDataStream());
        }

        private YouTubeRequest GetRequest()
        {
            var settings = new YouTubeRequestSettings("BandApp", _apiKey)
                {
                    AutoPaging = true
                };

            return new YouTubeRequest(settings);
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

        private IEnumerable<Video> GetVideos(FeedQuery q)
        {
            try
            {
                var request = GetRequest();
                var feed = request.Get<Video>(q);

                return feed.Entries;
            }
            catch (GDataRequestException gdre)
            {
                var response = (HttpWebResponse)gdre.Response;
                return null;
            }
        }
    }
}