using Ewk.BandWebsite.Domain.AppModel;

namespace Ewk.BandWebsite.Process
{
    /// <summary>
    /// Provides methods to process bands.
    /// </summary>
    public interface IBandProcess
    {
        /// <summary>
        /// Makes sure there is a <see cref="Band"/> in the store. When it's not there, it creates one.
        /// </summary>
        /// <returns>The current <see cref="Band"/>.</returns>
        Band EnsureBandExists();

        /// <summary>
        /// Gets the current <see cref="Band"/> from the collection.
        /// </summary>
        /// <returns>The current <see cref="Band"/>.</returns>
        Band GetBand();

        /// <summary>
        /// Updates the specified <see cref="Band"/> in the collection.
        /// </summary>
        /// <param name="band">The <see cref="Band"/> to be updated with its new values.</param>
        /// <returns>The updated <see cref="Band"/>.</returns>
        Band UpdateBand(Band band);
    }
}