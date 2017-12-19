namespace LendYourHome.Application.Areas.Admin.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BanUserViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BanEndDate { get; set; }
    }
}
