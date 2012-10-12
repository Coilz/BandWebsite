namespace Ewk.BandWebsite.Domain.BandModel
{
    /// <summary>
    /// A representation of a access token that is used in an OAuth process.
    /// </summary>
    public class OAuthAccessToken : BandEntity
    {
        /// <summary>
        /// The token of the OAuthAccessToken.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The public secret of the OAuthAccessToken.
        /// </summary>
        public string TokenSecret { get; set; }

        /// <summary>
        /// The fullname of the user that has been authenticated.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// The id of the user that has been authenticated.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The username of the user that has been authenticated.
        /// </summary>
        public string Username { get; set; }
    }
}