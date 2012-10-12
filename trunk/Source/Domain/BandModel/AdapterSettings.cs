namespace Ewk.BandWebsite.Domain.BandModel
{
    /// <summary>
    /// A representation of a photo that was uploaded to an external photo hosting service.
    /// </summary>
    public class AdapterSettings : BandEntity
    {
        /// <summary>
        /// The name of the adapter.
        /// </summary>
        public string AdapterName { get; set; }

        /// <summary>
        /// The name of the photo set known to the photo api from which photos are retrieved.
        /// </summary>
        public string SetName { get; set; }

        /// <summary>
        /// The authorization token that is used to request an access token for the photo api.
        /// </summary>
        public OAuthRequestToken OAuthRequestToken { get; set; }

        /// <summary>
        /// The access token for the photo api.
        /// </summary>
        public OAuthAccessToken OAuthAccessToken { get; set; }
    }
}