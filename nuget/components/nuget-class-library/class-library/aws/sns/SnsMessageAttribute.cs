using Amazon.SimpleNotificationService.Model;

namespace nuget_class_library.class_library.aws.sns
{
	/// <summary>
	/// Describes a message attribute associated with a message.
	/// </summary>
	public class SnsMessageAttribute
	{
		/// <summary>
		/// Gets the Key.
		/// </summary>
		public string Key { get; private set; }

		/// <summary>
		/// Gets the Value.
		/// </summary>
		public string Value { get; private set; }

		/// <summary>
		/// Gets the Type.
		/// </summary>
		public SnsMessageAttributeValueTypes Type { get; private set; }

		/// <summary>
		/// Initialises a new instance of the <see cref="SnsMessageAttribute"/> class.
		/// </summary>
		/// <param name="key">The key to assign.</param>
		/// <param name="value">The value to assign.</param>
		/// <param name="type">The type to assign.</param>
		public SnsMessageAttribute(string key, string value, SnsMessageAttributeValueTypes type)
		{
			Key = key;
			Value = value;
			Type = type;
		}
    }
}
