using LoginLogoutJWTFlowMAUI.Services;

namespace LoginLogoutJWTFlowMAUI.Pages;

public partial class LoginPage : ContentPage
{
    private readonly IAuthService _authService;

    public LoginPage(IAuthService authService)
	{
		InitializeComponent();
        _authService = authService;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var error = await _authService.LoginAsync(new Shared.Models.LoginRequestDto("Username", "password"));
        if (string.IsNullOrWhiteSpace(error))
        {
            await Shell.Current.GoToAsync($"//{nameof(ApplicationDetailsPage)}");
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", error, "Ok");
        }
    }
}