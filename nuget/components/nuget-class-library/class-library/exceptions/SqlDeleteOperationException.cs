using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nuget_class_library.class_library.exception
{
    public class SqlDeleteOperationException : Exception
    {
        public SqlDeleteOperationException() : base("Kinesis Operation in event is SQLDelete and should not be processed") { }
    }
}
