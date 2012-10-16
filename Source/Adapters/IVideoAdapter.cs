using System.Collections.Generic;
using System.IO;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Domain.Dto;

namespace Ewk.BandWebsite.Adapters
{
    public interface IVideoAdapter : IOAuth2
    {
        /// <summary>
        /// Get a list of items using the access token.
        /// </summary>
        /// <param name="setName">The name of the set to get audio from.</param>
        /// <param name="accessToken">The token that provides access to the resource.</param>
        /// <returns>A list of urls of audio.</returns>
        IEnumerable<Video> GetItems(string setName, OAuthAccessToken accessToken);

        /// <summary>
        /// Get a list of items using the access token.
        /// </summary>
        /// <param name="setName">The name of the set to get audio from.</param>
        /// <param name="accessToken">The token that provides access to the resource.</param>
        /// <param name="page">The page number (zero based).</param>
        /// <param name="pageSize">The size of a page.</param>
        /// <returns>A list of urls of audio.</returns>
        IEnumerable<Video> GetItems(string setName, OAuthAccessToken accessToken, int page, int pageSize);

        /// <summary>
        /// Gets an <see cref="AudioTrack"/>.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="AudioTrack"/>.</param>
        /// <param name="accessToken">The token that provides access to the resource.</param>
        /// <returns>An <see cref="AudioTrack"/>.</returns>
        Video GetItem(int id, OAuthAccessToken accessToken);

        /// <summary>
        /// Uploads an audiotrack.
        /// </summary>
        /// <param name="data">The actual <see cref="Stream"/> of the audio.</param>
        /// <param name="setName">The name of the set to upload audio to.</param>
        /// <param name="title">The title that will be used for naming.</param>
        /// <param name="accessToken">The token that provides access to the resource.</param>
        /// <returns>An identifier of the uploaded audio.</returns>
        string UploadItem(Stream data, string setName, string title, OAuthAccessToken accessToken);
    }
}
