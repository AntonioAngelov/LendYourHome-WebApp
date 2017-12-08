namespace LendYourHome.Application.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum SleepsRange
    {
        Any = 0,
        [Display(Name = "1-2")]
        FromOneToTwo = 1,
        [Display(Name = "3-5")]
        FromThreeToFive = 2,
        More = 3
    }
}
