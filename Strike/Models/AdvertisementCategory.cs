using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strike.Models
{
    public class AdvertisementCategory
    {
        public AdvertisementCategory() { }
        public AdvertisementCategory(int advertisementId, int categoryId)
        {
            this.AdvertisementId = advertisementId;
            this.CategoryId = categoryId;
        }

        //Connection table for many to many relation
        public int AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

}
