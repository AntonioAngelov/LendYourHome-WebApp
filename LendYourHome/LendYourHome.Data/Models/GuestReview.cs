namespace LendYourHome.Data.Models
{
    public class GuestReview : Review
    {
        public string HostId { get; set; }

        public User Host { get; set; }

        public string EvaluatedGuestId { get; set; }

        public User EvaluatedGuest { get; set; }
    }
}
