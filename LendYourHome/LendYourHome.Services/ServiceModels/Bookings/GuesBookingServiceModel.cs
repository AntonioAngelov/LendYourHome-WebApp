namespace LendYourHome.Services.ServiceModels.Bookings
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class GuestBookingServiceModel : IMapFrom<Booking>, IHaveCustomMapping
    {
        public int HomeId { get; set; }

        public string GuestId { get; set; }

        public string GuestUsername { get; set; }

        [Display(Name = "Check In Date")]
        public DateTime CheckInDate { get; set; }

        [Display(Name = "Check Out Date")]
        public DateTime CheckOutDate { get; set; }

        public string OwnerUsername { get; set; }

        public decimal PricePerNight { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        public string HostId { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Booking, GuestBookingServiceModel>()
                .ForMember(gpb => gpb.OwnerUsername, cfg => cfg.MapFrom(b => b.Home.Owner.UserName))
                .ForMember(gpb => gpb.GuestUsername, cfg => cfg.MapFrom(b => b.Guest.UserName))
                .ForMember(gpb => gpb.PricePerNight, cfg => cfg.MapFrom(b => b.Home.PricePerNight))
                .ForMember(gpb => gpb.TotalPrice, cfg => cfg.MapFrom(b => b.CheckOutDate.Value.Subtract(b.CheckInDate.Value).Days * b.Home.PricePerNight))
                .ForMember(gpb => gpb.HostId, cfg => cfg.MapFrom(b => b.Home.OwnerId));
        }
    }
}
