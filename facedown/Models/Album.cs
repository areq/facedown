using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacebookProject.Models
{
    public class Album
    {
        public string id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
		public string cover { get; set; }
		public int cantFotos { get; set; }
		
        public List<Foto> Fotos { get; set; }
    }
}

