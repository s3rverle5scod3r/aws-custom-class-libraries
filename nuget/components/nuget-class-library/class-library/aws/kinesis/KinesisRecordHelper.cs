using System.Text;
using Amazon.Lambda.KinesisEvents;
using nuget_class_library.class_library.data.core;
using nuget_class_library.class_library.exception;
using Newtonsoft.Json.Linq;

namespace nuget_class_library.class_library.aws.kinesis
{
    /// <summary>
    /// Helper class for functionality related to the processing of Kinesis records.
    /// </summary>
	public static class KinesisRecordHelper
    {
        public static string GetKinesisRecordContents(KinesisEvent.Record streamRecord)
        {
            using StreamReader streamReader = new(streamRecord.Data, Encoding.ASCII);
            return streamReader.ReadToEnd();
        }

        public static Core ExtractAndValidateCore(KinesisEvent.Record record)
        {
            var recordContents = GetRecordContents(record);
            ValidateRecordMetadata(recordContents);
            return GenerateCoreFromKinesisData(GetRecordData(recordContents));

        }

        /// <summary>
        /// Return the content of a Kinesis record in string format.
        /// </summary>
        /// <param name="streamRecord">The Kinesis record to process.</param>
        /// <returns>Decoded record contents.</returns>
        public static JObject GetRecordContents(KinesisEvent.Record streamRecord)
        {
            using var reader = new StreamReader(streamRecord.Data, Encoding.ASCII);
            return JObject.Parse(reader.ReadToEnd());
        }

        public static JToken GetRecordData(JObject rawData)
        {
            var recordData = rawData.SelectToken("$.data") ?? throw new ArgumentNullException("Kinesis record has no data and cannot be processed");
            return recordData;
        }

        public static void ValidateRecordMetadata(JObject rawData)
        {
            var recordMetadata = rawData.SelectToken("$.metadata") ?? throw new ArgumentNullException("Kinesis record has no metdata and cannot be processed");

            if (recordMetadata.Value<string>("operation") is null)
            {
                throw new ArgumentNullException("Record operation is null. Cannot continue");
            }
            else if (recordMetadata.Value<string>("operation") == "delete")
            {
                throw new SqlDeleteOperationException();
            }

        }
        public static bool ParseKinesisStringToBoolean(string value)
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

        public static Core GenerateCoreFromKinesisData(JToken recordData)
        {
            var reference = recordData.Value<string>("reference") ?? throw new ArgumentNullException("Client Reference is not included in the record data");
            var brand = recordData.Value<string>("brand") ?? throw new ArgumentNullException("Brand is not included in the record data");

            var core = new Core(
                recordData.Value<long>("referenceId"),
                reference,
                brand);

            return core ?? throw new ArgumentNullException("Record data has failed to generate as a valid Core model for engine evaluation.");
        }
    }
}




