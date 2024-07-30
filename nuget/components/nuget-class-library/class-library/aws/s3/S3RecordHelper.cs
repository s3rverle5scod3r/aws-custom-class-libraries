using Amazon.Lambda.S3Events;
using CsvHelper;
using CsvHelper.Configuration;
using nuget_class_library.class_library.aws.lambda;
using System.Globalization;

namespace nuget_class_library.class_library.aws.s3
{
    public class S3RecordHelper : LambdaBase
    {
        public bool S3EventCheckForNull(S3Event s3Event)
        {
            if (s3Event == null)
			{
				logHelper.LogError($"s3Event cannot be null; execution cannot continue.");
				return true;
			}
			if (s3Event.Records == null || s3Event.Records.Count == 0)
			{
				logHelper.LogError($"s3Event cannot be null or contain zero records; execution cannot continue.");
				return true;
			}

            return false;
        }
	}
}

