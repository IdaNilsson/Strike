using Microsoft.EntityFrameworkCore;
using Strike.Models;
using Strike.Views.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strike.Data
{
    public class StrikeContext : DbContext
    {
        public StrikeContext(DbContextOptions<StrikeContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; } 
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<AdvertisementImage> AdvertisementImages { get; set; }
        public DbSet<AdvertisementCategory> AdvertisementCategories { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Forigenkey UserAdvertisement (One to many relation)
            modelBuilder.Entity<Advertisement>()
                .HasOne(a => a.User)
                .WithMany(u => u.Advertisements)
                .HasForeignKey(a => a.UserId);

            //Forigenkey AdvertisementImage (One to many relation)
            modelBuilder.Entity<AdvertisementImage>()
                .HasOne(ai => ai.Advertisement)
                .WithMany(a => a.AdvertisementImages)
                .HasForeignKey(ai => ai.AdvertisementId);

            //Many to many relation AdvertisementCategory
            modelBuilder.Entity<AdvertisementCategory>()
                .HasKey(ac => new { ac.AdvertisementId, ac.CategoryId });

            modelBuilder.Entity<AdvertisementCategory>()
                .HasOne(ac => ac.Advertisement)
                .WithMany(a => a.AdvertisementCategories)
                .HasForeignKey(ac => ac.AdvertisementId);

            modelBuilder.Entity<AdvertisementCategory>()
                .HasOne(ac => ac.Category)
                .WithMany(c => c.AdvertisementCategories)
                .HasForeignKey(ac => ac.CategoryId);

            modelBuilder.Entity<Advertisement>()
                .Ignore(a => a.CategoryIds);

            //Set createdDate to advertisement
            modelBuilder.Entity<Advertisement>()
                .Property(a => a.CreatedDate)
                .HasDefaultValueSql("datetime('now', 'localtime')");

            //Email only aloud once
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "Bilar" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 2, Name = "Båtar" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 3, Name = "Husvagnar & husbilar" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 4, Name = "Mopeder & A-traktor" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 5, Name = "Motorcycklar" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 6, Name = "Badrum/WC/bastu" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 7, Name = "Kök" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 8, Name = "Trädgård & uteplats" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 9, Name = "Belysning" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 10, Name = "Möbler" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 11, Name = "Hemtextil & prydnad" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 12, Name = "Vitvaror" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 13, Name = "Kökstillbehör & poslin" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 14, Name = "Verktyg" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 15, Name = "Kläder" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 16, Name = "Accessoarer" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 17, Name = "Skor & väskor" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 18, Name = "Leksaker" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 20, Name = "Datorer & surfplattor" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 22, Name = "Spel" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 23, Name = "Foto & videokameror" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 24, Name = "Stereo & surround" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 25, Name = "Tv & projektor" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 26, Name = "Telefoner & tillbehör" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 27, Name = "Biljetter & presentkort" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 28, Name = "Böcker & studentlitteratur" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 29, Name = "Cycklar" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 30, Name = "Djur" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 31, Name = "Hobby & fritid" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 32, Name = "Jakt & fiske" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 33, Name = "Musikutrustning" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 34, Name = "Camping & friluftsliv" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 35, Name = "Träning & hälsa" });
        }
    }
}
