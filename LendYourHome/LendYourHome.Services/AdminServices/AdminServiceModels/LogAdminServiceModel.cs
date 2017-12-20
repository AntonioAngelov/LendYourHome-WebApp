namespace LendYourHome.Services.AdminServices.AdminServiceModels
{
    using System;
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using Data.Models.Enums;

    public class LogAdminServiceModel : IMapFrom<AdminLog>, IHaveCustomMapping
    {
        public string AdminName { get; set; }

        public AdminLogType LogType { get; set; }

        public string TargetedUserName { get; set; }

        public DateTime SubmitDate { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<AdminLog, LogAdminServiceModel>()
                .ForMember(lm => lm.AdminName, cfg => cfg.MapFrom(l => l.Admin.UserName));
        }
    }
}
