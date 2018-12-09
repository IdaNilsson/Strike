using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy HH\\:mm}")]
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string County { get; set; }
        public string Area { get; set; }
        public List<int> CategoryIds { get; set; }

        //FK for advertisementImages
        public List<AdvertisementImage> AdvertisementImages { get; set; }

        //Many to many relation category-advertisement
        public List<AdvertisementCategory> AdvertisementCategories { get; set; }
    }
}
