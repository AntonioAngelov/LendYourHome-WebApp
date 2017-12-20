namespace LendYourHome.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class LendYourHomeDbContext : IdentityDbContext<User>
    {
        public DbSet<Home> Homes { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<HomeReview> HomeReviews { get; set; }

        public DbSet<GuestReview> GuestReviews { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<AdminLog> AdminLogs { get; set; }

        public LendYourHomeDbContext(DbContextOptions<LendYourHomeDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasOne(u => u.Home)
                .WithOne(h => h.Owner)
                .HasForeignKey<Home>(h => h.OwnerId);

            builder.Entity<Booking>()
                .HasOne(b => b.Home)
                .WithMany(h => h.Bookings)
                .HasForeignKey(b => b.HomeId);

            builder.Entity<Booking>()
                .HasOne(b => b.Guest)
                .WithMany(u => u.BookingsMade)
                .HasForeignKey(b => b.GuestId);

            builder.Entity<Review>()
                .HasDiscriminator<string>("Type")
                .HasValue<HomeReview>("HomeReview")
                .HasValue<GuestReview>("GuestReview");

            builder.Entity<HomeReview>()
                .HasOne(r => r.EvaluatingGuest)
                .WithMany(u => u.HomeReviewsMade)
                .HasForeignKey(r => r.EvaluatingGuestId);

            builder.Entity<HomeReview>()
                .HasOne(r => r.Home)
                .WithMany(h => h.Reviews)
                .HasForeignKey(r => r.HomeId);

            builder.Entity<GuestReview>()
                .HasOne(r => r.EvaluatedGuest)
                .WithMany(u => u.GuestReviewsReceived)
                .HasForeignKey(r => r.EvaluatedGuestId);

            builder.Entity<GuestReview>()
                .HasOne(r => r.Host)
                .WithMany(u => u.GuestReviewsMade)
                .HasForeignKey(r => r.HostId);

            builder.Entity<Home>()
                .HasMany(h => h.Pictures)
                .WithOne(p => p.Home)
                .HasForeignKey(p => p.HomeId);

            builder.Entity<AdminLog>()
                .HasOne(l => l.Admin)
                .WithMany(u => u.AdminLogs)
                .HasForeignKey(l => l.AdminId);

            base.OnModelCreating(builder);
        }
    }
}
