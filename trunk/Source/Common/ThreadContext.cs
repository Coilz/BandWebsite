using System;

namespace Ewk.BandWebsite.Common
{
    public static class ThreadContext
    {
        [ThreadStatic]
        public static Guid BandId;
    }
}
