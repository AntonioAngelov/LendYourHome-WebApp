namespace LendYourHome.Application.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum NumbersRange
    {
        Any = 0,
        [Display(Name = "0-2")]
        FromZeroToTwo = 1,
        [Display(Name = "3-5")]
        FromThreeToFive = 2,
        More
    }
}
