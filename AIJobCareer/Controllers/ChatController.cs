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
        public async Task<IActionResult> UploadFile(IFormFile file, string user_id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using var stream = file.OpenReadStream();
            var fileId = await _difyClient.UploadFileAsync(stream, file.FileName, file.ContentType, user_id);
            return Ok(new { fileId });
        }

        /// <summary>
        /// Gets suggestions based on message ID and username
        /// </summary>
        /// <param name="message_id">The ID of the message</param>
        /// <param name="user_id">The username to filter suggestions</param>
        /// <returns>A list of suggestions</returns>
        [HttpGet("suggestions/{message_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSuggestions(string message_id, string user_id)
        {
            if (string.IsNullOrWhiteSpace(message_id))
            {
                _logger.LogWarning("Invalid message ID provided");
                return BadRequest("Message ID is required");
            }

            var (success, suggestions, errorMessage) = await _difyClient.GetSuggestions(message_id, user_id);
            if (!success)
            {
                _logger.LogError("Error from Dify API: {ErrorMessage}", errorMessage);
                return NotFound(errorMessage);
            }


            return Ok(suggestions);
        }



        [HttpGet("conversation_history")]
        public async Task<IActionResult> GetConversationHistory(
            [FromQuery] string conversationId,
            [FromQuery] string user,
            [FromQuery] string firstId = null,
            [FromQuery] int limit = 20)
        {
            var (success, data, errorMessage) = await _difyClient.GetHistory(conversationId, user, firstId, limit);
            if (!success || string.IsNullOrEmpty(data))
            {
                _logger.LogError("Error from Dify API: {ErrorMessage}", errorMessage);
                return BadRequest(errorMessage);
            }

            return Content(data, "application/json");
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
