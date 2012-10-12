using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.UnitTests.ModelCreators
{
    public static class OAuthAccessTokenCreator
    {
        public static IQueryable<OAuthAccessToken> CreateCollection()
        {
            return new List<OAuthAccessToken>
                       {
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                       }
                .AsQueryable();
        }

        public static OAuthAccessToken CreateSingle()
        {
            var entity = new OAuthAccessToken();

            entity.FullName = string.Format(CultureInfo.InvariantCulture, "FullName {0}", entity.Id);
            entity.Token = string.Format(CultureInfo.InvariantCulture, "Token {0}", entity.Id);
            entity.TokenSecret = string.Format(CultureInfo.InvariantCulture, "TokenSecret {0}", entity.Id);
            entity.UserId = string.Format(CultureInfo.InvariantCulture, "UserId {0}", entity.Id);
            entity.Username = string.Format(CultureInfo.InvariantCulture, "Username {0}", entity.Id);

            return entity;
        }
    }
}