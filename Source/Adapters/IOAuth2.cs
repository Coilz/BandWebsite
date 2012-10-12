using System;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Adapters
{
    public interface IOAuth2
    {
        /// <summary>
        /// Gets the url that must be used to request an acces token.
        /// </summary>
        /// <param name="callbackUri">The url to where the user will be redirected after it has authorized the application.</param>
        /// <returns>The url that must be used to to authorize the app.</returns>
        Uri GetOAuthCalculatedAuthorizationUri(Uri callbackUri);

        /// <summary>
        /// Gets an access token.
        /// </summary>
        /// <param name="currentUri">The callbackUri containing a verification code.</param>
        /// <returns>A token that provides access to the app.</returns>
        OAuthAccessToken GetOAuthAccessToken(Uri currentUri);
    }
}