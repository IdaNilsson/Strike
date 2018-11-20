using Microsoft.EntityFrameworkCore;
using Strike.Models;
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
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Forigenkey UserAdvertisement (One to many relation)
            modelBuilder.Entity<Advertisement>()
                .HasOne(a => a.User)
                .WithMany(u => u.Advertisements)
                .HasForeignKey(a => a.UserId);

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

            //Set createdDate to advertisement
            modelBuilder.Entity<Advertisement>()
                .Property(a => a.CreatedDate)
                .HasDefaultValueSql("datetime('now', 'localtime')");

            //Email only aloud once
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
