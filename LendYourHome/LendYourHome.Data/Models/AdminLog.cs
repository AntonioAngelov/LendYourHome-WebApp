namespace LendYourHome.Data.Models
{
    using System;
    using Enums;

    public class AdminLog
    {
        public int Id { get; set; }

        public string AdminId { get; set; }

        public User Admin { get; set; }

        public AdminLogType LogType { get; set; }

        public string TargetedUserName { get; set; }

        public DateTime SubmitDate { get; set; }

    }
}
