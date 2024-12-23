using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YummyProject.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public int NameSurname { get; set; }
       
        public int UserName { get; set; }
        public int Password { get; set; }
        public int ImageUrl { get; set; }
    }
}