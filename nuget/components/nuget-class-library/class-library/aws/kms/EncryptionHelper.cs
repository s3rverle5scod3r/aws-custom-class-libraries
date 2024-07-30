using System.Text;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;

namespace nuget_class_library.class_library.aws.kms
{
    /// <summary>
    /// Helper class for crytographic operations.
    /// </summary>
    public static class EncryptionHelper
	{
        /// <summary>
        /// Take a ciphertext in Base64 and return the plaintext.
        /// </summary>
        /// <param name="baseString">Base64 string to decode and decrypt.</param>
        /// <returns>The usable (plaintext) value for the given environment variable.</returns>
        public static async Task<string> DecodeEnvVar(string baseString)
        {
            if (string.IsNullOrWhiteSpace(baseString))
            {
                throw new ArgumentException("Ciphetext to decrypt cannot be null or empty.");
            }

            using (var client = new AmazonKeyManagementServiceClient())
            {
                var response = await client.DecryptAsync(new DecryptRequest
                {
                    CiphertextBlob = new MemoryStream(Convert.FromBase64String(baseString)),
                });

                using (var plaintextStream = response.Plaintext)
                {
                    var plaintextBytes = plaintextStream.ToArray();
                    var plaintext = Encoding.UTF8.GetString(plaintextBytes);
                    return plaintext;
                }
            }
        }
    }
}
