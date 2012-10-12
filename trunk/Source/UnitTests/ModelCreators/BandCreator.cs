using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Ewk.BandWebsite.Domain.AppModel;

namespace Ewk.BandWebsite.UnitTests.ModelCreators
{
    public static class BandCreator
    {
        public static IQueryable<Band> CreateCollection()
        {
            return new List<Band>
                       {
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                       }
                .AsQueryable();
        }

        public static Band CreateSingle()
        {
            var entity = new Band
                             {
                                 Founded = DateTime.UtcNow.AddYears(-1),
                                 ModificationDate = DateTime.UtcNow.AddMonths(-1),
                             };

            entity.Name = string.Format(CultureInfo.InvariantCulture, "Name {0}", entity.Id);
            entity.InitVector = string.Format(CultureInfo.InvariantCulture, "InitVector {0}", entity.Id).PadRight(16).Substring(0, 16);
            entity.Passphrase = string.Format(CultureInfo.InvariantCulture, "PassPhrase {0}", entity.Id);
            entity.SaltValue = string.Format(CultureInfo.InvariantCulture, "SaltValue {0}", entity.Id);
            entity.Description = string.Format(CultureInfo.InvariantCulture, "Description {0}", entity.Id);

            return entity;
        }
    }
}