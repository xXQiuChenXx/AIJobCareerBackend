using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Services
{
    public interface IDifyService
    {
        Task<(bool success, Stream? stream, string? errorMessage)> StreamChatMessageAsync(ChatRequest request);
        Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType, string username);
        Task<(bool success, SuggestionsResponse? data, string? errorMessage)> GetSuggestions(string message_id, string user_id);
        Task<(bool success, string? data, string? errorMessage)> GetHistory(string conversationId, string user, string firstId = null, int limit = 20);
    }

    public class DifyService : IDifyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<DifyService> _logger;

        public DifyService(IConfiguration configuration, ILogger<DifyService> logger)
        {
            var apiKey = Environment.GetEnvironmentVariable("DIFY_API_KEY") ??
                         configuration["Dify:ApiKey"] ??
                         throw new ArgumentNullException("Dify API Key is missing from configuration");

            _baseUrl = Environment.GetEnvironmentVariable("DIFY_BASE_URL") ??
                      configuration["Dify:BaseUrl"] ??
                      "https://api.dify.ai/v1";

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            _logger = logger;
        }

        public async Task<(bool success, Stream? stream, string? errorMessage)> StreamChatMessageAsync(ChatRequest request)
        {
            string endpoint = $"{_baseUrl}/chat-messages";

            _logger.LogInformation("Sending chat request: {Request}", JsonSerializer.Serialize(request));

            StringContent content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Dify API error: {Status} - {ErrorContent}", (int)response.StatusCode, errorContent);
                return (false, null, $"API Error (Status: {(int)response.StatusCode}): {errorContent}");
            }

            return (true, await response.Content.ReadAsStreamAsync(), null);
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType, string username)
        {
            var endpoint = $"{_baseUrl}/files/upload";

            _logger.LogInformation("Uploading file to Dify: {FileName}, {ContentType}", fileName, contentType);

            using var formContent = new MultipartFormDataContent();
            StreamContent streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            formContent.Add(streamContent, "file", fileName);
            formContent.Add(new StringContent(username), "user");

            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, formContent);

            try
            {
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<Dictionary<string, object>>(responseContent);
                return result["id"].ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload file to Dify: {ErrorMessage}", ex.Message);
                throw new ApplicationException($"Failed to upload file to Dify: {ex.Message}", ex);
            }
        }

        public async Task<(bool success, SuggestionsResponse? data, string? errorMessage)> GetSuggestions(string message_id, string user_id)
        {
            var endpoint = $"{_baseUrl}/messages/{message_id}/suggested";

            if (!string.IsNullOrWhiteSpace(user_id))
            {
                endpoint += $"?user={Uri.EscapeDataString(user_id)}";
            }

            _logger.LogInformation("Getting suggestions for message: {MessageId}, user: {UserId}", message_id, user_id);

            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Failed to get suggestions: {Status} - {ErrorContent}", (int)response.StatusCode, errorContent);
                return (false, null, $"API Error (Status: {(int)response.StatusCode}): {errorContent}");
            }

            string? responseContent = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<SuggestionsResponse>(responseContent);

            return (data?.result == "success", data, null);
        }

        public async Task<(bool success, string? data, string? errorMessage)> GetHistory(string conversationId, string user, string firstId = null, int limit = 20)
        {
            try
            {
                // Build the request URL with query parameters
                var requestUrl = $"{_baseUrl}/messages?conversation_id={conversationId}&user={user}";

                if (!string.IsNullOrEmpty(firstId))
                {
                    requestUrl += $"&first_id={firstId}";
                }

                requestUrl += $"&limit={limit}";

                _logger.LogInformation("Getting conversation history: {ConversationId}, user: {User}", conversationId, user);

                // Make the request to Dify API
                var response = await _httpClient.GetAsync(requestUrl);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string content = await response.Content.ReadAsStringAsync();
                    return (true, content, null);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Failed to get history: {Status} - {ErrorContent}", (int)response.StatusCode, errorContent);
                    return (false, null, $"Failed to retrieve conversation history. Status code: {response.StatusCode}, Error: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting conversation history: {ErrorMessage}", ex.Message);
                return (false, null, $"An error occurred: {ex.Message}");
            }
        }
    }

    // Keeping the original model classes
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
        public string input { get; set; }
        public DifyFileObject? file { get; set; }
        public string query { get; set; }
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