namespace Ewk.BandWebsite.Domain.BandModel
{
    /// <summary>
    /// A representation of a request token that is used in an OAuth process.
    /// </summary>
    public class OAuthRequestToken : BandEntity
    {
        /// <summary>
        /// The token of the OAuthRequestToken.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The public secret of the OAuthRequestToken.
        /// </summary>
        public string TokenSecret { get; set; }
    }
}