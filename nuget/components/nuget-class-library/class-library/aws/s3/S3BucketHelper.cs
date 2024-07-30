using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using nuget_class_library.class_library.aws.lambda;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;

namespace nuget_class_library.class_library.aws.s3
{
    public class S3BucketHelper : LambdaBase
	{
		/// <summary>
		/// Gets specified object from specified S3 bucket and returns a temporary filepath for local access.
		/// </summary>
		/// <param name="originBucketName">The name of the bucket to retrieve from.</param>
		/// <param name="originObjectKey">The key of the item to get from the bucket.</param>
		/// <returns>A filepath to the document.</returns>
		/// ***###FUNCTION IS DEPRICATED, USE CollectObjectFromS3ReturnTempFilePath###***
		public string CollectObjectFromS3ReturnTempFilePathOrResponse(string originBucketName, string originObjectKey)
		{
			var client = new AmazonS3Client();
			try
			{
				object? responseString = null;
				var request = new GetObjectRequest
				{
					BucketName = originBucketName,
					Key = originObjectKey
				};

				responseString = client.GetObjectAsync(request).Result;

				if (responseString == null)
				{
					logHelper.LogInfo($"Bucket is empty. Exiting.");
					logHelper.LogInfo($"responseString: {responseString}");
				}
				else
				{
					var tempPath = Path.GetTempFileName();
					using var responseStream = client.GetObjectAsync(request).Result.ResponseStream;
					using Stream stream = File.Create(tempPath);
					logHelper.LogInfo($"Writing object to tempPath: {tempPath}");
					responseStream.CopyTo(stream);
					return tempPath;
				}
				throw new Exception($"Could not collect document from S3.");
			}
			catch (Exception exception)
			{
				logHelper.LogError($"Could not collect document from S3. Fatal error: {exception.Message}\nStack trace: {exception.StackTrace}");
				throw;
			}
		}

		/// <summary>
		/// Gets specified object from specified S3 bucket and returns a temporary filepath for local access.
		/// </summary>
		/// <param name="originBucketName">The name of the bucket to retrieve from.</param>
		/// <param name="originObjectKey">The key of the item to get from the bucket.</param>
		/// <param name="originSubDirectoryInBucket">The subdirectory within the bucket to retrive from.</param>
		/// <returns>A filepath to the document.</returns>
		public string CollectObjectFromS3ReturnTempFilePath(string originBucketName, string originObjectKey, string? originSubDirectoryInBucket = null)
		{
			AmazonS3Client s3Client = new();
			try
			{
				// Making a GetObjectRequest instance.
				GetObjectRequest getObjectRequest = new();

				// Setting the 
				object? responseString = null;

				if (originSubDirectoryInBucket == "" || originSubDirectoryInBucket == null)
				{
					// If no subdirectory just bucket name.
					getObjectRequest.BucketName = originBucketName;
				}
				else
				{   // If sub subdirectory specified, subdirectory and bucket name.
					getObjectRequest.BucketName = originBucketName + @"/" + originSubDirectoryInBucket;
				}
				getObjectRequest.Key = originObjectKey;
				responseString = s3Client.GetObjectAsync(getObjectRequest).Result;

				if (responseString == null)
				{
					logHelper.LogInfo($"Bucket is empty. Exiting.");
					logHelper.LogInfo($"responseString: {responseString}");
					throw new Exception();
				}

				var tempPath = Path.GetTempFileName();
				using var responseStream = s3Client.GetObjectAsync(getObjectRequest).Result.ResponseStream;
				using Stream stream = File.Create(tempPath);
				logHelper.LogInfo($"Writing object to temp path");
				responseStream.CopyTo(stream);
				return tempPath;
			}
			catch (Exception exception)
			{
				logHelper.LogError($"Could not collect document from S3. Fatal error: {exception.Message}\nStack trace: {exception.StackTrace}");
				throw;
			}
		}

