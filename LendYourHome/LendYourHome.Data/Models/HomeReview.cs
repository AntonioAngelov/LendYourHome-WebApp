namespace LendYourHome.Data.Models
{
    public class HomeReview : Review
    {
        public int HomeId { get; set; }

        public Home Home { get; set; }

        public string EvaluatingGuestId { get; set; }

        public User EvaluatingGuest { get; set; }
    }
}
