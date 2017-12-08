﻿namespace LendYourHome.Application.Models.HomesViewModels
{
    using System.ComponentModel.DataAnnotations;

    using static Common.Constants.DataConstants;

    public class HomeCreateViewModel
    {
        [Required]
        [MinLength(HomeCountryMinLength, ErrorMessage = "Country name must be atleast 4 symbols.")]
        [MaxLength(HomeCountryMaxLength)]
        public string Country { get; set; }

        [Required]
        [MinLength(HomeCityMinLength, ErrorMessage = "City name must be at least 4 symbols.")]
        [MaxLength(HomeCityMaxLength)]
        public string City { get; set; }

        [Required]
        [MinLength(AddressMinLength, ErrorMessage = "Address must be at leas 5 symbols")]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Minimun 1 sleeps")]
        public int? Sleeps { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Minimun 0 bedrooms")]
        public int? Bedrooms { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Minimun 0 bathrooms")]
        public int? Bathrooms { get; set; }

        [Display(Name = "Price per night")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative number")]
        public decimal PricePerNight { get; set; }

        [Display(Name = "Additional lnformation")]
        public string Additionalnformation { get; set; }

        [Display(Name = "Activate offer from now")]
        public bool IsActiveOffer { get; set; }
    }
}
