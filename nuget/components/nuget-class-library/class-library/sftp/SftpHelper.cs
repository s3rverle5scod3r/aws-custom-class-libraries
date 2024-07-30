using nuget_class_library.class_library.aws.lambda;
using Renci.SshNet;

namespace nuget_class_library.class_library.sftp
{
    public class SftpHelper : LambdaBase
    {
        /// <summary>
        /// Takes a created file and SFTP transfers to SFTP.
        /// </summary>
        /// <param name="localFilePath">The file path; the local file path.</param>
        /// <param name="uploadFileName">The file name; the name of the the file to be transferred.</param>
        /// <param name="hostAddress">The host address to connect to.</param>
        /// <param name="username">The username; login credentials.</param>
        /// <param name="password">The password; login credentials.</param>
        /// <param name="port">The port; for connection, what port to connect via.</param>
        public void SendFileToSftp(string localFilePath, string uploadFileName, string hostAddress, string username, string password, int port)
        {
            ConnectionInfo connectionInfo = new PasswordConnectionInfo(hostAddress, port, username, password);
            using var sftpClient = new SftpClient(connectionInfo);
            sftpClient.Connect();
            if (sftpClient.IsConnected)
            {
                logHelper.LogInfo($"Connected to the SFTP server and starting file transfer.");
                using var fileStream = new FileStream(localFilePath, FileMode.Open); // Local file name and path.
                sftpClient.BufferSize = 4 * 1024;                                    // Bypass Payload error of large files.
                sftpClient.UploadFile(fileStream, Path.GetFileName(uploadFileName)); // Upload transfer request to SFTP.
                logHelper.LogInfo($"File transfer to the SFTP server complete.");
            }
        }
    }
}

