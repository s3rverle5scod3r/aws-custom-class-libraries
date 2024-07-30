using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace nuget_class_library.class_library.aws.sns
{
	/// <summary>
	/// Helper class for AWS SNS topic-related functionality.
	/// </summary>
	public static class SnsTopicHelper
	{
		/// <summary>
		/// Generates a list of AWS-compatible message attributes for attachment to a message.
		/// Allows classes using this library to use attributes without having to import the SNS package.
		/// </summary>
		/// <param name="attributes">The attributes to transform into the AWS equivalent</param>
		/// <returns></returns>
		private static Dictionary<string, MessageAttributeValue> GenerateMessageAttributes(List<SnsMessageAttribute> attributes)
		{
			Dictionary<string, MessageAttributeValue> requestAttributes = new Dictionary<string, MessageAttributeValue>();
			foreach (var attribute in attributes)
			{
				requestAttributes.Add(
					attribute.Key,
					new MessageAttributeValue()
					{
						DataType = attribute.Type.ToString(),
						StringValue = attribute.Value
					});
			}
			return requestAttributes;
		}

		/// <summary>
		/// Publishes a message to a specified SNS topic.
		/// </summary>
		/// <param name="subject">Subject of the message; subjects over 100 characters are shortened with an ellipsis to 99.</param>
		/// <param name="message">Message contents.</param>
		/// <param name="topicArn">ARN of the target topic.</param>
		/// <param name="attributes">Optional message attributes.</param>
		public static void AddMessageToTopic(string subject, string message, string topicArn, List<SnsMessageAttribute>? attributes = null)
		{
			// Truncate subject if necessary
			if (subject.Length >= 100)
			{
				subject = $"{subject.Substring(0, 96)}...";
			}

			using var client = new AmazonSimpleNotificationServiceClient(RegionEndpoint.EUWest1);
			client.PublishAsync(new PublishRequest
			{
				Subject = subject,
				Message = message,
				TopicArn = topicArn,
				MessageAttributes = attributes == null ? new Dictionary<string, MessageAttributeValue>() : GenerateMessageAttributes(attributes)
			}).Wait();
		}

        /// <summary>
        /// Generates a list of AWS-compatible message attributes for attachment to a message.
        /// Allows classes using this library to use attributes without having to import the SNS package.
        /// </summary>
        /// <param name="attributes">The attributes to transform into the AWS equivalent</param>
        /// <returns></returns>
        public static Dictionary<string, MessageAttributeValue> GenerateMessageAttributesReturnId(List<SnsMessageAttribute> attributes)
        {
            Dictionary<string, MessageAttributeValue> requestAttributes = new Dictionary<string, MessageAttributeValue>();
            foreach (var attribute in attributes)
            {
                requestAttributes.Add(
                    attribute.Key,
                    new MessageAttributeValue()
                    {
                        DataType = attribute.Type.ToString(),
                        StringValue = attribute.Value
                    });
            }
            return requestAttributes;
        }

        /// <summary>
        /// Publishes a message to a specified SNS topic.
        /// </summary>
        /// <param name="subject">Subject of the message; subjects over 100 characters are shortened with an ellipsis to 99.</param>
        /// <param name="message">Message contents.</param>
        /// <param name="topicArn">ARN of the target topic.</param>
        /// <param name="attributes">Optional message attributes.</param>
        public static string AddMessageToTopicReturnId(string subject, string message, string topicArn, List<SnsMessageAttribute> attributes = null)
        {
            // Truncate subject if necessary
            if (subject.Length >= 100)
            {
                subject = $"{subject.Substring(0, 96)}...";
            }

            using var client = new AmazonSimpleNotificationServiceClient(RegionEndpoint.EUWest1);
            var response = client.PublishAsync(new PublishRequest
            {
                Subject = subject,
                Message = message,
                TopicArn = topicArn,
                MessageAttributes = attributes == null ? new Dictionary<string, MessageAttributeValue>() : GenerateMessageAttributesReturnId(attributes)
            });

            return response.Result.MessageId;
        }
    }
}
