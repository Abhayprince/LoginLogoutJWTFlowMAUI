namespace LoginLogoutJWTFlowMAUI.Shared.Models
{
    public readonly record struct LoggedInUser(Guid Id, string Name, string Role, string Email);
}
