namespace LendYourHome.Application.Models.ReviewsViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;
    using static Common.Constants.DataConstants;

    public class ReviewCreateViewModel
    {
        [HiddenInput]
        public int HomeId { get; set; }

        [Range(EvaluationMinValue, EvaluationMaxValue)]
        [Display(Name = "Evaluate home")]
        public int Evaluation { get; set; }

        [Required]
        [MinLength(ReviewTitleMinLength)]
        [MaxLength(ReviewTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(AdditionalInfoMaxLength)]
        [Display(Name = "Say something about your visit")]
        [DataType(DataType.MultilineText)]
        public string AdditionalThoughts { get; set; }
    }
}
