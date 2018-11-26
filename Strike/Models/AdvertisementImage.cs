using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strike.Models
{
    public class AdvertisementImage
    {
        public AdvertisementImage()
        {

        }

        public AdvertisementImage(long size, string path, int advertisementId)
        {
            this.Size = size;
            this.Path = path;
            this.AdvertisementId = advertisementId;
        }

        public int Id { get; set; }
        public long Size { get; set; }
        public string Path { get; set; }
        public int AdvertisementId { get; set; }

        public Advertisement Advertisement { get; set; }
    }
}
