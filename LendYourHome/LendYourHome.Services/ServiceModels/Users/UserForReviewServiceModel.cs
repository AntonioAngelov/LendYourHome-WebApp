namespace LendYourHome.Services.ServiceModels.Users
{
    using Common.Mapping;
    using Data.Models;

    public class UserForReviewServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string UserName { get; set; }
    }
}
