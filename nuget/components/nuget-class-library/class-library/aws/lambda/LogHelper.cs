using Microsoft.Extensions.Logging;

namespace nuget_class_library.class_library.aws.lambda
{
	/// <summary>
	/// Helper class for logging Lambda function output.
	/// </summary>
	public class LogHelper
	{
		/// <summary>
		/// The ILogger instance; class acting as interface.
		/// </summary>
		private readonly ILogger logger;

		/// <summary>
		/// Gets the CurrentLogLevel.
		/// </summary>
		public LogLevel CurrentLogLevel { get; private set; }

		/// <summary>
		/// Initialises a new instance of the <see cref="LogHelper"/> class.
		/// </summary>
		/// <param name="logLevel">The minimum logging level, from 1 : 4. Any logging calls below this are ignored.</param>
		public LogHelper(int logLevel)
		{
			if (logLevel >= 1 && logLevel <= 4)
			{
				logger = ReturnLoggerInstance((LogLevel)logLevel);
			}
			else
			{
				logger = ReturnLoggerInstance(LogLevel.Information);
				logger.LogWarning($"Given log level {logLevel} is out of bounds. Defaulting to INFO (2).");
			}
		}

		/// <summary>
		/// Configuration of the Lambda logging to allow the use of the standard ASP.NET Core logging functionality to write CloudWatch Log events.
		/// All messages at level below the configured level are ignored for log output.
		/// </summary>
		private ILogger ReturnLoggerInstance(LogLevel level)
		{
			var logFactory = new LoggerFactory();

			var loggerOptions = new LambdaLoggerOptions
			{
				IncludeCategory = true,
				IncludeLogLevel = true,
				IncludeNewline = true,
				IncludeException = true,
				IncludeEventId = true,
				IncludeScopes = true
			};

			loggerOptions.Filter = (category, logLevel) => (logLevel >= level);

			logFactory.AddLambdaLogger(loggerOptions);
			CurrentLogLevel = level;
			return logFactory.CreateLogger<LambdaBase>();
		}

		/// <summary>
		/// Writes debug-level information to the logs.
		/// Use this for displaying low-level execution information (such as JSON serialisation results).
		/// </summary>
		/// <param name="output">The text to write to the logs.</param>
		public void LogDebug(string output)
		{
			logger.LogDebug(output);
		}

		/// <summary>
		/// Writes information-level information to the logs.
		/// Use this for displaying high-level execution information (such as completion of major execution steps).
		/// </summary>
		/// <param name="output">The text to write to the logs.</param>
		public void LogInfo(string output)
		{
			logger.LogInformation(output);
		}

		/// <summary>
		/// Writes warning-level information to the logs.
		/// Use this to log issues that may be handled to allow continued execution (such as defaulting).
		/// </summary>
		/// <param name="output">The text to write to the logs.</param>
		public void LogWarning(string output)
		{
			logger.LogWarning(output);
		}

		/// <summary>
		/// Writes error-level information to the logs.
		/// Use this to log issues that prevent further execution.
		/// </summary>
		/// <param name="output">The text to write to the logs.</param>
		public void LogError(string output)
		{
			logger.LogError(output);
		}
	}
}
