using System;

namespace Ewk.BandWebsite.Common
{
    /// <summary>
    /// Contains context that is unique for each thread.
    /// </summary>
    public static class ThreadContext
    {
        /// <summary>
        /// A value that identifies a band.
        /// </summary>
        [ThreadStatic]
        public static Guid BandId;
    }
}
