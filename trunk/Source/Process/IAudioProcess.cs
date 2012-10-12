using System;
using System.Collections.Generic;
using System.IO;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Domain.Dto;

namespace Ewk.BandWebsite.Process
{
    /// <summary>
    /// Provides methods to process audio.
    /// </summary>
    public interface IAudioProcess
    {
        /// <summary>
        /// Gets the url where authentication can be done.
        /// </summary>
        /// <param name="callbackUri">The uri for the callback after the user authorized the application.</param>
        /// <returns>The url where authentication can be done.</returns>
        Uri GetAuthenticationUri(Uri callbackUri);

        /// <summary>
        /// Authorizes the application to interact with the audio service.
        /// </summary>
        /// <param name="verificationUri">The url that contains the verification of the authorization.</param>
        void Authorize(Uri verificationUri);

        /// <summary>
        /// Gets the instances of all stored audio.
        /// </summary>
        /// <returns>A list of <see cref="Uri"/> instances that point to stored audio.</returns>
        IEnumerable<AudioTrack> GetAudioTracks();

        /// <summary>
        /// Gets some of the the instances of all stored audio.
        /// </summary>
        /// <param name="page">The page number (zero based).</param>
        /// <param name="pageSize">The size of a page.</param>
        /// <returns>A list of <see cref="Uri"/> instances that point to stored audio.</returns>
        IEnumerable<AudioTrack> GetAudioTracks(int page, int pageSize);

        /// <summary>
        /// Adds a audio to the store.
        /// </summary>
        /// <param name="audio">The stream that represents the audio to store.</param>
        /// <param name="fileName">The file name of the audio to store.</param>
        /// <returns>The id of the uploaded audio.</returns>
        string AddAudio(Stream audio, string fileName);

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

        /// <summary>
        /// Gets an <see cref="AudioTrack"/>.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="AudioTrack"/>.</param>
        /// <returns>An <see cref="AudioTrack"/>.</returns>
        AudioTrack GetAudioTrack(int id);
    }
}