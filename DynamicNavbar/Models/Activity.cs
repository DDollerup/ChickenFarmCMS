using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicNavbar.Models
{
    public class Activity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
    }
}