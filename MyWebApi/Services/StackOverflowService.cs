using System;
using System.Net.Http;
using System.Threading.Tasks;
using MyWebApi.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace MyWebApi.Services
{
    public class StackOverflowService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<StackOverflowService> _logger;

        public StackOverflowService(HttpClient httpClient, ILogger<StackOverflowService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<StackOverflowResponse> GetRecentAnswersAsync()
        {
            var requestUrl = "https://api.stackexchange.com/2.3/answers?order=desc&sort=activity&site=stackoverflow";
            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error fetching data from Stack Overflow API");
            }

            var content = await response.Content.ReadAsStringAsync();
            var answers = JsonConvert.DeserializeObject<StackOverflowResponse>(content);

            foreach (var item in answers.Items)
            {
            // Example: Set a default value for missing display names
            
            item.Owner.DisplayName = "Tapan";

            // Example: Calculate and set a new property
            item.Score *= 2; // Just an example of manipulation
            }

            return answers;

            _logger.LogInformation("Raw JSON response: {JsonResponse}", content);

            return JsonConvert.DeserializeObject<StackOverflowResponse>(content);
        }
    }
}
