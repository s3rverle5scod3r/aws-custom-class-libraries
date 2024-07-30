using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace nuget_class_library.class_library.sql
{
    /// <summary>
    /// Helper class for SQL Server database functionality.
    /// </summary>
    public class SqlHelper : IDisposable
    {
        /// <summary>
        /// Connection to the target database.
        /// </summary>
		private readonly SqlConnection sqlConnection;

        /// <summary>
        /// Initialises a new instance of the <see cref="SqlHelper"/> class.
        /// Establishes a connection to the target database.
        /// </summary>
        /// <param name="username">The SQL login username to connect with.</param>
        /// <param name="password">The SQL login password to connect with.</param>
        /// <param name="hostname">The hostname of the SQL server to connect to.</param>
        /// <param name="databaseName">The database to connect to under the SQL server.</param>
		public SqlHelper(string username, string password, string hostname, string databaseName)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(username));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(password));
            }

            if (string.IsNullOrEmpty(hostname))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(hostname));
            }

            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(databaseName));
            }

            if (sqlConnection == null || string.IsNullOrEmpty(sqlConnection.ConnectionString))
            {
                sqlConnection = new SqlConnection(new SqlConnectionStringBuilder()
                {
                    Password = password,
                    UserID = username,
                    DataSource = hostname,
                    InitialCatalog = databaseName,
                    MaxPoolSize = 250,
                    ConnectRetryCount = 3,
                    ConnectRetryInterval = 2
                }.ConnectionString);
                sqlConnection.Open();
            }
            else if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        /// <summary>
        /// Executes a stored procedure and returns a DataTable of results.
        /// </summary>
        /// <param name="procedureName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Optional list of parameters to run as arguments in the stored procedure.</param>
        /// <returns>DataTable containing the procedure results, if any.</returns>
        public DataTable GetDataTableFromStoredProcedure(string procedureName, List<SqlParameter>? parameters = null)
        {
            if (string.IsNullOrEmpty(procedureName))
            {
                throw new ArgumentException("Procedure name cannot be null or empty.");
            }

            using (var command = sqlConnection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = procedureName;

                if (parameters != null)
				{
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
				}

                var result = new DataTable();
                using (var dataAdapter = new SqlDataAdapter(command))
                {
                    dataAdapter.Fill(result);
                }
                return result;
            }
        }

        /// <summary>
        /// Executes a stored procedure in a fire-and-forget pattern.
        /// </summary>
        /// <param name="procedureName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Optional list of parameters to run as arguments in the stored procedure.</param>
        /// <returns>Integer count of the number of rows affected as a result of the procedure call.</returns>
        public void ExecuteStoredProcedure(string procedureName, List<SqlParameter>? parameters = null)
		{
            if (string.IsNullOrEmpty(procedureName))
            {
                throw new ArgumentException("Procedure name cannot be null or empty.");
            }

            using (var command = sqlConnection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = procedureName;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                command.ExecuteNonQuery();
            }
		}

        /// <summary>
        /// Execute the stored procedure and return the dataset produced, capable of single-table queries.
        /// </summary>
        /// <param name="procedureName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">List of parameters to run as arguments in the targeted stored procedure.</param>
        /// <returns>SqlDataReader containing the procedure results, if any.</returns>
        protected SqlDataReader GetStoredProcedureResults(string procedureName, List<SqlParameter> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (string.IsNullOrEmpty(procedureName))
            {
                throw new ArgumentException("Procedure name cannot be null or empty.");
            }

            using (var command = sqlConnection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = procedureName;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                var result = command.ExecuteReader(CommandBehavior.CloseConnection);
                return result;
            }
        }

        /// <summary>
        /// Allow the class to be created and consumed within using statements.
        /// Releases SQL resources.
        /// </summary>
        public void Dispose()
        {
            sqlConnection.Close();
            sqlConnection.Dispose();
        }
    }
}
