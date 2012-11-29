using System;
using System.Collections.Generic;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Process
{
    /// <summary>
    /// Provides methods to process performances.
    /// </summary>
    public interface IPerformanceProcess
    {
        /// <summary>
        /// Gets all upcoming <see cref="Performance"/> instances.
        /// </summary>
        /// <returns>A list of <see cref="Performance"/> instances.</returns>
        IEnumerable<Performance> GetPerformances();

        /// <summary>
        /// Gets some upcoming <see cref="Performance"/> instances.
        /// </summary>
        /// <param name="page">The page number (zero based).</param>
        /// <param name="pageSize">The size of a page.</param>
        /// <returns>A list of <see cref="Performance"/> instances.</returns>
        IEnumerable<Performance> GetPerformances(int page, int pageSize);

        /// <summary>
        /// Gets all past <see cref="Performance"/> instances.
        /// </summary>
        /// <returns>A list of <see cref="Performance"/> instances.</returns>
        IEnumerable<Performance> GetPastPerformances();

        /// <summary>
        /// Gets some past <see cref="Performance"/> instances.
        /// </summary>
        /// <param name="page">The page number (zero based).</param>
        /// <param name="pageSize">The size of a page.</param>
        /// <returns>A list of <see cref="Performance"/> instances.</returns>
        IEnumerable<Performance> GetPastPerformances(int page, int pageSize);

        /// <summary>
        /// Gets a <see cref="Performance"/>.
        /// </summary>
        /// <param name="id">A value that uniquely identifies a <see cref="Performance"/>.</param>
        /// <returns>The requested <see cref="Performance"/></returns>
        Performance GetPerformance(Guid id);

        /// <summary>
        /// Adds the specified <see cref="Performance"/> to the store.
        /// </summary>
        /// <param name="performance">The <see cref="Performance"/> to store.</param>
        /// <returns>The persisted <see cref="Performance"/></returns>
        Performance AddPerformance(Performance performance);

        /// <summary>
        /// Updates the specified <see cref="Performance"/> in the store.
        /// </summary>
        /// <param name="performance">The <see cref="Performance"/> to update in the store.</param>
        /// <returns>The persisted <see cref="Performance"/></returns>
        Performance UpdatePerformance(Performance performance);

        /// <summary>
        /// Removes the specified <see cref="Performance"/> from the store.
        /// </summary>
        /// <param name="performance">The <see cref="Performance"/> to update in the store.</param>
        /// <returns>The persisted <see cref="Performance"/></returns>
        void RemovePerformance(Performance performance);
    }
}