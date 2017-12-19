namespace LendYourHome.Services
{
    using System;
    using System.Collections.Generic;
    using ServiceModels.Users;

    public interface IUserService 
    {
        string GetUserProfilePicture(string userId);

        bool Exists(string id);

        UserDetailsServiceModel Details(string id);

        void AddInRole(string userId, string role);

        IEnumerable<UserForReviewServiceModel> WaitingForReview(string hostId);

        string GetName(string userId);

        IEnumerable<TopGuestServiceModel> TopSixGuestsByAverageRating();

        bool UsernameIsTaken(string userId, string userName);

        UserEditServiceModel GetForEdit(string userId);

        void Edit(string userId,
            string userName,
            string additionaInfo,
            string address,
            string profilePictureUrl);

        bool IsFreeEmail(string email);

        DateTime GetBanEndDate(string userName);
    }
}
