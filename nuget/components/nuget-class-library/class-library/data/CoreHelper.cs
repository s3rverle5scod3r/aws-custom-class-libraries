using nuget_class_library.class_library.data.core;
using nuget_class_library.class_library.exception;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace nuget_class_library.class_library.data
{
    public static class CoreHelper
    {
        public static Core GenerateCoreFromSQSEvent(string body)
        {
            if (string.IsNullOrWhiteSpace(body))
            {
                throw new ArgumentException($"'{nameof(body)}' cannot be null or whitespace.", nameof(body));
            }

            var message = JObject.Parse(body) ?? throw new ArgumentException($"'{nameof(body)}' property was empty or could not be found.", nameof(body));

            if (message.SelectToken("Message") == null || string.IsNullOrWhiteSpace(message.Value<string>("Message")))
            {
                // Cannot process a record with missing or empty master node
                throw new MissingDataException("Message property was empty or could not be found.");
            }

            var messageToken = message.Value<string>("Message") ?? throw new MissingDataException("Message property was empty or could not be found.");
            var deserializedJsonObject = JsonConvert.DeserializeObject<Core>(messageToken) ?? throw new MissingDataException("Message property was empty or could not be found.");
            
            return deserializedJsonObject;
        }

        public static bool ParseSQSStringToBoolean(string value)
        {
            // Normalize the string to lower case for comparison
            value = value.ToLower();

            if (value == "true" || value == "t")
            {
                return true;
            }
            else if (value == "false" || value == "f")
            {
                return false;
            }

            return false;
        }
    }
}

