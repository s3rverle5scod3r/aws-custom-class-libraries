using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace nuget_class_library.class_library.api.trustpilot
{
    /// <summary>
    /// Holds data describing an individual Task and its details.
    /// </summary>
    /// <remarks>
    /// Initialises a new instance of the <see cref="TrustpilotReview"/> class.
    /// </remarks>
    /// <param name="brand">The brand to set.</param>
    /// <param name="reference">The reference to set.</param>
    /// <param name="email">The email to set.</param>
    /// <param name="firstName">The first name to set.</param>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class TrustpilotReview(
        string brand,
        string reference,
        string email,
        string firstName)
    {
        /// <summary>
        /// Gets the Brand.
        /// </summary>
        public string Brand { get; private set; } = brand;

        /// <summary>
        /// Gets the Reference.
        /// </summary>
        public string Reference { get; private set; } = reference;

        /// <summary>
        /// Gets the Email address.
        /// </summary>
        public string Email { get; private set; } = email;

        /// <summary>
        /// Gets the FirstName; The customers first name.
        /// </summary>
        public string FirstName { get; private set; } = firstName;
    }
}
