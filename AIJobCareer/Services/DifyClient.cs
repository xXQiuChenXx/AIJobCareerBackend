using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;
using AIJobCareer.Controllers;

namespace AIJobCareer.Services
{
    public class DifyClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly ILogger<ChatController> _logger;

        public DifyClient(string apiKey, ILogger<ChatController> logger, string baseUrl = "https://api.dify.ai/v1")
        {
            _apiKey = apiKey;
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
            _logger = logger;
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        public async Task<(bool success, Stream? stream, string? errorMessage)> StreamChatMessageAsync(ChatRequest request)
        {
            string endpoint = $"{_baseUrl}/chat-messages";

            // Dictionary<string, object> requestData = new Dictionary<string, object>
            //{
            //    { "inputs", new {} },
            //    { "query", query },
            //    { "response_mode", "streaming" },
            //    { "user", user },
            //    { "conversation_id", conversationId },
            //    { "auto_generate_name", true }
            //};


            // Add file if provided
            if (request.fileId != null)
            {
                var file = new Dictionary<string, object>
                   {
                       { "upload_file_id", request.fileId },
                       { "type", "document" },
                       { "transfer_method", "local_file" }
                   };
                request.files = new List<Dictionary<string, object>> { file };
            }
            _logger.LogInformation(JsonSerializer.Serialize(request));

            StringContent content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return (false, null, $"API Error (Status: {(int)response.StatusCode}): {errorContent}");
            }

            return (true, await response.Content.ReadAsStreamAsync(), null);
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
        {
            var endpoint = $"{_baseUrl}/files/upload";

            using var formContent = new MultipartFormDataContent();
            StreamContent streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            formContent.Add(streamContent, "file", fileName);

            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, formContent);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Dictionary<string, object>>(responseContent);

            return result["id"].ToString();
        }

        public async Task<Dictionary<string, object>> GetConversationMessagesAsync(string conversationId, int firstId = 0, int limit = 20)
        {
            var endpoint = $"{_baseUrl}/messages?conversation_id={conversationId}&first_id={firstId}&limit={limit}";

            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Dictionary<string, object>>(responseContent);
        }
    }

    public class ChatRequest
    {
        public string query { get; set; }
        public Dictionary<string, string>? inputs { get; set; }
        public string? fileId { get; set; }
        public string? conversation_id { get; set; }
        public string user { get; set; }
        public List<Dictionary<string, object>>? files { get; set; }
        public string response_mode { get; set; }
        public bool? auto_generate_name { get; set; }
    }
}
