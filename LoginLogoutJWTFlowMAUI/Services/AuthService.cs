using LoginLogoutJWTFlowMAUI.Shared.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LoginLogoutJWTFlowMAUI.Services
{
    public interface IAuthService
    {
        Task<bool> IsUserAuthenticated();
        Task<string?> LoginAsync(LoginRequestDto dto);
        Task<AuthResponseDto?> GetAuthenticatedUserAsync();
        Task<HttpClient> GetAuthenticatedHttpClientAsync();
        void Logout();
    }

    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> IsUserAuthenticated()
        {
            var serializedData = await SecureStorage.Default.GetAsync(AppConstants.AuthStorageKeyName);
            return !string.IsNullOrWhiteSpace(serializedData);
        }

        public async Task<AuthResponseDto?> GetAuthenticatedUserAsync()
        {
            var serializedData = await SecureStorage.Default.GetAsync(AppConstants.AuthStorageKeyName);
            if(!string.IsNullOrWhiteSpace(serializedData))
            {
                return JsonSerializer.Deserialize<AuthResponseDto>(serializedData);
            }
            return null;
        }

        public async Task<string?> LoginAsync(LoginRequestDto dto)
        {
            var httpClient = _httpClientFactory.CreateClient(AppConstants.HttpClientName);

            var response = await httpClient.PostAsJsonAsync<LoginRequestDto>("api/auth/login", dto);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ApiResponse<AuthResponseDto> authResponse =
                    JsonSerializer.Deserialize<ApiResponse<AuthResponseDto>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                if (authResponse.Status)
                {
                    var serializedData = JsonSerializer.Serialize(authResponse.Data);
                    await SecureStorage.Default.SetAsync(AppConstants.AuthStorageKeyName, serializedData);
                }
                else
                {
                    return authResponse.Errors.FirstOrDefault();
                }
            }
            else
            {
                return "Error in logging in";
            }
            return null;
        }

        public void Logout() => SecureStorage.Default.Remove(AppConstants.AuthStorageKeyName);

        public async Task<HttpClient> GetAuthenticatedHttpClientAsync()
        {
            var httpClient = _httpClientFactory.CreateClient(AppConstants.HttpClientName);

            var authenticatedUser = await GetAuthenticatedUserAsync();

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authenticatedUser.Token);

            return httpClient;
        }
    }
}
