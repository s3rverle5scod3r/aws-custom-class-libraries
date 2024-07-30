using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace nuget_class_library.class_library.api.trustpilot
{
    /// <summary>
    /// Holds response data from an API access request and its details.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class TrustpilotAccessResponse
    {
        /// <summary>
        /// Gets the AccessToken.
        /// </summary>
        [JsonProperty("access_token")] 
        public string AccessToken { get; private set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="TrustpilotAccessResponse"/> class.
        /// </summary>
        /// <param name="accessToken">The access token to set.</param>
        public TrustpilotAccessResponse(
            string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
