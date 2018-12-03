using Microsoft.AspNetCore.Mvc.Rendering;
using Strike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strike.Views.Shared
{
    public class Constants
    {
        public static List<SelectListItem> Counties = new List<SelectListItem>(new SelectListItem[] {
            new SelectListItem { Text = "Välj", Value = "0" },
            new SelectListItem { Text = "Blekinge", Value = "1" },
            new SelectListItem { Text = "Bohuslän", Value = "2" },
            new SelectListItem { Text = "Dalarna ", Value = "3" },
            new SelectListItem { Text = "Dalsland ", Value = "4" },
            new SelectListItem { Text = "Gotland", Value = "5" },
            new SelectListItem { Text = "Gästrikland", Value = "6" },
            new SelectListItem { Text = "Halland", Value = "7" },
            new SelectListItem { Text = "Hälsingland ", Value = "8" },
            new SelectListItem { Text = "Härjedalen ", Value = "9" },
            new SelectListItem { Text = "Jämtland ", Value = "10" },
            new SelectListItem { Text = "Lappland", Value = "11" },
            new SelectListItem { Text = "Medelpad", Value = "12" },
            new SelectListItem { Text = "Norrbotten ", Value = "13" },
            new SelectListItem { Text = "Närke ", Value = "14" },
            new SelectListItem { Text = "Skåne", Value = "15" },
            new SelectListItem { Text = "Småland", Value = "16" },
            new SelectListItem { Text = "Södermanland", Value = "17" },
            new SelectListItem { Text = "Uppland ", Value = "18" },
            new SelectListItem { Text = "Värmland", Value = "19" },
            new SelectListItem { Text = "Västerbotten", Value = "20" },
            new SelectListItem { Text = "Västergötaland", Value = "21" },
            new SelectListItem { Text = "Västmanland ", Value = "22" },
            new SelectListItem { Text = "Ångermanland", Value = "23" },
            new SelectListItem { Text = "Öland", Value = "24" },
            new SelectListItem { Text = "Östergötland", Value = "25" }
        });
    }
}
