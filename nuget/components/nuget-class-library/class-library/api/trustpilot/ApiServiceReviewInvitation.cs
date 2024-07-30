using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace nuget_class_library.class_library.api.trustpilot
{
    /// <summary>
    /// Class for modelling data to be sent over in the HTTP request body to Trustpilot.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ApiServiceReviewInvitation
    {
        /// <summary>
        /// Gets the TemplateId; the Id of the template.
        /// </summary>
        [JsonProperty("templateId")] 
        public string TemplateId { get; private set; }

        /// <summary>
        /// Gets the PreferredSendTime; the preferred time to send the email.
        /// </summary>
        [JsonProperty("preferredSendTime")] 
        public string PreferredSendTime { get; private set; }

        /// <summary>
        /// Gets the RedirectUri; the Uri to redirect to once the review has been submitted.
        /// </summary>
        [JsonProperty("redirectUri")] 
        public string RedirectUri { get; private set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="ApiServiceReviewInvitation"/> class.
        /// </summary>
        /// <param name="templateId">The template id to set.</param>
        /// <param name="preferredSendTime">The preferred send time to set.</param>
        /// <param name="redirectUri">The redirect uri to set.</param>
        public ApiServiceReviewInvitation(
            string templateId,
            string preferredSendTime,
            string redirectUri)
        {
            TemplateId = templateId;
            PreferredSendTime = preferredSendTime;
            RedirectUri = redirectUri;
        }
    }
}
