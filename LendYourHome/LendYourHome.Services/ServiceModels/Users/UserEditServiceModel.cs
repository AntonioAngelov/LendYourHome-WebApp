namespace LendYourHome.Services.ServiceModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;
    using Common.Mapping;
    using Data.Models;
    using static Common.Constants.DataConstants;

    public class UserEditServiceModel : IMapFrom<User>
    {
        [Required]
        [MaxLength(UserNameMaxLength)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        [MaxLength(AdditionalInfoMaxLength)]
        [Display(Name = "Additional Info")]
        public string AdditionalInformation { get; set; }
    }
}
