using System;
using System.Collections.Generic;
using System.Linq;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Repositories
{
    public class BandRepository : IBandRepository
    {
        private readonly ICatalogsContainer _catalogsContainer;

        public BandRepository(ICatalogsContainer catalogsContainer)
        {
            _catalogsContainer = catalogsContainer;
        }

        #region Implementation of IBandRepository

        #region Musician

        public Musician AddMusician(Musician musician)
        {
            if (musician == null) throw new ArgumentNullException("musician");

            return _catalogsContainer.BandCatalog.Add(musician);
        }

        public Musician UpdateMusician(Musician musician)
        {
            if (musician == null) throw new ArgumentNullException("musician");

            return _catalogsContainer.BandCatalog.Update(musician);
        }

        public Musician GetMusician(Guid id)
        {
            var result = _catalogsContainer.BandCatalog.Musicians
                .SingleOrDefault(musician =>
                                 musician.Id == id);

            if (result == null)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public IEnumerable<Musician> GetAllMusicians()
        {
            return _catalogsContainer.BandCatalog.Musicians;
        }

        #endregion

        #region User

        public User AddUser(User user)
        {
            return _catalogsContainer.BandCatalog.Add(user);
        }

        public User UpdateUser(User user)
        {
            return _catalogsContainer.BandCatalog.Update(user);
        }

        public User GetUser(Guid id)
        {
            var result = _catalogsContainer.BandCatalog.Users
                .SingleOrDefault(user =>
                                 user.Id == id);

            if (result == null)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _catalogsContainer.BandCatalog.Users;
        }

        #endregion

        #region BlogArticle

        public BlogArticle AddBlogArticle(BlogArticle blogArticle)
        {
            if (blogArticle == null) throw new ArgumentNullException("blogArticle");

            return _catalogsContainer.BandCatalog.Add(blogArticle);
        }

        public BlogArticle UpdateBlogArticle(BlogArticle blogArticle)
        {
            if (blogArticle == null) throw new ArgumentNullException("blogArticle");

            return _catalogsContainer.BandCatalog.Update(blogArticle);
        }

        public void RemoveBlogArticle(BlogArticle blogArticle)
        {
            if (blogArticle == null) throw new ArgumentNullException("blogArticle");

            _catalogsContainer.BandCatalog.Remove(blogArticle);
        }

        public BlogArticle GetBlogArticle(Guid id)
        {
            var result = _catalogsContainer.BandCatalog.BlogArticles
                .SingleOrDefault(blogArticle =>
                                 blogArticle.Id == id);

            if (result == null)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public IEnumerable<BlogArticle> GetAllBlogArticles()
        {
            return QueryBlogArticles();
        }

        public IEnumerable<BlogArticle> GetBlogArticles(int page, int pageSize)
        {
            return QueryBlogArticles()
                .Skip(page * pageSize)
                .Take(pageSize);
        }

        IQueryable<BlogArticle> QueryBlogArticles()
        {
            return _catalogsContainer.BandCatalog.BlogArticles
                .OrderByDescending(article => article.ModificationDate);
        }

        #endregion

        #region Performance

        public Performance AddPerformance(Performance performance)
        {
            if (performance == null) throw new ArgumentNullException("performance");

            return _catalogsContainer.BandCatalog.Add(performance);
        }

        public Performance UpdatePerformance(Performance performance)
        {
            if (performance == null) throw new ArgumentNullException("performance");

            return _catalogsContainer.BandCatalog.Update(performance);
        }

        public void RemovePerformance(Performance performance)
        {
            if (performance == null) throw new ArgumentNullException("performance");

            _catalogsContainer.BandCatalog.Remove(performance);
        }

        public Performance GetPerformance(Guid id)
        {
            var result = _catalogsContainer.BandCatalog.Performances
                .SingleOrDefault(performance =>
                                 performance.Id == id);

            if (result == null)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public IEnumerable<Performance> GetAllFuturePerformances()
        {
            return QueryPerformances();
        }

        public IEnumerable<Performance> GetFuturePerformances(int page, int pageSize)
        {
            return QueryPerformances()
                .Skip(page*pageSize)
                .Take(pageSize);
        }

        public IEnumerable<Performance> GetAllPastPerformances()
        {
            return QueryPastPerformances();
        }

        public IEnumerable<Performance> GetPastPerformances(int page, int pageSize)
        {
            return QueryPastPerformances()
                .Skip(page * pageSize)
                .Take(pageSize);
        }

        IQueryable<Performance> QueryPerformances()
        {
            return _catalogsContainer.BandCatalog.Performances
                .Where(performance => performance.StartDateTime >= DateTime.UtcNow)
                .OrderBy(performance => performance.StartDateTime);
        }

        IQueryable<Performance> QueryPastPerformances()
        {
            return _catalogsContainer.BandCatalog.Performances
                .Where(performance => performance.StartDateTime < DateTime.UtcNow)
                .OrderByDescending(performance => performance.StartDateTime);
        }

        #endregion

        #region AdapterSettings

        public AdapterSettings AddAdapterSettings(AdapterSettings settings)
        {
            return _catalogsContainer.BandCatalog.Add(settings);
        }

        public AdapterSettings UpdateAdapterSettings(AdapterSettings settings)
        {
            return _catalogsContainer.BandCatalog.Update(settings);
        }

        public AdapterSettings GetAdapterSettings(string adapterName)
        {
            var result = _catalogsContainer.BandCatalog.AdapterSettings
                .FirstOrDefault(settings => settings.AdapterName == adapterName);

            if (result == null)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        #endregion

        #endregion
    }
}