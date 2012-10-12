using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.UnitTests.ModelCreators
{
    public static class PerformanceCreator
    {
        public static IQueryable<Performance> CreateCollection()
        {
            return new List<Performance>
                       {
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                       }
                .AsQueryable();
        }

        public static Performance CreateSingle()
        {
            var entity = new Performance
                                  {
                                      StartDateTime = DateTime.UtcNow.AddMonths(1),
                                      EndDateTime = DateTime.UtcNow.AddMonths(1).AddHours(3),
                                      Price = DateTime.UtcNow.Ticks,
                                  };

            entity.VenueName = string.Format(CultureInfo.InvariantCulture, "VenueName {0}", entity.Id);
            entity.VenueUri = new Uri("http://google.com");
            entity.City = string.Format(CultureInfo.InvariantCulture, "City {0}", entity.Id);
            entity.Info = string.Format(CultureInfo.InvariantCulture, "Info {0}", entity.Id);

            return entity;
        }
    }
}