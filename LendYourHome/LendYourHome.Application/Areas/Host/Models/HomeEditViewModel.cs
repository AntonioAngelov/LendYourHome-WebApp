namespace LendYourHome.Application.Areas.Host.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Common.Constants.DataConstants;

    public class HomeEditViewModel
    {
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
        [Range(OneMaxValue, int.MaxValue)]
        public int? Sleeps { get; set; }

        [Required]
        [Range(ZeroMaxValue, int.MaxValue)]
        public int? Bedrooms { get; set; }

        [Required]
        [Range(ZeroMaxValue, int.MaxValue)]
        public int? Bathrooms { get; set; }

        [Display(Name = "Price per Night")]
        [Range(ZeroMaxValue, double.MaxValue)]
        public decimal PricePerNight { get; set; }

        [Display(Name = "Additional Info")]
        public string Additionalnformation { get; set; }

        public bool IsActiveOffer { get; set; }
    }
}
