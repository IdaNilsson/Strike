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
            modelBuilder.Entity<Category>().HasData(new Category { Id = 2, Name = "Bildelar & tillbehör" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 3, Name = "Båtar" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 4, Name = "Båtdelar & tillbehör" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 5, Name = "Husvagnar & husbilar" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 6, Name = "Mopeder & A-traktor" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 7, Name = "Motorcycklar" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 8, Name = "Mc-delar & tillbehör" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 9, Name = "Badrum/WC/bastu" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 10, Name = "Byggmaterial" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 11, Name = "Kamin & värme" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 12, Name = "Kök" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 13, Name = "Trädgårdsmaskiner" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 14, Name = "Trädgård & uteplats" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 15, Name = "Antik & konst" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 16, Name = "Belysning" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 17, Name = "Bord & stolar" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 18, Name = "Hemtextil & prydnad" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 19, Name = "Hyllor & förvaring" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 20, Name = "Mattor" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 21, Name = "Säng & sovrum" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 22, Name = "Soffa/fåtölj/soffmöblering" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 23, Name = "TV/steriomöbler" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 24, Name = "Diskmaskin" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 25, Name = "Kyl & frys" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 26, Name = "Kökstillbehör & poslin" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 27, Name = "Spis & micro" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 28, Name = "Tvättmaskin & torktumlare" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 29, Name = "Verktyg" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 30, Name = "Brudklänningar" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 31, Name = "Byxor" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 32, Name = "Jackor & ytterplagg" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 33, Name = "Jeans" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 34, Name = "Kjolar & klänningar" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 35, Name = "Kostymer & kavajer" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 36, Name = "Skjortor" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 37, Name = "Skor" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 38, Name = "Toppar" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 39, Name = "Tröjor" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 40, Name = "Klockor" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 41, Name = "Smycken" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 42, Name = "Väskor" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 43, Name = "Barnkläder & baronskor" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 44, Name = "Barnmöbler" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 45, Name = "Barnvagnar  tillbehör" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 46, Name = "Bilbarnstolar" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 47, Name = "Leksaker" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 48, Name = "Stationära datorer" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 49, Name = "Bärbara datorer" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 50, Name = "Surfplattor" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 51, Name = "Datortillbehör & program" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 52, Name = "PC-spel, onlinespel & TV-spel" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 53, Name = "Filmer & musik" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 54, Name = "Foto & videokameror" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 55, Name = "Stereo & surround" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 56, Name = "Tv & projektor" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 57, Name = "Telefoner & tillbehör" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 58, Name = "Biljetter" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 59, Name = "Presentkort" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 60, Name = "Resor" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 61, Name = "Böcker & studentlitteratur" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 62, Name = "Damcycklar" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 63, Name = "Herrcycklar" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 64, Name = "Barncycklar" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 65, Name = "Mountainbike" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 66, Name = "Racercycklar" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 67, Name = "Cyckeltillbehör" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 68, Name = "Djur" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 69, Name = "Djurtillbehör" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 70, Name = "Hobby & samlarprylar" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 71, Name = "Hästar & ridsport" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 72, Name = "Jakt & fiske" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 73, Name = "Musikutrustning" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 74, Name = "Bollsport" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 75, Name = "Camping & friluftsliv" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 76, Name = "Dyk & vattensport" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 77, Name = "Golf" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 78, Name = "Träning & hälsa" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 79, Name = "Vintersport" });
        }
    }
}
