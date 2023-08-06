using LoginLogoutJWTFlowMAUI.Shared.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace LoginLogoutJWTFlowMAUI.Services
{
    public class UserService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAuthService _authService;

        public UserService(IHttpClientFactory httpClientFactory, IAuthService authService)
        {
            _httpClientFactory = httpClientFactory;
            _authService = authService;
        }

        public async Task<IEnumerable<UserDto>?> GetUsersAsync()
        {
            var httpClient = await _authService.GetAuthenticatedHttpClientAsync();

            var response = await httpClient.GetAsync("api/users");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<IEnumerable<UserDto>>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return apiResponse.Data;
            }
            else
            {
                var statusCode = response.StatusCode;
            }
            return null;
        }
    }
}
