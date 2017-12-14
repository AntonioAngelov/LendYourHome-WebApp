namespace LendYourHome.Application.Models.UsersViewModels
{
    using Services.ServiceModels.Users;

    public class UserEditViewModel
    {
        public UserEditServiceModel FormDataModel { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}
