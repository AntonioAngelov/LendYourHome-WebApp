namespace LendYourHome.Services.AdminServices.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdminServiceModels;
    using AdminServices;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Data.Models.Enums;

    public class LogService : ILogService
    {
        private readonly LendYourHomeDbContext db;

        public LogService(LendYourHomeDbContext db)
        {
            this.db = db;
        }

        public void Create(string adminId, AdminLogType logType, string targetedUserName, DateTime submitDate)
        {
            var log = new AdminLog
            {
                AdminId = adminId,
                LogType = logType,
                TargetedUserName = targetedUserName,
                SubmitDate = submitDate
            };

            this.db.AdminLogs.Add(log);

            this.db.SaveChanges();
        }

        public IEnumerable<LogAdminServiceModel> All(int pageNumber, int pageSize)
            => this.db.AdminLogs
                .OrderByDescending(l => l.SubmitDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<LogAdminServiceModel>()
                .ToList();

        public int Total()
            => this.db.AdminLogs
                .Count();
    }
}
