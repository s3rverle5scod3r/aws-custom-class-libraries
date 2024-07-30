using nuget_class_library.class_library.aws.lambda;
using nuget_class_library.class_library.aws.sns;
using nuget_class_library.class_library.data;
using Newtonsoft.Json;
using Npgsql;
using System.Data;
using System.Data.SqlClient;

namespace nuget_class_library.class_library.errorHandling
{
    public class ErrorHandlingHelper : LambdaBase
    {
        /// <summary>
        /// Takes error information and notifies the connected alert topic, as well as recording the error details in Rds.
        /// </summary>
        /// <param name="lambdaEnvironment">the lambda environment; this forms the subject of the alert message.</param>
        /// <param name="reason">A brief reason for failure; this forms the subject of the alert message.</param>
        /// <param name="microservice">The microservice component name; this forms the subject of the alert message.</param>
        /// <param name="context">Contextual information regarding the error, such as data being processed.</param>
        /// <param name="failureTopicArn">The target topic for the error to be sent to.</param>
        /// <param name="storedProcedure">The stored procedure to use.</param>
        /// <param name="exception">The exception, if applicable.</param>
        public void NotifyAndRecordError(
            string lambdaEnvironment,
            string reason,
            string microservice,
            string context,
            string failureTopicArn,
            string storedProcedure,
            Exception? exception = null)
        {
            var serializerSettings = new JsonSerializerSettings();

            SnsTopicHelper.AddMessageToTopic(
                $"{lambdaEnvironment.ToLower()}_{microservice} failed: {reason}",
                context,
                failureTopicArn);

            var rdsConnection = GetSqlConnection();

            rdsConnection.ExecuteStoredProcedure(
                storedProcedure,
                new List<SqlParameter>()
                {
                    new SqlParameter("@errorDate", UnitHelper.DateTimeToIsoString(DateTime.Now)),
                    new SqlParameter("@errorReason", reason),
                    new SqlParameter("@errorContext", context),
                    new SqlParameter("@errorExceptionData", exception == null ? "No exception data." : JsonConvert.SerializeObject(exception, serializerSettings))
                });
        }

        /// <summary>
        /// Takes error information and notifies the connected alert topic.
        /// </summary>
        /// <param name="lambdaEnvironment">the lambda environment; this forms the subject of the alert message.</param>
        /// <param name="reason">A brief reason for failure; this forms the subject of the alert message.</param>
        /// <param name="microservice">The microservice component name; this forms the subject of the alert message.</param>
        /// <param name="context">Contextual information regarding the error, such as data being processed.</param>
        /// <param name="failureTopicArn">The target topic for the error to be sent to.</param>
        public void NotifyError(
            string lambdaEnvironment,
            string reason,
            string microservice,
            string context,
            string failureTopicArn)
        {
            var serializerSettings = new JsonSerializerSettings();

            SnsTopicHelper.AddMessageToTopic(
                $"{lambdaEnvironment.ToLower()}_{microservice} failed: {reason}",
                JsonConvert.SerializeObject(context, serializerSettings),
                failureTopicArn);
        }

        /// <summary>
        /// Takes error information and records the error details in Rds.
        /// </summary>
        /// <param name="reason">A brief reason for failure; this forms the subject of the alert message.</param>
        /// <param name="context">Contextual information regarding the error, such as data being processed.</param>
        /// <param name="storedProcedure">The stored procedure to use.</param>
        /// <param name="exception">The exception, if applicable.</param>
        public void RecordError(
            string reason,
            string context,
            string storedProcedure,
            Exception? exception = null)
        {
            var serializerSettings = new JsonSerializerSettings();

            var rdsConnection = GetSqlConnection();

            rdsConnection.ExecuteStoredProcedure(
                storedProcedure,
                new List<SqlParameter>()
                {
                    new SqlParameter("@errorDate", UnitHelper.DateTimeToIsoString(DateTime.Now)),
                    new SqlParameter("@errorReason", reason),
                    new SqlParameter("@errorContext", context),
                    new SqlParameter("@errorExceptionData", exception == null ? "No exception data." : JsonConvert.SerializeObject(exception, serializerSettings))
                });
        }

