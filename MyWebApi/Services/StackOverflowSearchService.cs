using System;
using System.Net.Http;
using System.Threading.Tasks;
using MyWebApi.SearchModel;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace MyWebApi.Services
{
    public class StackOverflowSearchService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<StackOverflowSearchService> _logger;

        public StackOverflowSearchService(HttpClient httpClient, ILogger<StackOverflowSearchService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<StackOverflowSearchResponse> GetRelevantSearchAnswers(string query, int page)
        {
            var requestUrl = $"https://api.stackexchange.com/2.3/similar?page={page}&pagesize=10&order=desc&sort=relevance&title={Uri.EscapeDataString(query)}&site=stackoverflow&filter=!6WPIomnMOOD*e";
            // var requestUrl = $"https://api.stackexchange.com/2.3/search?order=desc&sort=activity&intitle={Uri.EscapeDataString(query)}&site=stackoverflow";

            try
            {
                _logger.LogInformation($"Fetching data from {requestUrl}");
                var response = await _httpClient.GetAsync(requestUrl);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Error fetching data. Status code: {response.StatusCode}");
                    throw new Exception("Error fetching data");
                }

                var content = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Response content: {content}");

                var answers = JsonConvert.DeserializeObject<StackOverflowSearchResponse>(content);
                if (answers == null)
                {
                    _logger.LogError("Deserialized response is null");
                    throw new Exception("Error deserializing data");
                }

                return answers;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request exception: {ex.Message}");
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError($"JSON exception: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"General exception: {ex.Message}");
                throw;
            }
        }
    }
}
