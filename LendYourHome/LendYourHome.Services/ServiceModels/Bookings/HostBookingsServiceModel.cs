using LendYourHome.Common.Mapping;using LendYourHome.Data.Models;namespace LendYourHome.Services.ServiceModels.Bookings
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;

    public class HostBookingsServiceModel : IMapFrom<Booking>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string GuestId { get; set; }

        public string GuestUsername { get; set; }

        public string GuestProfilePictureUrl { get; set; }

        [Display(Name = "Check In Date")]
        public DateTime CheckInDate { get; set; }

        [Display(Name = "Check Out Date")]
        public DateTime CheckOutDate { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Booking, HostBookingsServiceModel>()
                .ForMember(gpb => gpb.GuestUsername, cfg => cfg.MapFrom(b => b.Guest.UserName))
                .ForMember(gpb => gpb.GuestProfilePictureUrl, cfg => cfg.MapFrom(b => b.Guest.ProfilePictureUrl))
                .ForMember(gpb => gpb.TotalPrice, cfg => cfg.MapFrom(b => b.CheckOutDate.Value.Subtract(b.CheckInDate.Value).Days * b.Home.PricePerNight));
        }
    }
}
