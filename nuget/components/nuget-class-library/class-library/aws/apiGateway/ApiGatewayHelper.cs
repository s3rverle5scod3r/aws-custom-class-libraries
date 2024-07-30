﻿using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using nuget_class_library.class_library.api;
using Newtonsoft.Json;

namespace nuget_class_library.class_library.aws.apiGateway
{
	/// <summary>
	/// Helper class for interacting with AWS API Gateway.
	/// </summary>
	public static class ApiGatewayHelper
	{
        /// <summary>
        /// Returns an APIGatewayProxyResponse for a failed request.
        /// </summary>
        /// <param name="errorMessage">Descriptive message for error.</param>
        /// <param name="httpStatusCode">Status code to correspond with message.</param>
        /// <returns>A response to be sent back to the gateway.</returns>
        public static APIGatewayProxyResponse GetFailureResponse(
            HttpStatusCode httpStatusCode,
            string description)
        {
            return new APIGatewayProxyResponse()
            {
                IsBase64Encoded = false,
                StatusCode = (int)httpStatusCode,
                Body = JsonConvert.SerializeObject(new ApiErrorResponseData(description))
            };
        }
        
        /// <summary>
        /// Returns an APIGatewayProxyResponse for a request.
        /// </summary>
        /// <param name="httpStatusCode">Status code to correspond with message.</param>
        /// <param name="description">Description message to provide context in the response.</param>
        /// <returns>A response to be sent back to the gateway.</returns>
        public static APIGatewayProxyResponse GetApiGatewayResponse(
            HttpStatusCode httpStatusCode,
            string description)
        {
            return new APIGatewayProxyResponse()
            {
                IsBase64Encoded = false,
                StatusCode = (int)httpStatusCode,
                Body = JsonConvert.SerializeObject(new ApiGatewayResponseData(description))
            };
        }
    }
}