        public void NotifyToFailureTopicAndRecordErrorIntoAuroraRdsErrorTable(
            string lambdaEnvironment,
            string reason,
            string microservice,
            string context,
            string failureTopicArn,
            string connectionString, 
            string schema,
            Exception? exception = null)
        {
            var serializerSettings = new JsonSerializerSettings();

            SnsTopicHelper.AddMessageToTopic(
                $"{lambdaEnvironment.ToLower()}_{microservice} failed: {reason}",
                context,
                failureTopicArn);

            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            logHelper.LogInfo($"Connected to Aurora PostgreSQL database via RDS Proxy.");

            using var command = new NpgsqlCommand($"{schema}.p_utilities_insert_error_record", connection);
            command.CommandType = CommandType.StoredProcedure;

            // Add parameters as per stored procedure's definition
            command.Parameters.AddWithValue("p_errorDate", UnitHelper.DateTimeToIsoString(DateTime.Now));
            command.Parameters.AddWithValue("p_errorReason", reason);
            command.Parameters.AddWithValue("p_errorContext", context);
            command.Parameters.AddWithValue("p_errorExceptionData", exception == null ? "No exception data." : JsonConvert.SerializeObject(exception, serializerSettings));

            command.ExecuteNonQuery();
            connection.Close();
            logHelper.LogInfo("Error table update complete. Connection to Aurora PostgreSQL database via RDS Proxy Closed.");
        }

        /// <summary>
        /// Takes error information and notifies the connected alert topic.
        /// </summary>
        /// <param name="lambdaEnvironment">the lambda environment; this forms the subject of the alert message.</param>
        /// <param name="reason">A brief reason for failure; this forms the subject of the alert message.</param>
        /// <param name="microservice">The microservice component name; this forms the subject of the alert message.</param>
        /// <param name="context">Contextual information regarding the error, such as data being processed.</param>
        /// <param name="failureTopicArn">The target topic for the error to be sent to.</param>
        public void NotifyErrorToFailureTopic(
            string lambdaEnvironment,
            string reason,
            string microservice,
            string context,
            string failureTopicArn)
        {
            var serializerSettings = new JsonSerializerSettings();

            SnsTopicHelper.AddMessageToTopic(
                $"{lambdaEnvironment.ToLower()}_{microservice} failed: {reason}",
                JsonConvert.SerializeObject(context, serializerSettings),
                failureTopicArn);
        }

        /// <summary>
        /// Takes error information and records the error details in Rds.
        /// </summary>
        /// <param name="reason">A brief reason for failure; this forms the subject of the alert message.</param>
        /// <param name="context">Contextual information regarding the error, such as data being processed.</param>
        /// <param name="storedProcedure">The stored procedure to use.</param>
        /// <param name="exception">The exception, if applicable.</param>
        public void RecordErrorIntoAuroraRdsErrorTable(
            string reason,
            string context,
            string connectionString,
            string schema,
            Exception? exception = null)
        {
            var serializerSettings = new JsonSerializerSettings();

            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            logHelper.LogInfo($"Connected to Aurora PostgreSQL database via RDS Proxy.");

            using var command = new NpgsqlCommand($"{schema}.p_utilities_insert_error_record", connection);
            command.CommandType = CommandType.StoredProcedure;

            // Add parameters as per stored procedure's definition
            command.Parameters.AddWithValue("p_errorDate", UnitHelper.DateTimeToIsoString(DateTime.Now));
            command.Parameters.AddWithValue("p_errorReason", reason);
            command.Parameters.AddWithValue("p_errorContext", context);
            command.Parameters.AddWithValue("p_errorExceptionData", exception == null ? "No exception data." : JsonConvert.SerializeObject(exception, serializerSettings));

            command.ExecuteNonQuery();
            connection.Close();
            logHelper.LogInfo("Error table update complete. Connection to Aurora PostgreSQL database via RDS Proxy Closed.");
        }
    }
}

