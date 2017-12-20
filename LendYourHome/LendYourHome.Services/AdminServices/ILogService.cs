namespace LendYourHome.Services.AdminServices
{
    using System;
    using System.Collections.Generic;
    using AdminServiceModels;
    using Data.Models.Enums;

    public interface ILogService
    {
        void Create(string adminId, 
            AdminLogType logType, 
            string targetedUserName,
            DateTime submitDate);

        IEnumerable<LogAdminServiceModel> All(int pageNumber,
            int pageSize);

        int Total();
    }
}