using System.Globalization;
using System.Linq;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.UnitTests.ModelCreators
{
    public static class AdapterSettingsCreator
    {
        public static IQueryable<AdapterSettings> CreateCollection(params string[] adapterNames)
        {
            return adapterNames
                .Select(CreateSingle)
                .ToList()
                .AsQueryable();
        }

        public static AdapterSettings CreateSingle()
        {
            var entity = CreateSingle(null);
            entity.AdapterName = string.Format(CultureInfo.InvariantCulture, "AdapterName {0}", entity.Id);

            return entity;
        }

        public static AdapterSettings CreateSingle(string adapterName)
        {
            var entity = new AdapterSettings
                {
                    AdapterName = adapterName,
                    OAuthAccessToken = OAuthAccessTokenCreator.CreateSingle(),
                    OAuthRequestToken = OAuthRequestTokenCreator.CreateSingle()
                };

            entity.SetName = string.Format(CultureInfo.InvariantCulture, "SetName {0}", entity.Id);
            
            return entity;
        }
    }
}