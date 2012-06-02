using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacebookProject.Models
{
    public class Foto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
		public string source { get; set; }
		public int height { get; set; }
		public int width { get; set; }
		public string link { get; set; }
    }
}

