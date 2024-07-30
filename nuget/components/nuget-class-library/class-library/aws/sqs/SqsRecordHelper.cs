using Amazon.Lambda.SQSEvents;
using nuget_class_library.class_library.aws.lambda;
using nuget_class_library.class_library.exception;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace nuget_class_library.class_library.aws.sqs
{
    /// <summary>
    /// Helper class for functionality related to the processing of Kinesis records.
    /// </summary>
	public class SqsRecordHelper : LambdaBase
    {
        public static string GetMessageDataCheckForNull(string sqsMessage)
        {
            if (string.IsNullOrWhiteSpace(sqsMessage))
            {
                throw new ArgumentException($"'{nameof(sqsMessage)}' cannot be null or whitespace.", nameof(sqsMessage));
            }

            return sqsMessage;
        }

        public static JObject MessageTokenDataCheckForNull(JObject sqsMessage)
        {
            if (sqsMessage.SelectToken("Message") == null || string.IsNullOrWhiteSpace(sqsMessage.Value<string>("Message")))
            {
                // Cannot process a record with missing or empty master node
                throw new MissingDataException("Message property was empty or could not be found.");
            }
            return sqsMessage;
        }

        public bool SqsEventCheckForNull(SQSEvent sqsEvent)
        {
            if (sqsEvent == null)
            {
                logHelper.LogError($"SQSEvent cannot be null; execution cannot continue.");
                return true;
            }
            if (sqsEvent.Records == null || sqsEvent.Records.Count == 0)
            {
                logHelper.LogError($"SQSEvent cannot be null or contain zero records; execution cannot continue.");
                return true;
            }

            return false;
        }
    }
}




