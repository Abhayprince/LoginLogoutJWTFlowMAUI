using LoginLogoutJWTFlowMAUI.Services;

namespace LoginLogoutJWTFlowMAUI.Pages;

public partial class ApplicationDetailsPage : ContentPage
{
    private readonly ApplicationDetailsService _applicationDetailsService;
    private readonly IAuthService _authService;

    public ApplicationDetailsPage(ApplicationDetailsService applicationDetailsService, IAuthService authService)
	{
		InitializeComponent();
        _applicationDetailsService = applicationDetailsService;
        _authService = authService;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var applicationDetails = await _applicationDetailsService.GetApplicationDetails();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        _authService.Logout();
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }
}