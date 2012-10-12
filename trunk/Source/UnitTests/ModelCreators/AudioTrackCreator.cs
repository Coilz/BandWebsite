using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Ewk.BandWebsite.Domain.Dto;

namespace Ewk.BandWebsite.UnitTests.ModelCreators
{
    public static class AudioTrackCreator
    {
        public static IQueryable<AudioTrack> CreateCollection()
        {
            return new List<AudioTrack>
                       {
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                       }
                .AsQueryable();
        }

        public static AudioTrack CreateSingle()
        {
            const string baseUrl = "http://someurl.com";
            var id = Guid.NewGuid().ToString();
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", baseUrl, id);

            return new AudioTrack
                {
                    Id = id,
                    ArtworkUri = url,
                    Description = string.Format(CultureInfo.InvariantCulture, "Description {0}", id),
                    ResourceUri = url,
                    StreamUri = url,
                    Title = string.Format(CultureInfo.InvariantCulture, "Title {0}", id),
                    WaveformUri = url,
                };
        }
    }
}