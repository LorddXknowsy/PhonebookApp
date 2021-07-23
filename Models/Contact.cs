using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactApp.Models
{
    public class Contact
    {
        public int ID { get; set; }

        public string userName { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }
        public int phone { get; set; }
        public string address { get; set; }

        public string email { get; set; }

    }
}