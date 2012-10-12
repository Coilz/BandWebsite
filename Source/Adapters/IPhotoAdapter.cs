using System.Collections.Generic;
using System.IO;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Adapters
{
    public interface IPhotoAdapter : IOAuth1
    {
        /// <summary>
        /// Get a list of items using the access token.
        /// </summary>
        /// <param name="setName">The name of the set to get photos from.</param>
        /// <param name="accessToken">The token that provides access to the resource.</param>
        /// <returns>A list of urls of photos.</returns>
        IEnumerable<string> GetItems(string setName, OAuthAccessToken accessToken);

        /// <summary>
        /// Uploads a photo.
        /// </summary>
        /// <param name="data">The actual <see cref="Stream"/> of the photo.</param>
        /// <param name="setName">The name of the set to upload photos to.</param>
        /// <param name="fileName">The filename that will be used for naming.</param>
        /// <param name="accessToken">The token that provides access to the resource.</param>
        /// <returns>An identifier of the uploaded photo.</returns>
        string UploadItem(Stream data, string setName, string fileName, OAuthAccessToken accessToken);
    }
}