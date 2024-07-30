using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
using nuget_class_library.class_library.data;

namespace nuget_class_library.class_library.api.trustpilot
{
    /// <summary>
    /// Helper class for API calls to Trustpilot platform.
    /// </summary>
    public static class ApiHelper
    {
        /// <summary>
        /// Sends a request to the Trustpilot API, given path parameters, payload and authentication.
        /// </summary>
        /// <param name="environment">The deployment environment of the calling Lambda; determines endpoint.</param>
        /// <param name="host">The base host address for the request.</param>
        /// <param name="id">The business unit id to call on the API.</param>
        /// <param name="payload">The payload to deliver to Trustpilot.</param>
        /// <param name="brand">The brand for the request.</param>
        /// <param name="authToken">The authentication token for the request.</param>
        /// <returns>Response from the Trustpilot API.</returns>
		public static RestResponse SendRequest(
            string environment,
            string host,
            object payload,
            string id, 
            string brand,
            string authToken)
        {
            if (string.IsNullOrEmpty(environment))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(environment));
            }

            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(host));
            }

            if (payload == null)
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(payload));
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            }
            
            if (string.IsNullOrEmpty(brand))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(brand));
            }

            if (string.IsNullOrEmpty(authToken))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(authToken));
            }

            using HttpClient httpClient = new HttpClient();
            var options = new RestClientOptions(GenerateEndpoint(host, environment, id, brand));
            var client = new RestClient(options);
            
            var request = new RestRequest
            {
                Method = Method.Post
            };
            request.AddHeader("Authorization", $"Bearer {authToken}");
            request.AddHeader("Content-Type", "application/json");
            var body = @$"{payload}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);

            return response;
        }

        /// <summary>
        /// Constructs the request endpoint for a given host/id/method path.
        /// </summary>
        /// <param name="host">The host/root address.</param>
        /// <param name="environment">The deployment environment: DEV/TEST/STAGE/PROD.</param>
        /// <param name="id">The business unit id to use for the request.</param>
        /// <param name="brand">The brand to direct the request under.</param>
        /// <returns>The complete endpoint URL required to make a request to the Trustpilot API.</returns>
		public static string GenerateEndpoint(string host, string environment, string id, string brand)
        {
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(host));
            }

            if (string.IsNullOrEmpty(environment))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(environment));
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            }
            
            if (string.IsNullOrEmpty(brand))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(brand));
            }

            switch (environment)
            {
                case "DEV":
                    return host;
                case "TEST":
                    return host;
                case "STAGE":
                    if (string.IsNullOrEmpty(id))
                    {
                        throw new ArgumentException("Value cannot be null or empty.", nameof(brand));
                    }

                    return $"{host}/{id.ToLower()}/email-invitations";
                case "PROD":
                    if (string.IsNullOrEmpty(id))
                    {
                        throw new ArgumentException("Value cannot be null or empty.", nameof(brand));
                    }

                    return $"{host}/{id.ToLower()}/email-invitations";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Sends a request to the Authentication server of the Trustpilot API, given path parameters, to receive an authToken for future requests.
        /// </summary>
        /// <param name="apiTokenHostAddress">The host address for the request.</param>
        /// <param name="authKey">The auth key for the request.</param>
        /// <param name="authSecret">The auth secret for the request.</param>
        /// <param name="username">The payload to deliver to Intilery.</param>
        /// <param name="password">The brand for the request.</param>
        /// <returns>Response from the Trustpilot API.</returns>
		public static async Task<string> AuthRequestAsync(
            string apiTokenHostAddress,
            string authKey,
            string authSecret,
            string username,
            string password)
        {
            if (string.IsNullOrEmpty(apiTokenHostAddress))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(apiTokenHostAddress));
            }

            if (string.IsNullOrEmpty(authKey))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(authKey));
            }

            if (string.IsNullOrEmpty(authSecret))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(authSecret));
            }

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(username));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(password));
            }

            // Authenticate Trustpilot API and collect auth token for full run.
            using HttpClient httpClient = new HttpClient();
            string tokenUrl = $"{apiTokenHostAddress}";
            httpClient.BaseAddress = new Uri(tokenUrl);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            var authString = $"{authKey}:{authSecret}";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", UnitHelper.Base64Encode(authString));
            var stringPayload = $"grant_type=password&username={username}&password={password}";
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/x-www-form-urlencoded");

            using HttpResponseMessage response = await httpClient.PostAsync(tokenUrl, httpContent);

            using HttpContent responseContent = response.Content;
            string responseData = await responseContent.ReadAsStringAsync();

            return JsonConvert.SerializeObject(responseData);
        }
    }
}
