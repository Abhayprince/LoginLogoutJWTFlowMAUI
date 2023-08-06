using LoginLogoutJWTFlowMAUI.Pages;
using LoginLogoutJWTFlowMAUI.Services;
using Microsoft.Extensions.Logging;

namespace LoginLogoutJWTFlowMAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddCustomApiHttpClient();

        builder.Services.AddSingleton<IAuthService, AuthService>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<ApplicationDetailsService>();
        builder.Services.AddTransient<ApplicationDetailsPage>();
        builder.Services.AddTransient<UserService>();
        builder.Services.AddTransient<UsersPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
public static class MauiProgramExtensions
{
	public static IServiceCollection AddCustomApiHttpClient(this IServiceCollection services)
	{
        services.AddSingleton<IPlatformHttpMessageHandler>(_ =>
        {
#if ANDROID
            return new AndroidHttpMessageHandler();
#elif IOS
            return new IosHttpMessageHandler();
#endif
			return null;
        });

        services.AddHttpClient(AppConstants.HttpClientName, httpClient =>
        {
            var baseAddress =
                    DeviceInfo.Platform == DevicePlatform.Android
                        ? "https://10.0.2.2:7157"
                        : "https://localhost:7157";

            httpClient.BaseAddress = new Uri(baseAddress);
        })
            .ConfigureHttpMessageHandlerBuilder(builder =>
            {
                var platfromHttpMessageHandler = builder.Services.GetRequiredService<IPlatformHttpMessageHandler>();
                builder.PrimaryHandler = platfromHttpMessageHandler.GetHttpMessageHandler();
            });

        return services;
    }
}
