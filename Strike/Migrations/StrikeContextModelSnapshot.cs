﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Strike.Data;

namespace Strike.Migrations
{
    [DbContext(typeof(StrikeContext))]
    partial class StrikeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("Strike.Models.Advertisement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Area");

                    b.Property<string>("County");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("datetime('now', 'localtime')");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<double>("Price");

                    b.Property<string>("Title");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Advertisements");
                });

            modelBuilder.Entity("Strike.Models.AdvertisementCategory", b =>
                {
                    b.Property<int>("AdvertisementId");

                    b.Property<int>("CategoryId");

                    b.HasKey("AdvertisementId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("AdvertisementCategories");
                });

            modelBuilder.Entity("Strike.Models.AdvertisementImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdvertisementId");

                    b.Property<string>("Path");

                    b.Property<long>("Size");

                    b.HasKey("Id");

                    b.HasIndex("AdvertisementId");

                    b.ToTable("AdvertisementImages");
                });

            modelBuilder.Entity("Strike.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new { Id = 1, Name = "Bilar" },
                        new { Id = 2, Name = "Båtar" },
                        new { Id = 3, Name = "Husvagnar & husbilar" },
                        new { Id = 4, Name = "Mopeder & A-traktor" },
                        new { Id = 5, Name = "Motorcycklar" },
                        new { Id = 6, Name = "Badrum/WC/bastu" },
                        new { Id = 7, Name = "Kök" },
                        new { Id = 8, Name = "Trädgård & uteplats" },
                        new { Id = 9, Name = "Belysning" },
                        new { Id = 10, Name = "Möbler" },
                        new { Id = 11, Name = "Hemtextil & prydnad" },
                        new { Id = 12, Name = "Vitvaror" },
                        new { Id = 13, Name = "Kökstillbehör & poslin" },
                        new { Id = 14, Name = "Verktyg" },
                        new { Id = 15, Name = "Kläder" },
                        new { Id = 16, Name = "Accessoarer" },
                        new { Id = 17, Name = "Skor & väskor" },
                        new { Id = 18, Name = "Leksaker" },
                        new { Id = 20, Name = "Datorer & surfplattor" },
                        new { Id = 22, Name = "Spel" },
                        new { Id = 23, Name = "Foto & videokameror" },
                        new { Id = 24, Name = "Stereo & surround" },
                        new { Id = 25, Name = "Tv & projektor" },
                        new { Id = 26, Name = "Telefoner & tillbehör" },
                        new { Id = 27, Name = "Biljetter & presentkort" },
                        new { Id = 28, Name = "Böcker & studentlitteratur" },
                        new { Id = 29, Name = "Cycklar" },
                        new { Id = 30, Name = "Djur" },
                        new { Id = 31, Name = "Hobby & fritid" },
                        new { Id = 32, Name = "Jakt & fiske" },
                        new { Id = 33, Name = "Musikutrustning" },
                        new { Id = 34, Name = "Camping & friluftsliv" },
                        new { Id = 35, Name = "Träning & hälsa" }
                    );
                });

            modelBuilder.Entity("Strike.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Strike.Models.Advertisement", b =>
                {
                    b.HasOne("Strike.Models.User", "User")
                        .WithMany("Advertisements")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Strike.Models.AdvertisementCategory", b =>
                {
                    b.HasOne("Strike.Models.Advertisement", "Advertisement")
                        .WithMany("AdvertisementCategories")
                        .HasForeignKey("AdvertisementId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Strike.Models.Category", "Category")
                        .WithMany("AdvertisementCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Strike.Models.AdvertisementImage", b =>
                {
                    b.HasOne("Strike.Models.Advertisement", "Advertisement")
                        .WithMany("AdvertisementImages")
                        .HasForeignKey("AdvertisementId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
