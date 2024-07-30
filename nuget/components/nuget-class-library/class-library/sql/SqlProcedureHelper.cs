using nuget_class_library.class_library.aws.lambda;
using nuget_class_library.class_library.exception;
using System.Data.SqlClient;

namespace nuget_class_library.class_library.sql
{
    public class SqlProcedureHelper : LambdaBase
    {
        public void UpdateStubbedSourceData(int stubId, string messageId, string storedProcedure)
        {
            var rdsConnection = GetSqlConnection();
            try
            {
                logHelper.LogDebug($"Updating stub entry in data.");
                rdsConnection.ExecuteStoredProcedure(
                    storedProcedure,
                    new List<SqlParameter>()
                    {
                        new SqlParameter("@stubId", stubId),
                        new SqlParameter("@messageId", messageId)
                    });

                logHelper.LogInfo("Data activity completed.");
            }
            catch (Exception exception)
            {
                logHelper.LogError($"Failed to perform data updates for stubId {stubId}: {exception}");
                throw new SqlExecuteStoredProcedureException();
            }
        }
    }
}
