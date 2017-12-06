namespace LendYourHome.Application.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum PriceRange
    {
        Any = 0,
        [Display(Name = "0-10")]
        FromZeroToTen = 1,
        [Display(Name = "10-20")]
        FromTenToTwenty = 2,
        [Display(Name = "20-30")]
        FromTWentyToThirty = 3,
        More = 3
    }
}
