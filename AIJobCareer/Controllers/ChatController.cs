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
            _difyClient = new DifyClient(
                _configuration["Dify:ApiKey"],
                logger,
                _configuration["Dify:BaseUrl"]
            );
        }
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(Environment.GetEnvironmentVariable("TESTING") + "demo");
        }

        [HttpPost("message")]
        public async Task StreamChatMessage([FromBody] ChatRequest request)
        {
            Response.Headers.Append("Content-Type", "text/event-stream");
            Response.Headers.Append("Cache-Control", "no-cache");
            Response.Headers.Append("Connection", "keep-alive");

            ChatRequest difyRequest = new ChatRequest
            {
                query = request.query,
                conversation_id = request.conversation_id,
                user = request.user,
                fileId = request.fileId,
                auto_generate_name = true,
                response_mode = "streaming",
                inputs = request.inputs
            };

            var (success, stream, errorMessage) = await _difyClient.StreamChatMessageAsync(difyRequest);
            if (!success || stream == null)
            {
                _logger.LogError("Error from Dify API: {ErrorMessage}", errorMessage);
                // Send error as SSE event
                var errorEvent = new { error = errorMessage ?? "Unknown error occurred" };
                await Response.WriteAsync($"event: error\ndata: {JsonSerializer.Serialize(errorEvent)}\n\n");
                await Response.Body.FlushAsync();
                return;
            }
            await StreamCopyOperation.CopyToAsync(stream, Response.Body, null, CancellationToken.None);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            using var stream = file.OpenReadStream();
            var fileId = await _difyClient.UploadFileAsync(stream, file.FileName, file.ContentType);
            return Ok(new { fileId });
        }

        [HttpGet("conversation/{conversationId}")]
        public async Task<IActionResult> GetConversation(string conversationId, [FromQuery] int firstId = 0, [FromQuery] int limit = 20)
        {
            var result = await _difyClient.GetConversationMessagesAsync(conversationId, firstId, limit);
            return Ok(result);
        }
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
