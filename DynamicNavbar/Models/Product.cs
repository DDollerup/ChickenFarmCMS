using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicNavbar.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Image { get; set; }
        public string EggWeight { get; set; }
        public string ShellColor { get; set; }
        public string MaleWeight { get; set; }
        public string FemaleWeight { get; set; }
        public int CategoryID { get; set; }
    }
}