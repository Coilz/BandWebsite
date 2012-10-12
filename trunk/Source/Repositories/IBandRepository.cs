using System;
using System.Collections.Generic;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Repositories
{
    /// <summary>
    /// Provides access to entities that have a correlation to a band.
    /// </summary>
    public interface IBandRepository
    {
        #region Musician

        /// <summary>
        /// Adds a <see cref="Musician"/> to the catalog.
        /// </summary>
        /// <param name="musician">The <see cref="Musician"/> to add.</param>
        /// <returns>The added <see cref="Musician"/>.</returns>
        Musician AddMusician(Musician musician);

        /// <summary>
        /// Updates the given <see cref="Musician"/> to the catalog.
        /// </summary>
        /// <param name="musician">The <see cref="Musician"/> to update.</param>
        /// <returns>The updated <see cref="Musician"/>.</returns>
        Musician UpdateMusician(Musician musician);

        /// <summary>
        /// Gets a <see cref="Musician"/> from the catalog.
        /// </summary>
        /// <param name="id">A value that uniquely identifies a <see cref="Musician"/> instance.</param>
        /// <returns>The requested <see cref="Musician"/>.</returns>
        Musician GetMusician(Guid id);

        /// <summary>
        /// Gets a list of all <see cref="Musician"/> instances that are contained in the catalog.
        /// </summary>
        /// <returns>A list of all <see cref="Musician"/> instances.</returns>
        IEnumerable<Musician> GetAllMusicians();

        #endregion

        #region User

        /// <summary>
        /// Adds a <see cref="User"/> to the catalog.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to add.</param>
        /// <returns>The added <see cref="User"/>.</returns>
        User AddUser(User user);

        /// <summary>
        /// Updates the given <see cref="User"/> to the catalog.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to update.</param>
        /// <returns>The updated <see cref="User"/>.</returns>
        User UpdateUser(User user);

        /// <summary>
        /// Gets a <see cref="User"/> from the catalog.
        /// </summary>
        /// <param name="id">A value that uniquely identifies a <see cref="User"/> instance.</param>
        /// <returns>The requested <see cref="User"/>.</returns>
        User GetUser(Guid id);

        /// <summary>
        /// Gets a list of all <see cref="User"/> instances that are contained in the catalog.
        /// </summary>
        /// <returns>A list of all <see cref="User"/> instances.</returns>
        IEnumerable<User> GetAllUsers();

        #endregion

        #region BlogArticle

        /// <summary>
        /// Adds a <see cref="BlogArticle"/> to the catalog.
        /// </summary>
        /// <param name="blogArticle">The <see cref="BlogArticle"/> to add.</param>
        /// <returns>The added <see cref="BlogArticle"/>.</returns>
        BlogArticle AddBlogArticle(BlogArticle blogArticle);

        /// <summary>
        /// Updates a <see cref="BlogArticle"/> in the catalog.
        /// </summary>
        /// <param name="blogArticle">The <see cref="BlogArticle"/> to update.</param>
        /// <returns>The updated <see cref="BlogArticle"/>.</returns>
        BlogArticle UpdateBlogArticle(BlogArticle blogArticle);

        /// <summary>
        /// Removes a <see cref="BlogArticle"/> from the catalog.
        /// </summary>
        /// <param name="blogArticle">The <see cref="BlogArticle"/> to remove.</param>
        void RemoveBlogArticle(BlogArticle blogArticle);

        /// <summary>
        /// Gets a <see cref="BlogArticle"/> from the catalog.
        /// </summary>
        /// <param name="id">A value that uniquely identifies a <see cref="BlogArticle"/> instance.</param>
        /// <returns>The requested <see cref="BlogArticle"/>.</returns>
        BlogArticle GetBlogArticle(Guid id);

        /// <summary>
        /// Gets a list of all <see cref="BlogArticle"/> instances that are contained in the catalog.
        /// </summary>
        /// <returns>A list of all <see cref="BlogArticle"/> instances.</returns>
        IEnumerable<BlogArticle> GetAllBlogArticles();

        /// <summary>
        /// Gets a list of all <see cref="BlogArticle"/> instances that are contained in the catalog.
        /// </summary>
        /// <param name="page">The page number (zero based).</param>
        /// <param name="pageSize">The size of a page.</param>
        /// <returns>A list of all <see cref="BlogArticle"/> instances.</returns>
        IEnumerable<BlogArticle> GetBlogArticles(int page, int pageSize);

        #endregion

        #region Performance

        /// <summary>
        /// Adds a <see cref="Performance"/> to the catalog.
        /// </summary>
        /// <param name="performance">The <see cref="Performance"/> to add.</param>
        /// <returns>The added <see cref="Performance"/>.</returns>
        Performance AddPerformance(Performance performance);

        /// <summary>
        /// Updates a <see cref="Performance"/> in the catalog.
        /// </summary>
        /// <param name="performance">The <see cref="Performance"/> to update.</param>
        /// <returns>The updated <see cref="Performance"/>.</returns>
        Performance UpdatePerformance(Performance performance);

        /// <summary>
        /// Removes a <see cref="Performance"/> from the catalog.
        /// </summary>
        /// <param name="performance">The <see cref="Performance"/> to remove.</param>
        void RemovePerformance(Performance performance);

        /// <summary>
        /// Gets a <see cref="Performance"/> from the catalog.
        /// </summary>
        /// <param name="id">A value that uniquely identifies a <see cref="Performance"/> instance.</param>
        /// <returns>The requested <see cref="Performance"/>.</returns>
        Performance GetPerformance(Guid id);

        /// <summary>
        /// Gets a list of all upcoming <see cref="Performance"/> instances that are contained in the catalog.
        /// </summary>
        /// <returns>A list of all <see cref="Performance"/> instances.</returns>
        IEnumerable<Performance> GetAllPerformances();

        /// <summary>
        /// Gets a list of some upcoming <see cref="Performance"/> instances that are contained in the catalog.
        /// </summary>
        /// <param name="page">The page number (zero based).</param>
        /// <param name="pageSize">The size of a page.</param>
        /// <returns>A list of some <see cref="Performance"/> instances.</returns>
        IEnumerable<Performance> GetPerformances(int page, int pageSize);

        /// <summary>
        /// Gets a list of all past <see cref="Performance"/> instances that are contained in the catalog.
        /// </summary>
        /// <returns>A list of some <see cref="Performance"/> instances.</returns>
        IEnumerable<Performance> GetAllPastPerformances();

        /// <summary>
        /// Gets a list of some past <see cref="Performance"/> instances that are contained in the catalog.
        /// </summary>
        /// <param name="page">The page number (zero based).</param>
        /// <param name="pageSize">The size of a page.</param>
        /// <returns>A list of some <see cref="Performance"/> instances.</returns>
        IEnumerable<Performance> GetPastPerformances(int page, int pageSize);

        #endregion

        #region AdapterSettings

        /// <summary>
        /// Adds a <see cref="AdapterSettings"/> to the catalog.
        /// </summary>
        /// <param name="photo">The <see cref="AdapterSettings"/> to add.</param>
        /// <returns>The added <see cref="AdapterSettings"/>.</returns>
        AdapterSettings AddAdapterSettings(AdapterSettings photo);

        /// <summary>
        /// Updates a <see cref="AdapterSettings"/> in the catalog.
        /// </summary>
        /// <param name="settings">The <see cref="AdapterSettings"/> to update.</param>
        /// <returns>The updated <see cref="AdapterSettings"/>.</returns>
        AdapterSettings UpdateAdapterSettings(AdapterSettings settings);

        /// <summary>
        /// Gets a <see cref="AdapterSettings"/> from the catalog.
        /// </summary>
        /// <param name="adapterName">The name of the adapter to get settings for.</param>
        /// <returns>The requested <see cref="AdapterSettings"/>.</returns>
        AdapterSettings GetAdapterSettings(string adapterName);

        #endregion
    }
}