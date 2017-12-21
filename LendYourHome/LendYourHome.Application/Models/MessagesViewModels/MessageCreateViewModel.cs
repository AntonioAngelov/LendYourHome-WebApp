namespace LendYourHome.Application.Models.MessagesViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public class MessageCreateViewModel
    {
        [Required]
        [MaxLength(DataConstants.MessageSubjectMaxLength)]
        [MinLength(DataConstants.MessageSubjectMinLength)]
        public string Subject { get; set; }

        [Required]
        [MinLength(DataConstants.MessageTextMinLength)]
        public string Text { get; set; }
    }
}
