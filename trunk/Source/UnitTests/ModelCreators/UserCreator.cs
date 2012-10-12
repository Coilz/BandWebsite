using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.UnitTests.ModelCreators
{
    public static class UserCreator
    {
        public static IQueryable<User> CreateCollection()
        {
            return new List<User>
                       {
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                       }
                .AsQueryable();
        }

        public static User CreateSingle()
        {
            var entity = new User
                           {
                               ModificationDate = DateTime.UtcNow.AddMonths(-1),
                               Login = LoginAccountCreator.CreateSingle()
                           };

            entity.Name = string.Format(CultureInfo.InvariantCulture, "Name {0}", entity.Id);

            return entity;
        }
    }
}