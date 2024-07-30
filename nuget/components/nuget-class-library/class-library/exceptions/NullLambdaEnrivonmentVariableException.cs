using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nuget_class_library.class_library.exception
{
    public class NullLambdaEnvironmentVariableException : Exception
    {
        public NullLambdaEnvironmentVariableException(string variableName) : base($"The {variableName} environment variable was null and the process cannot proceed")
        {
        }
    }
}

