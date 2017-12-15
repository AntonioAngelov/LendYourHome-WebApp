namespace LendYourHome.Application.Models.HomesViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class HomesSearchingViewModel
    {
        [Display(Prompt = "Holland")]
        public string Country { get; set; }
        
        [Display(Prompt = "Amsterdam")]
        public string City { get; set; }
       
        public SleepsRange Sleeps { get; set; }
        
        public NumbersRange Bedrooms { get; set; }
        
        public NumbersRange Bathrooms { get; set; }

        [Display(Name = "Price Range")]
        public PriceRange PriceRange { get; set; }

        public PageListingModel PageListingData { get; set; }
    }
}
