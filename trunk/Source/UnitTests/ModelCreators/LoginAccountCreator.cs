using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Ewk.BandWebsite.Domain.AppModel;

namespace Ewk.BandWebsite.UnitTests.ModelCreators
{
    public static class LoginAccountCreator
    {
        public static IQueryable<LoginAccount> CreateCollection()
        {
            return new List<LoginAccount>
                       {
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                       }
                .AsQueryable();
        }

        public static LoginAccount CreateSingle()
        {
            var entity = new LoginAccount
                            {
                                ModificationDate = DateTime.UtcNow.AddMonths(-1),
                                IsApproved = true,
                            };

            entity.EmailAddress = string.Format(CultureInfo.InvariantCulture, "EmailAddress@{0}.com", entity.Id);
            entity.LoginName = string.Format(CultureInfo.InvariantCulture, "LoginName {0}", entity.Id);
            entity.Password = string.Format(CultureInfo.InvariantCulture, "Password {0}", entity.Id);

            return entity;
        }
    }
}