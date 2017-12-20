namespace LendYourHome.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    
    using static Common.Constants.DataConstants;

    public class User : IdentityUser
    {
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        public string AdditionalInformation { get; set; }

        public string ProfilePictureUrl { get; set; }

        public Home Home { get; set; }

        public DateTime? BanEndDate { get; set; }

        public List<Booking> BookingsMade { get; set; } = new List<Booking>();

        public List<HomeReview> HomeReviewsMade { get; set; } = new List<HomeReview>();

        public List<GuestReview> GuestReviewsMade { get; set; } = new List<GuestReview>();

        public List<GuestReview> GuestReviewsReceived { get; set; } = new List<GuestReview>();

        public List<AdminLog> AdminLogs { get; set; } = new List<AdminLog>();
    }
}
