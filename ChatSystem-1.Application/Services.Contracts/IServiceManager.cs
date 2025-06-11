namespace ChatSystem_1.Application.Services.Contracts
{

    public interface IServiceManager
    {
        IAuthenticationService AuthenticationService { get; }
        IUserProfileService UserProfileService { get; }
        IPostService PostService { get; }
    }
}
