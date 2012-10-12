using System;
using Ewk.BandWebsite.Common;

namespace Ewk.BandWebsite.UnitTests
{
    public class BandIdContainer : IBandIdResolver, IBandIdInstaller
    {
        private Guid _bandId = ValueNotSetConstants.BandIdNotSet;

        #region Constructors

        public BandIdContainer()
        {
        }

        public BandIdContainer(Guid id)
        {
            _bandId = id;
        }

        #endregion

        #region Implementation of IBandIdResolver

        public Guid GetBandId()
        {
            return _bandId;
        }

        #endregion

        #region Implementation of IBandIdInstaller

        public void SetBandId(Guid id)
        {
            _bandId = id;
        }

        #endregion
    }
}