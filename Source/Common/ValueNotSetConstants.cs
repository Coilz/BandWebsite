using System;

namespace Ewk.BandWebsite.Common
{
    public static class ValueNotSetConstants
    {
        public static Guid BandIdNotSet
        {
            get { return Guid.Empty; }
        }

        public const string RequiredStringNotSet = "...";
    }
}