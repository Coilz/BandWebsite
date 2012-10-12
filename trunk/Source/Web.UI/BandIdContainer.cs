using System;
using System.Web;
using Ewk.BandWebsite.Common;

namespace Ewk.BandWebsite.Web.UI
{
    public class BandIdContainer : IBandIdResolver, IBandIdInstaller
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
