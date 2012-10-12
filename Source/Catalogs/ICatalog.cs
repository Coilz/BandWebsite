using System;
using System.Collections.Generic;
using Ewk.BandWebsite.Domain;

namespace Ewk.BandWebsite.Catalogs
{
    public interface ICatalog : IDisposable
    {
        /// <summary>
        /// Adds the entity to the store.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Entity"/> to add to the store.</typeparam>
        /// <param name="entity">The <see cref="Entity"/> to add to the store.</param>
        /// <returns>The added <see cref="Entity"/>.</returns>
        T Add<T>(T entity) where T : Entity;

        /// <summary>
        /// Adds the entity to the store.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Entity"/> to add to the store.</typeparam>
        /// <param name="entities">The <see cref="Entity"/> to add to the store.</param>
        /// <returns>The added <see cref="Entity"/>.</returns>
        IEnumerable<T> Add<T>(IEnumerable<T> entities) where T : Entity;

        /// <summary>
        /// Updates the entity in the store.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Entity"/> to update.</typeparam>
        /// <param name="entity">The <see cref="Entity"/> to update.</param>
        /// <returns>The updated <see cref="Entity"/>.</returns>
        T Update<T>(T entity) where T : Entity;

        /// <summary>
        /// Removes the entity from the store.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Entity"/> to remove.</typeparam>
        /// <param name="entity">The <see cref="Entity"/> to remove.</param>
        void Remove<T>(T entity) where T : Entity;
    }
}