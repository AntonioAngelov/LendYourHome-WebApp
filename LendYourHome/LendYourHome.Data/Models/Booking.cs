namespace LendYourHome.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Booking
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? CheckInDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? CheckOutDate { get; set; }

        public bool IsApproved { get; set; }
        
        public int HomeId { get; set; }

        public Home Home { get; set; }

        public string GuestId { get; set; }
    
        public User Guest { get; set; }
    }
}
