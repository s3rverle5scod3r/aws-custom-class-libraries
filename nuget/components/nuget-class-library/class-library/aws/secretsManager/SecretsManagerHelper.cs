using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using nuget_class_library.class_library.aws.lambda;

namespace nuget_class_library.class_library.aws.secretsManager
{
    public class SecretsManagerHelper : LambdaBase
    {
		public async Task<string> SecretsManagerCollectSecretAsync(string secretName)
		{
			IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName("eu-west-1"));

			GetSecretValueRequest request = new()
			{
				SecretId = secretName,
				VersionStage = "AWSCURRENT" // VersionStage defaults to AWSCURRENT if unspecified.
			};

			GetSecretValueResponse response;

			response = await client.GetSecretValueAsync(request);
			string secret = response.SecretString;
			return secret;

			// For a list of the exceptions thrown, see
			// https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
		}
	}
}
