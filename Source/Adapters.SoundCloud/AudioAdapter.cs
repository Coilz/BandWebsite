using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Domain.Dto;
using Ewk.SoundCloud.ApiLibrary.Entities;
using Api = Ewk.SoundCloud.ApiLibrary;

namespace Ewk.BandWebsite.Adapters.SoundCloud
{
    public class AudioAdapter : IAudioAdapter
    {
        private readonly string _apiKey;

        public AudioAdapter()
        {
            _apiKey = ConfigurationManager.AppSettings["AudioApiKey"] ?? "daf9c365e9b0c127f9863841c6e85434";
        }

        #region Implementation of IOAuth2

        public Uri GetOAuthCalculatedAuthorizationUri(Uri callbackUri)
        {
            var soundCloud = new Api.SoundCloud(_apiKey);
            return soundCloud.GetAuthorizeUri(callbackUri);
        }

        public OAuthAccessToken GetOAuthAccessToken(Uri currentUri)
        {
            var apiSecret = ConfigurationManager.AppSettings["AudioApiSecret"] ?? "141710b5a9167c693c3e67ad60377fc1";

            var soundCloud = new Api.SoundCloud(_apiKey);
            var token = soundCloud.RequestOAuthToken(currentUri, apiSecret);
            var me = soundCloud.Me.Get();

            return new OAuthAccessToken
            {
                Token = token.access_token,
                FullName = me.full_name,
                UserId = me.id.ToString(CultureInfo.InvariantCulture),
                Username = me.username,
            };
        }

        #endregion

        #region Implementation of IAudioAdapter

        public IEnumerable<AudioTrack> GetItems(string setName, OAuthAccessToken accessToken)
        {
            var soundCloud = new Api.SoundCloud(_apiKey, accessToken.Token);

            if (string.IsNullOrEmpty(setName))
            {
                return soundCloud.Me.Tracks.Get()
                    .Select(Map);
            }

            return soundCloud.Me.Playlists.Get()
                .Single(p => p.title == setName)
                .tracks
                .Select(Map);
        }

        public IEnumerable<AudioTrack> GetItems(string setName, OAuthAccessToken accessToken, int page, int pageSize)
        {
            var soundCloud = new Api.SoundCloud(_apiKey, accessToken.Token);
            soundCloud.SetPageSize(pageSize);

            if (string.IsNullOrEmpty(setName))
            {
                return soundCloud.Me.Tracks.Get()
                    .Skip(page*pageSize)
                    .Take(pageSize)
                    .Select(Map);
            }

            return soundCloud.Me.Playlists.Get()
                .Single(p => p.title == setName)
                .tracks
                .Skip(page*pageSize)
                .Take(pageSize)
                .Select(Map);
        }

        public AudioTrack GetItem(int id, OAuthAccessToken accessToken)
        {
            var soundCloud = new Api.SoundCloud(_apiKey, accessToken.Token);

            return Map(soundCloud.Tracks[id].Get());
        }

        public string UploadItem(Stream data, string setName, string title, OAuthAccessToken accessToken)
        {
            var soundCloud = new Api.SoundCloud(_apiKey, accessToken.Token);

            byte[] buffer;
            using (var ms = new MemoryStream())
            {
                data.CopyTo(ms);
                buffer = ms.ToArray();
            }
            var track = new Track
                {
                    asset_data = buffer,
                    title = title
                };

            return soundCloud.Tracks.Post(track).stream_url;
        }

        private static AudioTrack Map(Track track)
        {
            return new AudioTrack
                {
                    Id = track.id.ToString(CultureInfo.InvariantCulture),
                    ArtworkUri = track.artwork_url,
                    Description = track.description,
                    ResourceUri = track.uri,
                    StreamUri = track.stream_url,
                    Title = track.title,
                    WaveformUri = track.waveform_url,
                };
        }

        #endregion
    }
}