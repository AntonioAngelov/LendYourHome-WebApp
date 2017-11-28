namespace LendYourHome.Services
{
    using ServiceModels.Users;

    public interface IUserService 
    {
        string GetUserProfilePicture(string userId);

        bool Exists(string id);

        UserDetailsServiceModel Details(string id);

        void AddInRole(string userId, string role);
    }
}
