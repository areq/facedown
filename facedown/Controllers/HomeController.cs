using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using facedown.Controllers;
using facedown.Models;

namespace facedown.Controllers
{

    // View Model Classes

    public class HomePageViewModel{

        public User User { get; set; }
        public IEnumerable<Album> Albums { get; set; }

    }

    public class FotosViewModel
    {

        public User User { get; set; }
        public IEnumerable<Foto> Fotos { get; set; }

    }
    
    // Controller Class

    public class HomeController : Controller
    {

        //aplicar spring
        Models.FacebookAPI FB = (FacebookAPI)ServiceFactory.GetImpl("FB");


        //filtro para checkear si el usuario esta autenticado en Facebook
        [AuthFilter]
        public ActionResult Index()
        {
            //guardo el usuario que está logueado en sesión ya que se utiliza bastante
            Session["user"] = FB.GetUser("me");

            //el modelo para trabajar en la vista
            HomePageViewModel viewdata = new HomePageViewModel();
            viewdata.Albums  = FB.GetAlbums("0");
            viewdata.User = (User)Session["user"];

            return View("index", viewdata);
        }

        [AuthFilter]
        public ActionResult fotos(string id)
        {
            FotosViewModel viewdata = new FotosViewModel();
            viewdata.Fotos = FB.GetFotos(id, "0");
            viewdata.User = (User)Session["user"];
            return View("fotos", viewdata);
        }

        public ActionResult downloadFotos(string dire, string nombre)
       {
           WebClient q = new WebClient();
           byte[] imagen = q.DownloadData(dire);

           return File(imagen, "image/jpg", "facedown_imagen-"+nombre+".jpg");
            
           //Guid.NewGuid().ToString())
           //string img = "http://photos-g.ak.fbcdn.net/hphotos-ak-snc6/205306_10150936984529596_560505665_s.jpg";
       }
    }
}
