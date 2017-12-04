namespace LendYourHome.Application.Models.HomesViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class HomesSearchingViewModel
    {
        [Display(Prompt = "Holland")]
        public string Country { get; set; }
        
        [Display(Prompt = "Amsterdam")]
        public string City { get; set; }
       
        [Display(Prompt = "1")]
        public int Sleeps { get; set; }

        [Display(Prompt = "0")]
        public int Bedrooms { get; set; }

        [Display(Prompt = "0")]
        public int Bathrooms { get; set; }

        [Display(Name = "Min Price per Night", Prompt = "0.00")]
        [Range(0, Double.MaxValue, ErrorMessage = "Price must be possitive number.")]
        public decimal MinPricePerNight { get; set; }

        [Display(Name = "Max Price per Night", Prompt = "0.00")]
        [Range(0, Double.MaxValue, ErrorMessage = "Price must be possitive number.")]
        public decimal MaxPricePerNight { get; set; }
    }
}
