namespace LendYourHome.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public class Message
    {
        public int Id { get; set; }

        public string SenderId { get; set; }

        public User Sender { get; set; }

        public string RecipientId { get; set; }

        public User Recipient { get; set; }

        [Required]
        [MaxLength(DataConstants.MessageSubjectMaxLength)]
        [MinLength(DataConstants.MessageSubjectMaxLength)]
        public string Subject { get; set; }

        [Required]
        [MinLength(DataConstants.MessageTextMinLength)]
        public string Text { get; set; }

        public DateTime SentDate { get; set; }
    }
}
