namespace LendYourHome.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Picture
    {
        public int Id { get; set; }

        public int HomeId { get; set; }

        public Home Home { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
