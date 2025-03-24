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

    public class R2FileService : IR2FileService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;
        private readonly ILogger<R2FileService> _logger;

        public R2FileService(IConfiguration configuration, ILogger<R2FileService> logger)
        {
            _logger = logger;

            var accessKey = Environment.GetEnvironmentVariable("CLOUDFLARE_R2_ACCESS_KEY") ?? configuration["Cloudflare:R2:AccessKey"];
            var secretKey = Environment.GetEnvironmentVariable("CLOUDFLARE_R2_SECRET_KEY") ?? configuration["Cloudflare:R2:SecretKey"];
            var accountId = Environment.GetEnvironmentVariable("CLOUDFLARE_R2_ACCOUNT_ID") ?? configuration["Cloudflare:R2:AccountId"];
            _bucketName = Environment.GetEnvironmentVariable("CLOUDFLARE_R2_BUCKET_NAME") ?? configuration["Cloudflare:R2:BucketName"];

            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var config = new AmazonS3Config
            {
                ServiceURL = $"https://{accountId}.r2.cloudflarestorage.com",
                ForcePathStyle = true,
                RequestChecksumCalculation = RequestChecksumCalculation.WHEN_REQUIRED,
                ResponseChecksumValidation = ResponseChecksumValidation.WHEN_REQUIRED
            };

            _s3Client = new AmazonS3Client(credentials, config);
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folderName = "")
        {
            try
            {
                // Generate a unique file name (you might want to implement your own naming strategy)
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                _logger.LogInformation(fileName);

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
                        BucketName = "demo",
                        Key = fileKey,

                        ContentType = file.ContentType,
                        DisablePayloadSigning = true
                    };

                    await transferUtility.UploadAsync(uploadRequest);
                }

                _logger.LogInformation($"File {fileName} uploaded successfully to {fileKey}");

                return fileKey;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error uploading file: {ex.Message}");
                throw;
            }
        }

        public async Task<byte[]> RetrieveFileAsync(string fileKey)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving file {fileKey}: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteFileAsync(string fileKey)
        {
            try
            {
                var request = new DeleteObjectRequest
                {
                    BucketName = _bucketName,
                    Key = fileKey
                };

                await _s3Client.DeleteObjectAsync(request);
                _logger.LogInformation($"File {fileKey} deleted successfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting file {fileKey}: {ex.Message}");
                throw;
            }
        }
    }
}
