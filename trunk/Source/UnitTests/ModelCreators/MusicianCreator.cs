using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.UnitTests.ModelCreators
{
    public static class MusicianCreator
    {
        public static IQueryable<Musician> CreateCollection()
        {
            return new List<Musician>
                       {
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                       }
                .AsQueryable();
        }

        public static Musician CreateSingle()
        {
            var entity = new Musician
                           {
                               ModificationDate = DateTime.UtcNow.AddMonths(-1),
                               Relation = Musician.ContractType.Member
                           };

            entity.Name = string.Format(CultureInfo.InvariantCulture, "Name {0}", entity.Id);
            entity.Instrument = string.Format(CultureInfo.InvariantCulture, "Instrument {0}", entity.Id);

            return entity;
        }
    }
}