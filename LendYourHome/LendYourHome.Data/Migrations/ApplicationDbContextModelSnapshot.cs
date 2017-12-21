﻿// <auto-generated />
using LendYourHome.Data;
using LendYourHome.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace LendYourHome.Data.Migrations
{
    [DbContext(typeof(LendYourHomeDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LendYourHome.Data.Models.AdminLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdminId");

                    b.Property<int>("LogType");

                    b.Property<DateTime>("SubmitDate");

                    b.Property<string>("TargetedUserName");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("AdminLogs");
                });

            modelBuilder.Entity("LendYourHome.Data.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CheckInDate")
                        .IsRequired();

                    b.Property<DateTime?>("CheckOutDate")
                        .IsRequired();

                    b.Property<string>("GuestId");

                    b.Property<int>("HomeId");

                    b.Property<bool>("IsApproved");

                    b.HasKey("Id");

                    b.HasIndex("GuestId");

                    b.HasIndex("HomeId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("LendYourHome.Data.Models.Home", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Additionalnformation");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<int?>("Bathrooms")
                        .IsRequired();

                    b.Property<int?>("Bedrooms")
                        .IsRequired();

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(48);

                    b.Property<bool>("IsActiveOffer");

                    b.Property<string>("OwnerId");

                    b.Property<decimal>("PricePerNight");

                    b.Property<int?>("Sleeps")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("OwnerId")
                        .IsUnique()
                        .HasFilter("[OwnerId] IS NOT NULL");

                    b.ToTable("Homes");
                });

            modelBuilder.Entity("LendYourHome.Data.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RecipientId");

                    b.Property<string>("SenderId");

                    b.Property<DateTime>("SentDate");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("LendYourHome.Data.Models.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HomeId");

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("HomeId");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("LendYourHome.Data.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdditionalThoughts")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<int>("Evaluation");

                    b.Property<DateTime>("SubmitDate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Reviews");

                    b.HasDiscriminator<string>("Type").HasValue("Review");
                });

            modelBuilder.Entity("LendYourHome.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AdditionalInformation");

                    b.Property<string>("Address")
                        .HasMaxLength(40);

                    b.Property<DateTime?>("BanEndDate");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("ProfilePictureUrl");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("LendYourHome.Data.Models.GuestReview", b =>
                {
                    b.HasBaseType("LendYourHome.Data.Models.Review");

                    b.Property<string>("EvaluatedGuestId");

                    b.Property<string>("HostId");

                    b.HasIndex("EvaluatedGuestId");

                    b.HasIndex("HostId");

                    b.ToTable("GuestReview");

                    b.HasDiscriminator().HasValue("GuestReview");
                });

            modelBuilder.Entity("LendYourHome.Data.Models.HomeReview", b =>
                {
                    b.HasBaseType("LendYourHome.Data.Models.Review");

                    b.Property<string>("EvaluatingGuestId");

                    b.Property<int>("HomeId");

                    b.HasIndex("EvaluatingGuestId");

                    b.HasIndex("HomeId");

                    b.ToTable("HomeReview");

                    b.HasDiscriminator().HasValue("HomeReview");
                });

            modelBuilder.Entity("LendYourHome.Data.Models.AdminLog", b =>
                {
                    b.HasOne("LendYourHome.Data.Models.User", "Admin")
                        .WithMany("AdminLogs")
                        .HasForeignKey("AdminId");
                });

            modelBuilder.Entity("LendYourHome.Data.Models.Booking", b =>
                {
                    b.HasOne("LendYourHome.Data.Models.User", "Guest")
                        .WithMany("BookingsMade")
                        .HasForeignKey("GuestId");

                    b.HasOne("LendYourHome.Data.Models.Home", "Home")
                        .WithMany("Bookings")
                        .HasForeignKey("HomeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LendYourHome.Data.Models.Home", b =>
                {
                    b.HasOne("LendYourHome.Data.Models.User", "Owner")
                        .WithOne("Home")
                        .HasForeignKey("LendYourHome.Data.Models.Home", "OwnerId");
                });

            modelBuilder.Entity("LendYourHome.Data.Models.Message", b =>
                {
                    b.HasOne("LendYourHome.Data.Models.User", "Recipient")
                        .WithMany("MessagesReceived")
                        .HasForeignKey("RecipientId");

                    b.HasOne("LendYourHome.Data.Models.User", "Sender")
                        .WithMany("MessagesSent")
                        .HasForeignKey("SenderId");
                });

            modelBuilder.Entity("LendYourHome.Data.Models.Picture", b =>
                {
                    b.HasOne("LendYourHome.Data.Models.Home", "Home")
                        .WithMany("Pictures")
                        .HasForeignKey("HomeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LendYourHome.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LendYourHome.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LendYourHome.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LendYourHome.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LendYourHome.Data.Models.GuestReview", b =>
                {
                    b.HasOne("LendYourHome.Data.Models.User", "EvaluatedGuest")
                        .WithMany("GuestReviewsReceived")
                        .HasForeignKey("EvaluatedGuestId");

                    b.HasOne("LendYourHome.Data.Models.User", "Host")
                        .WithMany("GuestReviewsMade")
                        .HasForeignKey("HostId");
                });

            modelBuilder.Entity("LendYourHome.Data.Models.HomeReview", b =>
                {
                    b.HasOne("LendYourHome.Data.Models.User", "EvaluatingGuest")
                        .WithMany("HomeReviewsMade")
                        .HasForeignKey("EvaluatingGuestId");

                    b.HasOne("LendYourHome.Data.Models.Home", "Home")
                        .WithMany("Reviews")
                        .HasForeignKey("HomeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
