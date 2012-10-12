using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Adapters.Flickr
{
    static class FlickrExtensions
    {
        public static void Authorize(this FlickrNet.Flickr flickr, OAuthAccessToken accessToken)
        {
            if (!IsComplete(accessToken)) throw new AuthorizationException();

            flickr.OAuthAccessToken = accessToken.Token;
            flickr.OAuthAccessTokenSecret = accessToken.TokenSecret;
        }

        private static bool IsComplete(OAuthAccessToken accessToken)
        {
            return !(string.IsNullOrEmpty(accessToken.Token) ||
                    string.IsNullOrEmpty(accessToken.TokenSecret) ||
                    string.IsNullOrEmpty(accessToken.UserId));
        }
    }
}