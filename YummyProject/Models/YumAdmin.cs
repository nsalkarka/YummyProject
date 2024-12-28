using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YummyProject.Models
{
    public class YumAdmin
    {
        public int YumAdminId { get; set; }
        public string NameSurname { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }
    }
}