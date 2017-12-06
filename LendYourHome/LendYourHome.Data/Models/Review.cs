namespace LendYourHome.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Common.Constants.DataConstants;

    public abstract class Review
    {
        public int Id { get; set; }
        
        [Range(EvaluationMinValue, EvaluationMaxValue)]
        public int Evaluation { get; set; }

        [Required]
        [MinLength(ReviewTitleMinLength)]
        [MaxLength(ReviewTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(AdditionalInfoMaxLength)]
        public string AdditionalThoughts { get; set; }

        [DataType(DataType.Date)]
        public DateTime SubmitDate { get; set; }
    }
}
