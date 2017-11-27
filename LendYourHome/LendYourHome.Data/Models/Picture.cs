namespace LendYourHome.Data.Models
{
    public class Picture
    {
        public int Id { get; set; }

        public int HomeId { get; set; }

        public Home Home { get; set; }

        public string Url { get; set; }
    }
}
