using LoginLogoutJWTFlowMAUI.Services;

namespace LoginLogoutJWTFlowMAUI.Pages;

public partial class UsersPage : ContentPage
{
    private readonly UserService _userService;

    public UsersPage(UserService userService)
	{
		InitializeComponent();
        _userService = userService;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var users = await _userService.GetUsersAsync();
    }
}