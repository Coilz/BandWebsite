using System;
using System.Collections.Generic;
using System.IO;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Process
{
    /// <summary>
    /// Provides methods to process photos.
    /// </summary>
    public interface IPhotoProcess
    {
        /// <summary>
        /// Gets the url where authentication can be done.
        /// </summary>
        /// <param name="callbackUri">The uri for the callback after the user authorized the application.</param>
        /// <returns>The url where authentication can be done.</returns>
        Uri GetAuthenticationUri(Uri callbackUri);

        /// <summary>
        /// Authorizes the application to interact with the photo service.
        /// </summary>
        /// <param name="verificationUri">The url that contains the verification of the authorization.</param>
        void Authorize(Uri verificationUri);

        /// <summary>
        /// Gets the <see cref="Uri"/> instances of all stored photos.
        /// </summary>
        /// <returns>A list of <see cref="Uri"/> instances that point to stored photos.</returns>
        IEnumerable<Uri> GetPhotos();

        /// <summary>
        /// Adds a photo to the store.
        /// </summary>
        /// <param name="photo">The stream that represents the photo to store.</param>
        /// <param name="fileName">The file name of the photo to store.</param>
        /// <returns>The id of the uploaded photo.</returns>
        string AddPhoto(Stream photo, string fileName);

        /// <summary>
        /// Gets the <see cref="AdapterSettings"/>.
        /// </summary>
        /// <returns>The requested <see cref="AdapterSettings"/>.</returns>
        AdapterSettings GetAdapterSettings();

        /// <summary>
        /// Updates the <see cref="AdapterSettings"/> in the store with the specified <see cref="AdapterSettings"/>.
        /// </summary>
        /// <param name="settings">The <see cref="AdapterSettings"/> to update in the store.</param>
        /// <returns>The persisted <see cref="AdapterSettings"/>.</returns>
        AdapterSettings UpdateAdapterSettings(AdapterSettings settings);
    }
}