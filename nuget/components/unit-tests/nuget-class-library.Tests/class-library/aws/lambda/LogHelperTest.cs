using nuget_class_library.class_library.aws.lambda;
using Xunit;

namespace nuget_class_library.Tests.class_library.aws.lambda
{
	/// <summary>
	/// Unit tests for LogHelper class.
	/// </summary>
	public class LogHelperTest
	{
		/// <summary>
		/// Positively tests the LogHelper constructor.
		/// Given a log level from 1 : 4, the LogHelper should be initialized with the corresponding LogLevel enum:
		/// 1: DEBUG, 2: INFO, 3: WARNING, 4: ERROR
		/// </summary>
		/// <param name="logLevelValue">The log level to set.</param>
		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		[InlineData(4)]
		public void LogHelper_SetsLogLevel_ForValidLevel(int logLevelValue)
		{
			// Arrange, Act
			var logHelper = new LogHelper(logLevelValue);

			// Assert
			Assert.Equal(logLevelValue, (int)logHelper.CurrentLogLevel);
		}

		/// <summary>
		/// Positively tests the LogHelper constructor.
		/// Given a log level outside of 1 : 4, the LogHelper should be initialized with the default LogLevel enum INFO.
		/// </summary>
		/// <param name="logLevelValue">The log level to set.</param>
		[Theory]
		[InlineData(0)]
		[InlineData(99)]
		public void LogHelper_DefaultsLogLevel_ForInvalidLevel(int logLevelValue)
		{
			// Arrange, Act
			var logHelper = new LogHelper(logLevelValue);

			// Assert
			Assert.Equal(2, (int)logHelper.CurrentLogLevel);
		}
	}
}
