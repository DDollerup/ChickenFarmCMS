using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicNavbar.Models
{
    public class FrontPage
    {
        public int ID { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string PageContent { get; set; }
        public string Image { get; set; }
    }
}