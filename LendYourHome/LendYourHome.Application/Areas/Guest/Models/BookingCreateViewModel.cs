namespace LendYourHome.Application.Areas.Guest.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using Infrastructure.Attributes.ModelValidation;

    public class BookingCreateViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Check In Date")]
        [DateValidation]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CheckInDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateValidation]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CheckOutDate { get; set; }
    }
}
