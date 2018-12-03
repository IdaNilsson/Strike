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
            modelBuilder.Entity<Category>().HasData(new Category { Id = 2, Name = "Bildelar och tillbehör" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 3, Name = "Båtar" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 4, Name = "Båtdelar och tillbehör" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 5, Name = "Husvagnar och husbilar" });
        }
    }
}
