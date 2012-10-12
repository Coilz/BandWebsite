using System;
using Ewk.BandWebsite.Common;
using Ewk.Configuration;

namespace Ewk.BandWebsite.Domain.BandModel
{
    public class BandEntity : Entity
    {
        private Guid _bandId = ValueNotSetConstants.BandIdNotSet;

        public Guid BandId
        {
            get
            {
                if (_bandId == ValueNotSetConstants.BandIdNotSet)
                {
                    var resolver = ResolveBandIdResolver();
                    _bandId = resolver.GetBandId();
                }

                return _bandId;
            }
            set { _bandId = value; }
        }

        private static IBandIdResolver ResolveBandIdResolver()
        {
            return DependencyConfiguration.DependencyResolver.Resolve<IBandIdResolver>();
        }
    }
}