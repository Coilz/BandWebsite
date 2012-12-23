using System;
using Ewk.BandWebsite.Common;

namespace Ewk.BandWebsite.Web.UI
{
    /// <summary>
    /// Provides access to the ThreadContext.
    /// </summary>
    public class ThreadContextAccessor : IBandIdResolver, IBandIdInstaller
    {
        #region Implementation of IBandIdResolver

        public Guid GetBandId()
        {
            return ThreadContext.BandId;
        }

        #endregion

        #region Implementation of IBandIdInstaller

        public void SetBandId(Guid id)
        {
            ThreadContext.BandId = id;
        }

        #endregion
    }
}