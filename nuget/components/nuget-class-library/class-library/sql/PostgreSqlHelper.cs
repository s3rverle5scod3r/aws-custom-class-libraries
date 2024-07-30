using nuget_class_library.class_library.aws.lambda;
using Newtonsoft.Json;
using Npgsql;
using System.Data;

namespace nuget_class_library.class_library.sql
{
    public class PostgreSqlHelper : LambdaBase
    {
        public DataTable ExecuteAuroraPostgresStoredFunction(string connectionString, string sqlCommandText, Dictionary<string, object> parameters)
        {
            var dataTable = new DataTable();
            using var connection = new NpgsqlConnection(connectionString);
            try
            {
                connection.Open();
                logHelper.LogDebug("Connected to Aurora PostgreSQL database via RDS Proxy.");

                using var command = new NpgsqlCommand(sqlCommandText, connection);
                command.CommandTimeout = 60; // Timeout in seconds

                // Add parameters from the dictionary
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue($"{param.Key}", param.Value);
                }

                using var reader = command.ExecuteReader();
                dataTable.Load(reader);
                logHelper.LogDebug($"DataTable Results: {JsonConvert.SerializeObject(dataTable, SerializerSettings)}");

                connection.Close();
                logHelper.LogDebug("Connection to Aurora PostgreSQL database via RDS Proxy closed.");
            }
            catch (Exception exception)
            {
                throw new NpgsqlException($"NpgsqlException: Failed to connect to Rds instance - {exception.Message}\nStack trace: {exception.StackTrace}");
            }
            return dataTable;
        }

        public bool CheckRecordExistsInAuroraRdsTable(string connectionString, string schema, string storedProcedureName, long referenceId, string reference, string brand)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                connection.Open();
                logHelper.LogDebug($"Connected to Aurora PostgreSQL database via RDS Proxy.");

                using var command = new NpgsqlCommand($"SELECT * FROM {schema}.{storedProcedureName}(@p_referenceId, @p_reference, @p_brand)", connection);
                command.CommandTimeout = 60; // Timeout in seconds

                // Add parameters as per stored procedure's definition
                command.Parameters.AddWithValue("p_referenceId", referenceId);
                command.Parameters.AddWithValue("p_reference", reference);
                command.Parameters.AddWithValue("p_brand", brand);

                var recordExistence = (bool)command.ExecuteScalar();
                connection.Close();
                logHelper.LogDebug($"Record Existence : {recordExistence}.");
                logHelper.LogDebug("Record check complete. Connection to Aurora PostgreSQL database via RDS Proxy Closed.");
                return recordExistence;
            }
            catch (Exception exception)
            {
                throw new NpgsqlException($"NpgsqlException: Failed to check record existence - {exception.Message}\nStack trace: {exception.StackTrace}");
            }
        }
    }
}

