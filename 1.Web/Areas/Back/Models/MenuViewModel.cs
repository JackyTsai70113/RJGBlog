using System.Collections.Generic;
using Core.Data.Entities;

namespace Web.Areas.Back.Models
{
    public class MenuViewModel
    {
        public List<Menu> Menus { get; set; }

        public string PageArea { get; set; }

        public string PageController { get; set; }
    }
}
