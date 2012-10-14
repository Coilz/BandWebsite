using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Ewk.BandWebsite.Domain.Dto;

namespace Ewk.BandWebsite.UnitTests.ModelCreators
{
    public static class PhotoCreator
    {
        public static IQueryable<Photo> CreateCollection()
        {
            return new List<Photo>
                       {
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                       }
                .AsQueryable();
        }

        public static Photo CreateSingle()
        {
            const string baseUrl = "http://someurl.com";
            var id = Guid.NewGuid().ToString();
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", baseUrl, id);

            return new Photo
                {
                    Id = id,

                    OriginalUrl = url,
                    OriginalWidth = 500,
                    OriginalHeigth = 400,

                    LargeUrl = url,
                    LargeWidth = 500,
                    LargeHeight = 400,

                    MediumUrl = url,
                    MediumWidth = 200,
                    MediumHeight = 160,

                    SmallUrl = url,
                    SmallWidth = 50,
                    SmallHeight = 40,

                    WebUrl = url,

                    Description = string.Format(CultureInfo.InvariantCulture, "Description {0}", id),
                    Title = string.Format(CultureInfo.InvariantCulture, "Title {0}", id),
                };
        }
    }
}