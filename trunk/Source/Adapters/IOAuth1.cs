using System;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Adapters
{
    public interface IOAuth1
    {
        /// <summary>
        /// Gets a token that can be used to request an access token.
        /// </summary>
        /// <param name="callbackUri">The url to where the user will be redirected after it has authorized the application.</param>
        /// <returns>A token that can be used to request and validate an access token.</returns>
        OAuthRequestToken GetOAuthRequestToken(Uri callbackUri);

        /// <summary>
        /// Gets the url that must be used to request an acces token.
        /// </summary>
        /// <param name="requestToken">The request token that is used for the calculation.</param>
        /// <returns>The url that must be used to to authorize the app.</returns>
        Uri GetOAuthCalculatedAuthorizationUri(OAuthRequestToken requestToken);

        /// <summary>
        /// Gets an access token.
        /// </summary>
        /// <param name="requestToken">The request token that was used to authorize.</param>
        /// <param name="verifier">The verifier that verifies the request token.</param>
        /// <returns>A token that provides access to the app.</returns>
        OAuthAccessToken GetOAuthAccessToken(OAuthRequestToken requestToken, string verifier);
    }
} 