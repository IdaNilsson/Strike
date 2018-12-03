using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Strike.Models
{
    public class Category
    {
        public Category()
        {

        }
        public Category(string Name)
        {
            this.Name = Name;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        //Many to many relation category-advertisement
        public List<AdvertisementCategory> AdvertisementCategories { get; set; }
    }
}
