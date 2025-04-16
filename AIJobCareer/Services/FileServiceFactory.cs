namespace AIJobCareer.Services
{
    /// <summary>
    /// Factory to create appropriate file service based on configuration
    /// </summary>
    public class FileServiceFactory
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IServiceProvider _serviceProvider;

        public FileServiceFactory(
            IConfiguration configuration,
            ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Creates the appropriate file service based on configuration
        /// </summary>
        public IFileService CreateFileService()
        {
            // Get storage type from configuration with fallback to R2
            var storageType = _configuration["Storage:Type"]?.ToLowerInvariant() switch
            {
                "local" => StorageType.LocalFileSystem,
                _ => StorageType.CloudflareR2,
            };

            // Create appropriate service based on storage type
            return storageType switch
            {
                StorageType.LocalFileSystem => new LocalFileService(
                    _configuration,
                    _loggerFactory.CreateLogger<LocalFileService>()),
                _ => new R2FileService(
                    _configuration,
                    _loggerFactory.CreateLogger<R2FileService>()),
            };
        }

        /// <summary>
        /// Creates a file service with explicit type override
        /// </summary>
        public IFileService CreateFileService(StorageType storageType)
        {
            return storageType switch
            {
                StorageType.LocalFileSystem => new LocalFileService(
                    _configuration,
                    _loggerFactory.CreateLogger<LocalFileService>()),
                _ => new R2FileService(
                    _configuration,
                    _loggerFactory.CreateLogger<R2FileService>())
            };
        }
    }
}
