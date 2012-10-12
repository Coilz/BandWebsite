using System;
using System.Collections.Generic;
using Ewk.BandWebsite.Domain.AppModel;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Repositories
{
    /// <summary>
    /// Provides access to entities that have a correlation to the app.
    /// </summary>
    public interface IAppRepository
    {
        #region Band

        /// <summary>
        /// Adds a <see cref="Band"/> to the catalog.
        /// </summary>
        /// <param name="band">The <see cref="Band"/> to add.</param>
        /// <returns>The added <see cref="Band"/>.</returns>
        Band AddBand(Band band);

        /// <summary>
        /// Updates a <see cref="Band"/> in the catalog.
        /// </summary>
        /// <param name="band">The <see cref="Band"/> to update.</param>
        /// <returns>The updated <see cref="Band"/>.</returns>
        Band UpdateBand(Band band);

        /// <summary>
        /// Gets the current <see cref="Band"/> from the catalog.
        /// </summary>
        /// <returns>The requested <see cref="Band"/>.</returns>
        Band GetBand();

        /// <summary>
        /// Gets a <see cref="Band"/> from the catalog.
        /// </summary>
        /// <param name="id">A value that uniquely identifies a <see cref="Band"/> instance.</param>
        /// <returns>The requested <see cref="Band"/>.</returns>
        Band GetBand(Guid id);

        /// <summary>
        /// Gets a list of all <see cref="Band"/> instances that are contained in the catalog.
        /// </summary>
        /// <returns>A list of all <see cref="Band"/> instances.</returns>
        IEnumerable<Band> GetAllBands();

        #endregion

        #region LoginAccount

        /// <summary>
        /// Gets a <see cref="LoginAccount"/> from the catalog.
        /// </summary>
        /// <param name="loginName">The value to identify the <see cref="LoginAccount"/> with.</param>
        /// <returns>The requested <see cref="LoginAccount"/>.</returns>
        LoginAccount GetLoginAccountByLoginName(string loginName);

        #endregion

        #region User

        /// <summary>
        /// Gets a list of all <see cref="User"/> instances with the specified <paramref name="loginName"/> that are contained in the catalog.
        /// </summary>
        /// <param name="loginName">The loginName that is used as filter.</param>
        /// <returns>A list of all <see cref="User"/> instances.</returns>
        IEnumerable<User> GetUsersByLoginName(string loginName);

        /// <summary>
        /// Gets a list of all <see cref="User"/> instances with the specified <paramref name="emailAddress"/> that are contained in the catalog.
        /// </summary>
        /// <param name="emailAddress">The emailAddress that is used as filter.</param>
        /// <returns>A list of all <see cref="User"/> instances.</returns>
        IEnumerable<User> GetUsersByEmailAddress(string emailAddress);

        /// <summary>
        /// Gets a list of all <see cref="User"/> instances with the specified <paramref name="id"/> for
        /// <see cref="LoginAccount"/> that are contained in the catalog.
        /// </summary>
        /// <param name="id">A value that uniquely identifies a <see cref="LoginAccount"/>.</param>
        /// <returns>A list of all <see cref="User"/> instances.</returns>
        IEnumerable<User> GetUsersByLoginAccount(Guid id);

        /// <summary>
        /// Gets a list of all <see cref="User"/> instances that are contained in the catalog.
        /// </summary>
        /// <returns>A list of all <see cref="User"/> instances.</returns>
        IEnumerable<User> GetAllUsers();

        #endregion
    }
}