		/// <summary>
		/// Takes a created file and uploads to S3.
		/// </summary>
		/// <param name="localFilePath">The full local file path for the upload.</param>
		/// <param name="bucketName">The name of the bucket in S3 ,the bucket should be already created.</param>
		/// <param name="subDirectoryInBucket">The sub directory to upload to, if applicable.</param>
		/// <param name="fileNameInS3">The name the file is to be called within s3.</param>
		/// <returns>A true response if the file was successfully uploaded.</returns>
		public void SendFileToS3Bucket(string localFilePath, string bucketName, string fileNameInS3, string subDirectoryInBucket)
		{
			using IAmazonS3 s3Client = new AmazonS3Client(RegionEndpoint.EUWest1);
			// Create a TransferUtility instance passing it the IAmazonS3 created in the first step.
			TransferUtility utility = new(s3Client);
			// Making a TransferUtilityUploadRequest instance.
			TransferUtilityUploadRequest request = new();

			if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
			{
				// If no subdirectory just bucket name.
				request.BucketName = bucketName;
			}
			else
			{   // If sub subdirectory specified, subdirectory and bucket name.
				request.BucketName = bucketName + @"/" + subDirectoryInBucket;
			}
			logHelper.LogInfo($"Starting file transfer to s3.");
			request.Key = fileNameInS3; // File name for transfer to S3.
			request.FilePath = localFilePath; // Local file name and path.
			utility.Upload(request); // Upload transfer request to s3.
			logHelper.LogInfo("File transfer to s3 completed.");
		}

		/// <summary>
		/// Takes a created file and uploads to S3.
		/// </summary>
		/// <param name="bucketName">The name of the bucket in S3 ,the bucket should be already created.</param>
		/// <param name="subDirectoryInBucket">The sub directory to upload to, if applicable.</param>
		/// <param name="fileNameInS3">The name the file is to be called within s3.</param>
		/// <returns>A true response if the file was successfully uploaded.</returns>
		public void DeleteFileFromS3Bucket(string bucketName, string fileNameInS3, string subDirectoryInBucket)
		{
			using IAmazonS3 s3Client = new AmazonS3Client(RegionEndpoint.EUWest1);
			// Making a DeleteObjectRequest instance.
			DeleteObjectRequest request = new();

			if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
			{
				// If no subdirectory just bucket name.
				request.BucketName = bucketName;
			}
			else
			{   // If sub subdirectory specified, subdirectory and bucket name.
				request.BucketName = bucketName + @"/" + subDirectoryInBucket;
			}
			logHelper.LogInfo($"Starting deletion of file {fileNameInS3} from s3.");
			request.Key = fileNameInS3; // File name for transfer to S3.
			s3Client.DeleteObjectAsync(request);
			logHelper.LogInfo("File deletion from s3 completed.");
		}

		/// <summary>
		/// Save specified object to the specific S3 bucket and returns object key in bucket
		/// </summary>
		/// <param name="originBucketName">The name of the bucket to retrieve from.</param>
		/// <param name="defineObjectKey">The key of the item to save to the bucket.</param>
		/// <param name="stringMessage">The details of the item to save to the bucket</param>
		/// <returns>Object key saved in S3</returns>
		public string PutObjectIntoS3ReturnObjectKey(string originBucketName, string defineObjectKey, string stringMessage)
		{
			var client = new AmazonS3Client();
			try
			{
				var request = new PutObjectRequest
				{
					BucketName = originBucketName,
					Key = defineObjectKey,
					ContentType = "*/*",
					ContentBody = stringMessage
				};

				var responseString = client.PutObjectAsync(request).Result;

				if (responseString != null)
				{
					logHelper.LogInfo($"Object saved on S3. Path: {defineObjectKey}");
				}
				return defineObjectKey;
			}
			catch (Exception exception)
			{
				logHelper.LogError($"Could not put file into S3. Fatal error: {exception.Message}\nStack trace: {exception.StackTrace}");
				throw;
			}
		}
	}
}
