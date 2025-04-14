using AIJobCareer.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AIJobCareer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly DifyClient _difyClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ChatController> _logger;

        public ChatController(IConfiguration configuration, ILogger<ChatController> logger)
        {
            _logger = logger;
            _configuration = configuration;

            // Fix for CS8604: Ensure the environment variable or configuration value is not null
            var apiKey = Environment.GetEnvironmentVariable("DIFY_API_KEY") ?? _configuration["Dify:ApiKey"];
            var baseUrl = Environment.GetEnvironmentVariable("DIFY_BASE_URL") ?? _configuration["Dify:BaseUrl"];

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey), "API key cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new ArgumentNullException(nameof(baseUrl), "Base URL cannot be null or empty.");
            }

            _difyClient = new DifyClient(apiKey, logger, baseUrl);
        }

        // Other methods remain unchanged
    }


    public static class StreamUtils
    {
        public static async Task CopyToWithBufferAsync(Stream source, Stream destination)
        {
            var buffer = new byte[4096];
            int bytesRead;

            while ((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await destination.WriteAsync(buffer, 0, bytesRead);
                await destination.FlushAsync();
            }
        }
    }
}
