namespace LendYourHome.Application.Areas.Host.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;
    using Microsoft.AspNetCore.Mvc;

    public class ReviewCreateViewModel
    {
        [HiddenInput]
        public string GuestId { get; set; }

        [Range(DataConstants.EvaluationMinValue, DataConstants.EvaluationMaxValue)]
        [Display(Name = "Evaluate guest")]
        public int Evaluation { get; set; }

        [Required]
        [MinLength(DataConstants.ReviewTitleMinLength)]
        [MaxLength(DataConstants.ReviewTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DataConstants.AdditionalInfoMaxLength)]
        [Display(Name = "Say something you guest's visit")]
        [DataType(DataType.MultilineText)]
        public string AdditionalThoughts { get; set; }
    }
}
