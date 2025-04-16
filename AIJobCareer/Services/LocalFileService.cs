namespace AIJobCareer.Services
{
    /// <summary>
    /// Local file system implementation of BaseFileService
    /// </summary>
    public class LocalFileService : BaseFileService
    {
        private readonly string _baseUploadPath;
        private readonly string _baseUrl;

        public override StorageType StorageType => StorageType.LocalFileSystem;

        public LocalFileService(IConfiguration configuration, ILogger<LocalFileService> logger)
            : base(configuration, logger)
        {
            _baseUploadPath = configuration["Storage:Local:UploadPath"] ?? "wwwroot/uploads";
            _baseUrl = configuration["Storage:Local:BaseUrl"] ?? "/uploads";

            // Ensure upload directory exists
            if (!Directory.Exists(_baseUploadPath))
            {
                Directory.CreateDirectory(_baseUploadPath);
            }
        }

        // Override the virtual methods from the base class with proper implementation
        protected override async Task<string> ProcessUploadAsync(IFormFile file, string fileName, string folderName)
        {
            // Create folder path
            var folderPath = string.IsNullOrEmpty(folderName)
                ? _baseUploadPath
                : Path.Combine(_baseUploadPath, folderName);

            // Ensure folder exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Create file path
            var filePath = Path.Combine(folderPath, fileName);

            // Create the relative key for storage and retrieval
            var fileKey = string.IsNullOrEmpty(folderName)
                ? fileName
                : $"{folderName.TrimEnd('/')}/{fileName}";

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileKey;
        }

        protected override async Task<byte[]> ProcessRetrievalAsync(string fileKey)
        {
            var filePath = Path.Combine(_baseUploadPath, fileKey.Replace('/', Path.DirectorySeparatorChar));

            if (!File.Exists(filePath))
            {
                _logger.LogWarning($"File not found: {filePath}");
                throw new FileNotFoundException($"File not found: {fileKey}");
            }

            return await File.ReadAllBytesAsync(filePath);
        }

        protected override Task<bool> ProcessDeletionAsync(string fileKey)
        {
            var filePath = Path.Combine(_baseUploadPath, fileKey.Replace('/', Path.DirectorySeparatorChar));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return Task.FromResult(true);
            }

            _logger.LogWarning($"File {fileKey} not found for deletion");
            return Task.FromResult(false);
        }

        protected override string GenerateUrl(string fileKey)
        {
            return $"{_baseUrl}/{fileKey}";
        }
    }

}
