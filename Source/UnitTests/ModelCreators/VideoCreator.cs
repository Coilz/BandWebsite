using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Ewk.BandWebsite.Domain.Dto;

namespace Ewk.BandWebsite.UnitTests.ModelCreators
{
    public static class VideoCreator
    {
        public static IQueryable<Video> CreateCollection()
        {
            return new List<Video>
                       {
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                       }
                .AsQueryable();
        }

        public static Video CreateSingle()
        {
            const string baseUrl = "http://someurl.com";
            var id = Guid.NewGuid().ToString();
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", baseUrl, id);

            return new Video
                {
                    Id = id,
                    ArtworkUri = url,
                    Description = string.Format(CultureInfo.InvariantCulture, "Description {0}", id),
                    ResourceUri = url,
                    StreamUri = url,
                    Title = string.Format(CultureInfo.InvariantCulture, "Title {0}", id),
                };
        }
    }
}