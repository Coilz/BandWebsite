using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.UnitTests.ModelCreators
{
    public static class PerformanceCreator
    {
        public static IQueryable<Performance> CreateFutureCollection()
        {
            return new List<Performance>
                       {
                           CreateSingleFuture(),
                           CreateSingleFuture(),
                           CreateSingleFuture(),
                           CreateSingleFuture(),
                           CreateSingleFuture(),
                       }
                .AsQueryable();
        }

        public static IQueryable<Performance> CreatePastCollection()
        {
            return new List<Performance>
                       {
                           CreateSinglePast(),
                           CreateSinglePast(),
                           CreateSinglePast(),
                           CreateSinglePast(),
                           CreateSinglePast(),
                       }
                .AsQueryable();
        }

        public static Performance CreateSingleFuture()
        {
            var start = DateTime.UtcNow.AddMonths(1);
            return CreateSingle(start);
        }

        public static Performance CreateSinglePast()
        {
            var start = DateTime.UtcNow.AddMonths(-1);
            return CreateSingle(start);
        }

        public static Performance CreateSingle(DateTime start)
        {
            var entity = new Performance
                                  {
                                      StartDateTime = start,
                                      EndDateTime = start.AddHours(3),
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