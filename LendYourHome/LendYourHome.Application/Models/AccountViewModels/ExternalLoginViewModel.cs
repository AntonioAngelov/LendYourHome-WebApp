namespace LendYourHome.Application.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ExternalLoginViewModel
    {
        [Required]
        public string Username { get; set; }

        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
