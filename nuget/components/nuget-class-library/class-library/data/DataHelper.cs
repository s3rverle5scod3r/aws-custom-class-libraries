using nuget_class_library.class_library.data.core;
using nuget_class_library.class_library.exception;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace nuget_class_library.class_library.data
{
    /// <summary>
    /// Guard extension method
    /// </summary>
    public static class DataHelper
    {
        /// <summary>
        /// Converts an object to dictionary.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <returns></returns>
        public static IDictionary<string, object> DataObjectToDictionary(this object source,
            BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            if (source == null)
            {
                throw new MissingDataException("Input value cannot be null or empty.");
            }

            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );
        }
    }
}

