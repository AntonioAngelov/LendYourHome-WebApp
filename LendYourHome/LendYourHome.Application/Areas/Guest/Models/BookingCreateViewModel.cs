namespace LendYourHome.Application.Areas.Guest.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Infrastructure.Attributes.ModelValidation;

    public class BookingCreateViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Check In Date")]
        [DateValidation]
        public DateTime? CheckInDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Check Out Date")]
        [DateValidation]
        public DateTime? CheckOutDate { get; set; }
    }
}
