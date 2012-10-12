using System;
using System.Threading.Tasks;
using Ewk.BandWebsite.Domain.AppModel;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Process
{
    public interface IUserProcess
    {
        /// <summary>
        /// Gets a <see cref="User"/> from the collection.
        /// </summary>
        /// <param name="id">A value that uniquely identifies a <see cref="User"/>.</param>
        /// <returns>The requested <see cref="User"/>.</returns>
        User GetUser(Guid id);

        /// <summary>
        /// Gets a <see cref="User"/> from the collection.
        /// </summary>
        /// <param name="loginName">A value that uniquely identifies a <see cref="User"/>.</param>
        /// <returns>The requested <see cref="User"/>.</returns>
        User GetUserByLoginName(string loginName);

        /// <summary>
        /// Gets a <see cref="User"/> from the collection.
        /// </summary>
        /// <param name="emailAddress">A value that uniquely identifies a <see cref="LoginAccount"/>.</param>
        /// <returns>The requested <see cref="User"/>.</returns>
        User GetUserByEmailAddress(string emailAddress);

        /// <summary>
        /// Gets a <see cref="User"/> from the collection.
        /// </summary>
        /// <param name="id">A value that uniquely identifies a <see cref="LoginAccount"/>.</param>
        /// <returns>The requested <see cref="User"/>.</returns>
        User GetUserByLoginAccount(Guid id);

        /// <summary>
        /// Adds a <see cref="User"/> to the collection.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to add.</param>
        /// <returns>The added <see cref="User"/>.</returns>
        User AddUser(User user);

        /// <summary>
        /// Updates the specified <see cref="User"/> in the collection.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to be updated with its new values.</param>
        /// <returns>The updated <see cref="User"/>.</returns>
        User UpdateUser(User user);
    }
}