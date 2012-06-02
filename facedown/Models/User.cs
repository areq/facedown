using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacebookProject.Models
{
    public class User
    {
        public string id { get; set; }
        public string usuario { get; set; }
        public string nombre_completo { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string sexo { get; set; }
        public string link { get; set; }
        public string timezone { get; set; }
        public string locale { get; set; }

        //listado de amigos asociados
        //esta lista en general no se va a cargar al momento de la creación del usuario
        public List<User> amigos { get; set; }

    }
}
