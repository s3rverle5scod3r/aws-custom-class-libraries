﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace nuget_class_library.class_library.aws.apiGateway
{
    /// <summary>
    /// Class for holding response body data concerning API responses.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ApiGatewayResponseData
    {
        /// <summary>
        /// Gets the message to provide context in the response.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="ApiGatewayResponseData"/> class.
        /// </summary>
        /// <param name="message">The message to set.</param>
        public ApiGatewayResponseData(string message)
        {
            Message = message;
        }
    }
}
