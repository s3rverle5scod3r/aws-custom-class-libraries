using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace nuget_class_library.class_library.api
{
	/// <summary>
	/// Class for holding response body data concerning non-200 API responses.
	/// </summary>
	[JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
	public class ApiErrorResponseData
	{
		/// <summary>
		/// Gets the description indicating the reason for failure.
		/// </summary>
		public string Message { get; private set; }

		/// <summary>
		/// Initialises a new instance of the <see cref="ApiErrorResponseData"/> class.
		/// </summary>
		/// <param name="description">The description to set.</param>
		public ApiErrorResponseData(string message)
		{
			Message = message;
		}
	}
}
