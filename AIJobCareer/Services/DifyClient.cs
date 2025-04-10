using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;
using AIJobCareer.Controllers;
using System.ComponentModel.DataAnnotations;

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

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType, string username)
        {
            var endpoint = $"{_baseUrl}/files/upload";

            using var formContent = new MultipartFormDataContent();
            StreamContent streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            formContent.Add(streamContent, "file", fileName);
            formContent.Add(new StringContent(username), "user");

            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, formContent);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Dictionary<string, object>>(responseContent);

            return result["id"].ToString();
        }

        public async Task<(bool success, SuggestionsResponse? data, string? errorMessage)> GetSuggestions(string message_id, string user_id)
        {
            var endpoint = $"{_baseUrl}/messages/{message_id}/suggested";

            if (!string.IsNullOrWhiteSpace(user_id))
            {
                endpoint += $"?user={Uri.EscapeDataString(user_id)}";
            }

            _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return (false, null, $"API Error (Status: {(int)response.StatusCode}): {errorContent}");
            }


            string? responseContent = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<SuggestionsResponse>(responseContent);

            return (data?.result == "success", data, null);
        }
    }

    public class DifyFileObject
    {
        [RegularExpression("^(document|image|audio|video|custom)$", ErrorMessage = "Type must be one of: document, image, audio, video, custom")]
        public string type { get; set; }
        
        [RegularExpression("^(remote_url|local_file)$", ErrorMessage = "Transfer method must be either remote_url or local_file")]
        public string transfer_method { get; set; }
        public string? url { get; set; }

        public string? upload_file_id { get; set; }
    }

    public class DifyInputObject
    {
        public string input;
        public DifyFileObject? file;
        public string query;

    }

    public class ChatRequest
    {
        public string query { get; set; }
        public Dictionary<string, string>? inputs { get; set; }
        public string? conversation_id { get; set; }
        public string user { get; set; }
        public List<DifyFileObject>? files { get; set; }
        public string response_mode { get; set; }
        public bool? auto_generate_name { get; set; }
    }

    public class SuggestionsResponse
    {
        public string result { get; set; }
        public List<string> data { get; set; }
    }
}
