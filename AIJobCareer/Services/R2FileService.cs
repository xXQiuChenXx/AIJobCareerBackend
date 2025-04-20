using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace AIJobCareer.Services
{
    public interface IR2FileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName = "");
        Task<byte[]> RetrieveFileAsync(string fileKey);
        Task<bool> DeleteFileAsync(string fileKey);
    }

    /// <summary>
    /// Cloudflare R2 implementation of BaseFileService
    /// </summary>
    public class R2FileService : BaseFileService, IR2FileService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;
        private readonly string _cdnBaseUrl;

        public override StorageType StorageType => StorageType.CloudflareR2;

        public R2FileService(IConfiguration configuration, ILogger<R2FileService> logger)
            : base(configuration, logger)
        {
            var accessKey = Environment.GetEnvironmentVariable("CLOUDFLARE_R2_ACCESS_KEY") ?? configuration["Cloudflare:R2:AccessKey"];
            var secretKey = Environment.GetEnvironmentVariable("CLOUDFLARE_R2_SECRET_KEY") ?? configuration["Cloudflare:R2:SecretKey"];
            var accountId = Environment.GetEnvironmentVariable("CLOUDFLARE_R2_ACCOUNT_ID") ?? configuration["Cloudflare:R2:AccountId"];
            _bucketName = Environment.GetEnvironmentVariable("CLOUDFLARE_R2_BUCKET_NAME") ?? configuration["Cloudflare:R2:BucketName"];
            _cdnBaseUrl = Environment.GetEnvironmentVariable("CLOUDFLARE_R2_CDN_URL") ?? configuration["Cloudflare:R2:CdnUrl"];

            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var config = new AmazonS3Config
            {
                ServiceURL = $"https://{accountId}.r2.cloudflarestorage.com",
                ForcePathStyle = true,
                RequestChecksumCalculation = Amazon.Runtime.RequestChecksumCalculation.WHEN_REQUIRED,
                ResponseChecksumValidation = Amazon.Runtime.ResponseChecksumValidation.WHEN_REQUIRED
            };

            _s3Client = new AmazonS3Client(credentials, config);
        }

        // Implementation for IR2FileService interface to maintain compatibility
        public async Task<string> UploadFileAsync(IFormFile file, string folderName = "")
        {
            return await base.UploadFileAsync(file, folderName);
        }

        public async Task<byte[]> RetrieveFileAsync(string fileKey)
        {
            return await base.RetrieveFileAsync(fileKey);
        }

        public async Task<bool> DeleteFileAsync(string fileKey)
        {
            return await base.DeleteFileAsync(fileKey);
        }

        // Override the protected methods from the base class
        protected override async Task<string> ProcessUploadAsync(IFormFile file, string fileName, string folderName)
        {
            // Create the full path including folder if provided
            var fileKey = string.IsNullOrEmpty(folderName)
                ? fileName
                : $"{folderName.TrimEnd('/')}/{fileName}";

            using (var fileStream = file.OpenReadStream())
            {
                var transferUtility = new TransferUtility(_s3Client);
                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = fileStream,
                    BucketName = _bucketName,
                    Key = fileKey,
                    ContentType = file.ContentType,
                    DisablePayloadSigning = true
                };

                await transferUtility.UploadAsync(uploadRequest);
            }

            return fileKey;
        }

        protected override async Task<byte[]> ProcessRetrievalAsync(string fileKey)
        {
            var request = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = fileKey
            };

            using (var response = await _s3Client.GetObjectAsync(request))
            {
                using (var responseStream = response.ResponseStream)
                using (var memoryStream = new MemoryStream())
                {
                    await responseStream.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

        protected override async Task<bool> ProcessDeletionAsync(string fileKey)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = fileKey
            };

            await _s3Client.DeleteObjectAsync(request);
            return true;
        }

        protected override string GenerateUrl(string fileKey)
        {
            if (string.IsNullOrEmpty(_cdnBaseUrl))
            {
                // Direct S3 URL if no CDN configured
                return $"https://{_bucketName}.r2.cloudflarestorage.com/{fileKey}";
            }

            return $"{_cdnBaseUrl.TrimEnd('/')}/{fileKey}";
        }
    }
}
