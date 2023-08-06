using LoginLogoutJWTFlowMAUI.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LoginLogoutJWTFlowMAUI.Services
{
    public class ApplicationDetailsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApplicationDetailsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApplicationDetails?> GetApplicationDetails()
        {
            var httpClient = _httpClientFactory.CreateClient(AppConstants.HttpClientName);

            var response = await httpClient.GetAsync("api/Application");
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<ApplicationDetails>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return apiResponse.Data;
            }
            return null;
        }
    }
}
