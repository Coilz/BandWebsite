using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.UnitTests.ModelCreators
{
    public static class OAuthRequestTokenCreator
    {
        public static IQueryable<OAuthRequestToken> CreateCollection()
        {
            return new List<OAuthRequestToken>
                       {
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                       }
                .AsQueryable();
        }

        public static OAuthRequestToken CreateSingle()
        {
            var entity = new OAuthRequestToken();

            entity.Token = string.Format(CultureInfo.InvariantCulture, "Token {0}", entity.Id);
            entity.TokenSecret = string.Format(CultureInfo.InvariantCulture, "TokenSecret {0}", entity.Id);

            return entity;
        }
    }
}