using Newtonsoft.Json;

namespace nuget_class_library.class_library.api.trustpilot
{
    public class TrustpilotAuthToken
    {
        /// <summary>
        /// Gets the IssuedAt; The epoch time the token was issued at in milliseconds.
        /// </summary>
        [JsonProperty("issued_at")]
        public string IssuedAt { get; private set; }
        
        /// <summary>
        /// Gets the AccessToken.
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; private set; }

        /// <summary>
        /// Gets the ExpiresIn; the amount of time after the 'issued at' datetime which the token expires in seconds.
        /// </summary>
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; private set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="TrustpilotAuthToken"/> class.
        /// </summary>
        /// <param name="issuedAt">The issued at to set.</param>
        /// <param name="accessToken">The access token to set.</param>
        /// <param name="expiresIn">The expires in to set.</param>
        public TrustpilotAuthToken(
            string issuedAt,
            string accessToken,
            string expiresIn)
        {
            IssuedAt = issuedAt;
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
        }

    }
}
