using System;
using System.Collections.Generic;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Process
{
    /// <summary>
    /// Provides methods to process blog performances.
    /// </summary>
    public class PerformanceProcess : ProcessBase, IPerformanceProcess
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="catalogsContainer">A container with all catalogs to access the store.</param>
        public PerformanceProcess(ICatalogsContainer catalogsContainer)
            :base(catalogsContainer)
        {
        }

        #region Implementation of IPerformanceProcess

        public IEnumerable<Performance> GetPerformances()
        {
            return BandRepository.GetAllPerformances();
        }

        public IEnumerable<Performance> GetPerformances(int page, int pageSize)
        {
            return BandRepository.GetPerformances(page, pageSize);
        }

        public IEnumerable<Performance> GetPastPerformances()
        {
            return BandRepository.GetAllPastPerformances();
        }

        public IEnumerable<Performance> GetPastPerformances(int page, int pageSize)
        {
            return BandRepository.GetPastPerformances(page, pageSize);
        }

        public Performance GetPerformance(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException("id");

            return BandRepository.GetPerformance(id);
        }

        public Performance AddPerformance(Performance performance)
        {
            if (performance == null) throw new ArgumentNullException("performance");

            return BandRepository.AddPerformance(performance);
        }

        public Performance UpdatePerformance(Performance performance)
        {
            if (performance == null) throw new ArgumentNullException("performance");

            return BandRepository.UpdatePerformance(performance);
        }

        public void RemovePerformance(Performance performance)
        {
            if (performance == null) throw new ArgumentNullException("performance");

            BandRepository.RemovePerformance(performance);
        }

        #endregion
    }
}