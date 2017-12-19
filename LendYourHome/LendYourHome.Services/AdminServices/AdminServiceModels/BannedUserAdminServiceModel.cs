namespace LendYourHome.Services.AdminServices.AdminServiceModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common.Mapping;
    using Data.Models;

    public class BannedUserAdminServiceModel : UserBaseAdminServiceModel, IMapFrom<User>
    {
        [Display(Name = "Banned Till")]
        public DateTime? BanEndDate { get; set; }
    }
}
