using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nuget_class_library.class_library.exception
{
    public class SqlExecuteStoredProcedureException : Exception
    {
        public SqlExecuteStoredProcedureException() : base("Sql Execute Stored Procedure Operation has failed to complete and the process cannot proceed")
        { 
        }
    }
}
