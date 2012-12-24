using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using Ewk.BandWebsite.Common;
using Ewk.BandWebsite.Domain;
using Ewk.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Ewk.BandWebsite.Catalogs.MongoDb
{
    public abstract class Catalog : ICatalog
    {
        static Catalog()
        {
            ClassMapper.RegisterMaps();
        }

        private MongoDatabase _database;

        protected Catalog()
        {
            var dbUrl = ConfigurationManager.AppSettings["MONGOLAB_URI"] ?? "mongodb://localhost/BandAppDb";
            var connectionString = string.Format(CultureInfo.InvariantCulture, "{0}?safe=true", dbUrl);

            _database = MongoDatabase.Create(connectionString);
        }

        #region Implementation of ICatalog

        public virtual T Add<T>(T entity) where T : Entity
        {
            entity.ModificationDate = DateTime.UtcNow;

            var collection = GetMongoCollection<T>();
            var result = collection.Insert(entity);

            return BsonSerializer.Deserialize<T>(result.Response);
        }

        public virtual IEnumerable<T> Add<T>(IEnumerable<T> entities) where T : Entity
        {
            entities = entities.ToList();

            foreach (var entity in entities)
            {
                entity.ModificationDate = DateTime.UtcNow;
            }

            var collection = GetMongoCollection<T>();
            var results = collection.InsertBatch(entities);

            return results
                .Select(result =>
                        BsonSerializer.Deserialize<T>(result.Response));
        }

        public virtual T Update<T>(T entity) where T : Entity
        {
            entity.ModificationDate = DateTime.UtcNow;

            var collection = GetMongoCollection<T>();
            var result = collection.Save(entity);

            return BsonSerializer.Deserialize<T>(result.Response);
        }

        public virtual void Remove<T>(T entity) where T : Entity
        {
            var query = Query<T>.EQ(arg => arg.Id, entity.Id);
            var collection = GetMongoCollection<T>();
            var result = collection.Remove(query, RemoveFlags.Single);
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }

            // free native resources if there are any.
            if (_database != null)
            {
                _database.Server.Disconnect();
                _database = null;
            }
        }

        #endregion

        protected virtual IQueryable<T> GetCollection<T>() where T : Entity
        {
            return GetMongoCollection<T>()
                .AsQueryable();
        }

        private MongoCollection<T> GetMongoCollection<T>() where T : Entity
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }

        protected Guid BandId
        {
            get
            {
                if (_bandId == ValueNotSetConstants.BandIdNotSet)
                {
                    var resolver = ResolveBandIdResolver();
                    _bandId = resolver.GetBandId();
                }

                return _bandId;
            }
        }
        private Guid _bandId = ValueNotSetConstants.BandIdNotSet;

        private static IBandIdResolver ResolveBandIdResolver()
        {
            return DependencyConfiguration.DependencyResolver.Resolve<IBandIdResolver>();
        }
    }
}