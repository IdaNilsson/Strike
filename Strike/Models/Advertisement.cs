using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Strike.Models
{
    public class Advertisement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string County { get; set; }
        public string Area { get; set; }

        //FK for advertisementImages
        public List<AdvertisementImage> AdvertisementImages { get; set; }

        public List<SelectListItem> Counties = new List<SelectListItem>( new SelectListItem[] {
            new SelectListItem { Text = "Blekinge", Value = "0" },
            new SelectListItem { Text = "Dalarna ", Value = "1" },
            new SelectListItem { Text = "Gotland", Value = "2" },
            new SelectListItem { Text = "Gävleborg", Value = "3" },
            new SelectListItem { Text = "Halland", Value = "4" },
            new SelectListItem { Text = "Jämtland ", Value = "5" },
            new SelectListItem { Text = "Jönköping", Value = "6" },
            new SelectListItem { Text = "Kalmar", Value = "7" },
            new SelectListItem { Text = "Kronoberg", Value = "8" },
            new SelectListItem { Text = "Norrbotten ", Value = "9" },
            new SelectListItem { Text = "Skåne", Value = "10" },
            new SelectListItem { Text = "Stockholm", Value = "11" },
            new SelectListItem { Text = "Södermanland", Value = "12" },
            new SelectListItem { Text = "Uppsala ", Value = "13" },
            new SelectListItem { Text = "Värmland", Value = "14" },
            new SelectListItem { Text = "Västerbotten", Value = "15" },
            new SelectListItem { Text = "Västernorrland", Value = "16" },
            new SelectListItem { Text = "Västmanland ", Value = "17" },
            new SelectListItem { Text = "Västra Götaland", Value = "18" },
            new SelectListItem { Text = "Örebro", Value = "19" },
            new SelectListItem { Text = "Östergötland", Value = "20" }
        });

        //Many to many relation category-advertisement
        public List<AdvertisementCategory> AdvertisementCategories { get; set; }
    }
}
