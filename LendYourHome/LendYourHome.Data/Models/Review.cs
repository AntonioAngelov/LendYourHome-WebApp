namespace LendYourHome.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Common.Constants.DataConstants;

    public abstract class Review
    {
        public int Id { get; set; }
        
        [Range(EvaluationMinValue, EvaluationMaxValue)]
        public byte Evaluation { get; set; }

        [Required]
        [MinLength(ReviewTitleMinLength)]
        [MaxLength(ReviewTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string AdditionalThoughts { get; set; }

        public DateTime SubmitDate { get; set; }
    }
}
