using nuget_class_library.class_library.aws.lambda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nuget_class_library.class_library.data
{
    public class TemporaryFileHelper : LambdaBase
    {
        /// <summary>
        /// Updates the information held within a temporary file.
        /// </summary>
        /// <param name="temporaryWorkingFile">The name of the temporary working file to be updated.</param>
        /// <param name="temporaryWorkingFileUpdate">The temporary working file update; the updated information to write to the fle.</param>
        public void UpdateTemporaryWorkingFile(string temporaryWorkingFile, string temporaryWorkingFileUpdate)
        {
            // Write to the temporary working file.
            StreamWriter streamWriter = File.AppendText(temporaryWorkingFile);
            streamWriter.WriteLine(temporaryWorkingFileUpdate);
            streamWriter.Flush();
            streamWriter.Close();

            logHelper.LogDebug("Temporary working file updated.");
        }

        /// <summary>
        /// Deletes the temporary file.
        /// </summary>
        /// <param name="temporaryWorkingFile">The name of the temporary working file to be deleted.</param>
        /// <param name="reference">The unique reference for the request being sent.</param>
        public void DeleteTemporaryWorkingFile(string temporaryWorkingFile)
        {
            // Delete the temporary working file.
            if (File.Exists(temporaryWorkingFile))
            {
                File.Delete(temporaryWorkingFile);
                logHelper.LogDebug("Temporary working file deleted.");
            }
        }

    }
}

