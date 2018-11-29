using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strike.Views.Shared
{
    public class Constants
    {
        public static List<SelectListItem> Counties = new List<SelectListItem>(new SelectListItem[] {
            new SelectListItem { Text = "Välj", Value = "-1" },
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

        public static List<SelectListItem> Areas = new List<SelectListItem>(new SelectListItem[] {
            new SelectListItem { Text = "Malmö", Value = "10" },
            new SelectListItem { Text = "Helsingborg", Value = "10" },
            new SelectListItem { Text = "Trelleborg", Value = "10" },
            new SelectListItem { Text = "Mjölby", Value = "20" },
        });
    }
}
