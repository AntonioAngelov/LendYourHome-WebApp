namespace LendYourHome.Application.Models.HomesViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using static Common.Constants.DataConstants;

    public class HomeCreateViewModel
    {
        [Required]
        [MinLength(HomeCountryMinLength, ErrorMessage = "Country name must be atleast 4 symbols.")]
        [MaxLength(HomeCountryMaxLength)]
        [Display(Prompt = "Holland")]
        public string Country { get; set; }

        [Required]
        [MinLength(HomeCityMinLength, ErrorMessage = "City name must be at least 4 symbols.")]
        [MaxLength(HomeCityMaxLength)]
        [Display(Prompt = "Amsterdam")]
        public string City { get; set; }

        [Required]
        [MinLength(AddressMinLength, ErrorMessage = "Address must be at leas 5 symbols")]
        [MaxLength(AddressMaxLength)]
        [Display(Prompt = "street 1, bl. 1, floor 2, ap. 10")]
        public string Address { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Minimun 1 sleeps")]
        [Display(Prompt = "1")]
        public int? Sleeps { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Minimun 0 bedrooms")]
        [Display(Prompt = "0")]
        public int? Bedrooms { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Minimun 0 bathrooms")]
        [Display(Prompt = "0")]
        public int? Bathrooms { get; set; }

        [Display(Name = "Price per night", Prompt = "0.00")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative number")]
        public decimal PricePerNight { get; set; }

        [Display(Name = "Additional lnformation", Prompt = "Say something about your home")]
        public string Additionalnformation { get; set; }

        [Display(Name = "Activate offer from now")]
        public bool IsActiveOffer { get; set; }

        public List<IFormFile> Pictures { get; set; }
    }
}
