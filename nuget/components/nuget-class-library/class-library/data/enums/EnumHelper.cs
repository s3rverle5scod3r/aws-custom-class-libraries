using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nuget_class_library.class_library.data.enums
{
    public class EnumHelper
    {
        public static string TransformBusinessEventStringForEnum(string input)
        {
            // Check if the string contains a space
            if (input.Contains(' '))
            {
                // Replace spaces with underscores
                return input.Replace(" ", "_");
            }
            else
            {
                // Return the original string if no space is found
                return input;
            }
        }
    }
}

