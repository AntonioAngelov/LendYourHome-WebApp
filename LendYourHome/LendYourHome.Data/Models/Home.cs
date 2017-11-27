namespace LendYourHome.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static LendYourHome.Common.Constants.DataConstants;

    public class Home
    {
        public int Id { get; set; }

        [Required]
        [MinLength(HomeCountryMinLength)]
        [MaxLength(HomeCountryMaxLength)]
        public string Country { get; set; }

        [Required]
        [MinLength(HomeCityMinLength)]
        [MaxLength(HomeCityMaxLength)]
        public string City { get; set; }

        [Required]
        [MinLength(AddressMinLength)]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        [Required]
        public int? Sleeps { get; set; }

        [Required]
        public int? Bedrooms { get; set; }

        [Required]
        public int? Bathrooms { get; set; }

        public decimal PricePerNight { get; set; }
        
        public string Additionalnformation { get; set; }

        public bool IsActiveOffer { get; set; }

        public List<Picture> Pictures { get; set; } = new List<Picture>();

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public List<Booking> Bookings { get; set; } = new List<Booking>();

        public List<HomeReview> Reviews { get; set; } = new List<HomeReview>();
    }
}
