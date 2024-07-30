using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Newtonsoft.Json;
using nuget_class_library.class_library.aws.kms;
using nuget_class_library.class_library.data.enums;
using nuget_class_library.class_library.exception;
using nuget_class_library.class_library.sql;

namespace nuget_class_library.class_library.aws.lambda
    
{
    /// <summary>
    /// Establishes core functionality/attributes for all Lambda functions.
    /// All Lambda functions should extend from this class.
    /// </summary>
    public abstract class LambdaBase
	{
        /// <summary>
        /// Gets the Logger instance.
        /// </summary>
        protected LogHelper logHelper;

        /// <summary>
        /// Gets the SalesforceHostAddress for API requests.
        /// </summary>
        protected string? SalesforceHostAddress { get; private set; }

        /// <summary>
        /// Gets the SalesforceOrganizationId for API requests.
        /// </summary>
        protected string? SalesforceOrganizationId { get; private set; }

        /// <summary>
        /// Gets the SalesforceUsername for API requests.
        /// </summary>
        protected string? SalesforceUsername { get; private set; }

        /// <summary>
        /// Gets the SalesforcePassword for API requests.
        /// </summary>
        protected string? SalesforcePassword { get; private set; }

        /// <summary>
        /// Gets the SftpHostAddress for transfer requests.
        /// </summary>
        protected string? SftpHostAddress { get; private set; }

        /// <summary>
        /// Gets the SftpUsername for transfer requests.
        /// </summary>
        protected string? SftpUsername { get; private set; }

        /// <summary>
        /// Gets the SftpAuthToken for transfer requests.
        /// </summary>
        protected string? SftpAuthToken { get; private set; }

        /// <summary>
        /// Gets the BrandSftpHostAddress for SFTP requests.
        /// </summary>
        protected string? BrandSftpHostAddress { get; private set; }

        /// <summary>
        /// Gets the BrandSftpUsername for SFTP requests.
        /// </summary>
        protected string? BrandSftpUsername { get; private set; }

        /// <summary>
        /// Gets the BrandSftpAuthToken for SFTP requests.
        /// </summary>
        protected string? BrandSftpAuthToken { get; private set; }

        /// <summary>
        /// Gets the SubmissionTopicArn for API requests.
        /// </summary>
        protected string? SubmissionTopicArn { get; private set; }

        /// <summary>
        /// Gets the FailureTopicArn for API requests.
        /// </summary>
        protected string? FailureTopicArn { get; private set; }

        /// <summary>
        /// Gets the S3BucketName; the ARN of the bucket.
        /// </summary>
        protected string? S3BucketName { get; private set; }

        /// <summary>
        /// Gets the OutputBucketName; the ARN of the bucket to which the output file is stored.
        /// </summary>
        protected string? OutputBucketName { get; private set; }

        /// <summary>
        /// Gets the UrlExpiryTimeMinutes; the expiry time of the presigned document url, in minutes.
        /// </summary>
        protected int UrlExpiryTimeMinutes { get; private set; }

        /// <summary>
		/// Gets the AuroraRdsDbName for connecting to the instance.
		/// </summary>
		protected string? AuroraRdsDbName { get; private set; }

        /// <summary>
        /// Gets the AuroraRdsHostname for connecting to the instance.
        /// </summary>
        protected string? AuroraRdsHostname { get; private set; }

        /// <summary>
        /// Gets the AuroraRdsSecretArn for connecting to the instance.
        /// </summary>
        protected string? AuroraRdsSecretArn { get; private set; }

        /// <summary>
        /// Gets the AuroraRdsSchema environment.
        /// </summary>
        protected string? AuroraRdsSchema { get; private set; }

        /// <summary>
        /// Gets the serialization settings for handling JSON packets.
        /// </summary>
        protected JsonSerializerSettings SerializerSettings { get; private set; }

