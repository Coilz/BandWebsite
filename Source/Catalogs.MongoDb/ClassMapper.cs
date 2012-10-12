using System;
using Ewk.BandWebsite.Domain;
using Ewk.BandWebsite.Domain.AppModel;
using Ewk.BandWebsite.Domain.BandModel;
using MongoDB.Bson.Serialization;

namespace Ewk.BandWebsite.Catalogs.MongoDb
{
    class ClassMapper
    {
        public static void RegisterMaps()
        {
            RegisterMap<Band>(map =>
                              map.GetMemberMap(band =>
                                               band.Passphrase)
                                  .SetElementName("PassPhrase"));
            RegisterMap<LoginAccount>();

            RegisterMap<BlogArticle>();
            RegisterMap<Performance>(map =>
                                     map.GetMemberMap(c =>
                                                      c.VenueUri)
                                         .SetDefaultValue(null));
            RegisterMap<AdapterSettings>();
            RegisterMap<User>();

            // OAuthAccessToken, Passphrase
        }

        static void RegisterMap<T>() where T : Entity
        {
            RegisterMap<T>(null);
        }

        static void RegisterMap<T>(Action<BsonClassMap<T>> action) where T : Entity
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(T))) return;

            BsonClassMap.RegisterClassMap<T>(
                classMap =>
                {
                    classMap.AutoMap();
                    classMap.SetIgnoreExtraElements(true);

                    if (action == null) return;

                    action(classMap);
                });
        }
    }
}