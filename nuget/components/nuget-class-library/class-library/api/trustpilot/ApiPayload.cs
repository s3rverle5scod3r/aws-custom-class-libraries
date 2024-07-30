using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace nuget_class_library.class_library.api.trustpilot
{
    /// <summary>
    /// Class for modelling data to be sent over in the HTTP request body to Trustpilot.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ApiPayload
    {
        /// <summary>
        /// Gets the ReplyTo; the reply to email address.
        /// </summary>
        [JsonProperty("replyTo")]
        public string ReplyTo { get; private set; }

        /// <summary>
        /// Gets the Locale; the language in which to send the email out.
        /// </summary>
        [JsonProperty("locale")] 
        public string Locale { get; private set; }

        /// <summary>
        /// Gets the SenderName.
        /// </summary>
        [JsonProperty("senderName")] 
        public string SenderName { get; private set; }

        /// <summary>
        /// Gets the SenderEmail.
        /// </summary>
        [JsonProperty("senderEmail")] 
        public string SenderEmail { get; private set; }

        /// <summary>
        /// Gets the ReferenceNumber; the client reference number.
        /// </summary>
        [JsonProperty("referenceNumber")] 
        public string ReferenceNumber { get; private set; }

        /// <summary>
        /// Gets the ConsumerName.
        /// </summary>
        [JsonProperty("consumerName")] 
        public string ConsumerName { get; private set; }

        /// <summary>
        /// Gets the ConsumerEmail.
        /// </summary>
        [JsonProperty("consumerEmail")] 
        public string ConsumerEmail { get; private set; }

        /// <summary>
        /// Gets the ServiceReviewInvitation; The details of the invitation to be sent out.
        /// </summary>
        [JsonProperty("serviceReviewInvitation")] 
        public ApiServiceReviewInvitation ServiceReviewInvitation { get; private set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="ApiPayload"/> class.
        /// </summary>
        /// <param name="replyTo">The reply to to set.</param>
        /// <param name="locale">The locale to set.</param>
        /// <param name="senderName">The sender name to set.</param>
        /// <param name="senderEmail">The sender email to set.</param>
        /// <param name="referenceNumber">The reference number to set.</param>
        /// <param name="consumerName">The consumer name to set.</param>
        /// <param name="consumerEmail">The consumer email to set.</param>
        /// <param name="serviceReviewInvitation">The service review invitation to set.</param>
        public ApiPayload(
            string replyTo,
            string locale,
            string senderName,
            string senderEmail,
            string referenceNumber,
            string consumerName,
            string consumerEmail,
            ApiServiceReviewInvitation serviceReviewInvitation)
        {
            ReplyTo = replyTo;
            Locale = locale;
            SenderName = senderName;
            SenderEmail = senderEmail;
            ReferenceNumber = referenceNumber;
            ConsumerName = consumerName;
            ConsumerEmail = consumerEmail;
            ServiceReviewInvitation = serviceReviewInvitation;
        }
    }
}
