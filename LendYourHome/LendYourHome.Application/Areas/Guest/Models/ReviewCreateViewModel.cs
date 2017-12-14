namespace LendYourHome.Application.Areas.Guest.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;
    using Microsoft.AspNetCore.Mvc;

    public class ReviewCreateViewModel
    {
        [HiddenInput]
        public int HomeId { get; set; }

        [Range(DataConstants.EvaluationMinValue, DataConstants.EvaluationMaxValue)]
        [Display(Name = "Evaluate home")]
        public int Evaluation { get; set; }

        [Required]
        [MinLength(DataConstants.ReviewTitleMinLength)]
        [MaxLength(DataConstants.ReviewTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DataConstants.AdditionalInfoMaxLength)]
        [Display(Name = "Say something about your visit")]
        [DataType(DataType.MultilineText)]
        public string AdditionalThoughts { get; set; }
    }
}
