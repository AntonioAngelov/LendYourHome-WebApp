﻿namespace LendYourHome.Services.AdminServices.AdminServiceModels
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class ActiveUserAdminServiceModel :  UserBaseAdminServiceModel, IMapFrom<User>, IHaveCustomMapping
    {
        [Display(Name = "Is Host")]
        public string IsHost { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<User, ActiveUserAdminServiceModel>()
                .ForMember(ua => ua.IsHost, cfg => cfg.MapFrom(u => u.Home != null ? "Yes" : "No"));
        }
    }
}