        ///<summary>
        /// Gets the current runtime environment.
        ///</summary>
        protected RuntimeEnvironment RuntimeEnvironment { get; private set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="LambdaBase"/> class.
        /// Configures an instance of Logger.
        /// </summary>
        protected LambdaBase()
        {
			_ = int.TryParse(Environment.GetEnvironmentVariable("LOG_LEVEL"), out int logLevel)
				? logHelper = new LogHelper(logLevel)
				: logHelper = new LogHelper(2);
        }

        protected static SqlHelper GetSqlConnection()
        {
            return new SqlHelper(
                Environment.GetEnvironmentVariable("RDS_USERNAME") ?? throw new NullLambdaEnvironmentVariableException("RDS_USERNAME"),
                EncryptionHelper.DecodeEnvVar(Environment.GetEnvironmentVariable("RDS_PASSWORD") ?? throw new NullLambdaEnvironmentVariableException("RDS_PASSWORD")).Result,
                Environment.GetEnvironmentVariable("RDS_HOSTNAME") ?? throw new NullLambdaEnvironmentVariableException("RDS_HOSTNAME"),
                Environment.GetEnvironmentVariable("RDS_DB_NAME") ?? throw new NullLambdaEnvironmentVariableException("RDS_DB_NAME"));

        }

        protected void GetAndStoreSalesforceConnectionDetails()
        {
            SalesforceHostAddress = Environment.GetEnvironmentVariable("SALESFORCE_HOST_ADDRESS") ?? throw new NullLambdaEnvironmentVariableException("SALESFORCE_HOST_ADDRESS");
            SalesforceOrganizationId = Environment.GetEnvironmentVariable("SALESFORCE_ORGANIZATION_ID") ?? throw new NullLambdaEnvironmentVariableException("SALESFORCE_ORGANIZATION_ID");
            SalesforceUsername = Environment.GetEnvironmentVariable("SALESFORCE_USERNAME") ?? throw new NullLambdaEnvironmentVariableException("SALESFORCE_USERNAME");
            SalesforcePassword = EncryptionHelper.DecodeEnvVar(Environment.GetEnvironmentVariable("SALESFORCE_PASSWORD") ?? throw new NullLambdaEnvironmentVariableException("SALESFORCE_PASSWORD")).Result;
        }

        protected void GetAndStoreSftpConnectionDetails()
        {
            SftpHostAddress = Environment.GetEnvironmentVariable("SFTP_HOST_ADDRESS") ?? throw new NullLambdaEnvironmentVariableException("SFTP_HOST_ADDRESS");
            SftpUsername = Environment.GetEnvironmentVariable("SFTP_USERNAME") ?? throw new NullLambdaEnvironmentVariableException("SFTP_USERNAME");
            SftpAuthToken = EncryptionHelper.DecodeEnvVar(Environment.GetEnvironmentVariable("SFTP_AUTH_TOKEN") ?? throw new NullLambdaEnvironmentVariableException("SFTP_AUTH_TOKEN")).Result;
        }

        protected string GetAndStoreSftpConnectionHostAddress(string sftpHostAddress)
        {
            BrandSftpHostAddress = Environment.GetEnvironmentVariable(sftpHostAddress) ?? throw new NullLambdaEnvironmentVariableException(sftpHostAddress);
            return BrandSftpHostAddress;
        }

        protected string GetAndStoreSftpConnectionUsername(string sftpUsername)
        {
            BrandSftpUsername = Environment.GetEnvironmentVariable(sftpUsername) ?? throw new NullLambdaEnvironmentVariableException(sftpUsername);
            return BrandSftpUsername;
        }

        protected string GetAndStoreSftpConnectionAuthToken(string sftpAuthToken)
        {
            BrandSftpAuthToken = EncryptionHelper.DecodeEnvVar(Environment.GetEnvironmentVariable(sftpAuthToken) ?? throw new NullLambdaEnvironmentVariableException(sftpAuthToken)).Result;
            return BrandSftpAuthToken;
        }

        protected string GetAndStoreOutputSubmissionTopic(string submissionTopic)
        {
            SubmissionTopicArn = Environment.GetEnvironmentVariable(submissionTopic) ?? throw new NullLambdaEnvironmentVariableException(submissionTopic);
            return SubmissionTopicArn;
        }
        
        protected void GetAndStoreFailureTopic()
        {
            FailureTopicArn = Environment.GetEnvironmentVariable("FAILURE_TOPIC_ARN") ?? throw new NullLambdaEnvironmentVariableException("FAILURE_TOPIC_ARN");
        }

        protected void GetStoreS3BucketName()
        {
            S3BucketName = Environment.GetEnvironmentVariable("S3_BUCKET_NAME") ?? throw new NullLambdaEnvironmentVariableException("S3_BUCKET_NAME");
        }
        protected void GetStoreOutputBucketName()
        {
            OutputBucketName = Environment.GetEnvironmentVariable("OUTPUT_S3_BUCKET_NAME") ?? throw new NullLambdaEnvironmentVariableException("OUTPUT_S3_BUCKET_NAME");
        }
        protected void GetStoreUrlExpiryTimeMinutes()
        {
            UrlExpiryTimeMinutes = Convert.ToInt32(Environment.GetEnvironmentVariable("URL_EXPIRY_TIME_MINUTES"));
        }

        protected void GetAndStoreAuroraRdsConnectionDetails()
        {
            AuroraRdsDbName = Environment.GetEnvironmentVariable("AURORA_POSTGRES_DB_NAME") ?? throw new NullLambdaEnvironmentVariableException("AURORA_POSTGRES_DB_NAME");
            AuroraRdsHostname = Environment.GetEnvironmentVariable("AURORA_POSTGRES_HOSTNAME") ?? throw new NullLambdaEnvironmentVariableException("AURORA_POSTGRES_HOSTNAME");
            AuroraRdsSecretArn = Environment.GetEnvironmentVariable("AURORA_POSTGRES_SECRET_NAME") ?? throw new NullLambdaEnvironmentVariableException("AURORA_POSTGRES_SECRET_NAME");
            AuroraRdsSchema = Environment.GetEnvironmentVariable("AURORA_POSTGRES_SCHEMA") ?? throw new NullLambdaEnvironmentVariableException("AURORA_POSTGRES_SCHEMA");
        }

        protected void GetStoreRuntimeEnvironment()
        {
            RuntimeEnvironment = (RuntimeEnvironment)Enum.Parse(typeof(RuntimeEnvironment), Environment.GetEnvironmentVariable("ENVIRONMENT") ?? throw new NullLambdaEnvironmentVariableException("ENVIRONMENT"), true);
        }

        /// <summary>
        /// Gets a pre-signed URL for a given S3 item.
        /// </summary>
        /// <param name="bucketName">The name of the bucket containing the item.</param>
        /// <param name="bucketKey">The key of the item.</param>
        /// <param name="minutesAvailable">The amount of time the URL should be available to make requests to.</param>
        /// <returns>Temporary URL to the given file.</returns>
        protected static string GetDocumentPreSignedUrl(string bucketName, string bucketKey, int minutesAvailable)
        {
            using AmazonS3Client amazonS3Client = new(new AmazonS3Config { RegionEndpoint = RegionEndpoint.EUWest1, SignatureVersion = "v4" });
            var request = new GetPreSignedUrlRequest()
            {
                BucketName = bucketName,
                Key = bucketKey,
                Expires = DateTime.Now.AddMinutes(minutesAvailable)
            };

            return amazonS3Client.GetPreSignedURL(request);
        }
    }
}
