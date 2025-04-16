namespace AIJobCareer.Services
{
    /// <summary>
    /// Interface for file services
    /// </summary>
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName = "");
        Task<byte[]> RetrieveFileAsync(string fileKey);
        Task<bool> DeleteFileAsync(string fileKey);
        string GetFileUrl(string fileKey);
        StorageType StorageType { get; }
    }

    /// <summary>
    /// Enum to represent different storage providers
    /// </summary>
    public enum StorageType
    {
        CloudflareR2,
        LocalFileSystem
    }

    /// <summary>
    /// Abstract base class for file services with virtual methods
    /// </summary>
    public abstract class BaseFileService : IFileService
    {
        protected readonly ILogger _logger;
        protected readonly IConfiguration _configuration;

        public abstract StorageType StorageType { get; }

        protected BaseFileService(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        // Virtual methods for polymorphism using override
        public virtual async Task<string> UploadFileAsync(IFormFile file, string folderName = "")
        {
            // Base implementation for logging and common operations
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            _logger.LogInformation($"Beginning upload for file: {fileName} in folder: {folderName}");

            try
            {
                var result = await ProcessUploadAsync(file, fileName, folderName);
                _logger.LogInformation($"File {fileName} uploaded successfully");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error uploading file: {ex.Message}");
                throw;
            }
        }

        public virtual async Task<byte[]> RetrieveFileAsync(string fileKey)
        {
            _logger.LogInformation($"Retrieving file: {fileKey}");
            try
            {
                return await ProcessRetrievalAsync(fileKey);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving file {fileKey}: {ex.Message}");
                throw;
            }
        }

        public virtual async Task<bool> DeleteFileAsync(string fileKey)
        {
            _logger.LogInformation($"Deleting file: {fileKey}");
            try
            {
                return await ProcessDeletionAsync(fileKey);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting file {fileKey}: {ex.Message}");
                throw;
            }
        }

        public virtual string GetFileUrl(string fileKey)
        {
            return GenerateUrl(fileKey);
        }

        // Protected abstract methods that must be implemented by derived classes
        protected abstract Task<string> ProcessUploadAsync(IFormFile file, string fileName, string folderName);
        protected abstract Task<byte[]> ProcessRetrievalAsync(string fileKey);
        protected abstract Task<bool> ProcessDeletionAsync(string fileKey);
        protected abstract string GenerateUrl(string fileKey);
    }
}
