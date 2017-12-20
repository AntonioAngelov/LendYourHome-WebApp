namespace LendYourHome.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum AdminLogType
    {
        [Display(Name = "Banned")]
        Ban = 0,
        [Display(Name = "Unbanned")]
        Unban = 1,
        [Display(Name = "Gave Admin Privileges to")]
        MakeAdmin = 2
    }
}
