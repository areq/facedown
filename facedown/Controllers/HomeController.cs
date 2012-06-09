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
using FacebookProject.Controllers;
using FacebookProject.Models;

namespace FacebookProject.Controllers
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
        Models.FacebookAPI FB = new Models.FacebookAPI();


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
        //Petición AJAX para publicar un mensaje en el muro
        //[AuthFilter]
        //////////////public ActionResult Post(string mensaje)
        //////////////{
        //////////////    string postid = "";

        //////////////    try
        //////////////    {
        //////////////        //utilizo la API de facebook para publicar el post
        //////////////        postid = FB.PublicarPost("me", mensaje);

        //////////////    }
        //////////////    catch (Exception e)
        //////////////    {

        //////////////        //envio un mensaje de error a la vista
        //////////////        string strError = "Error al intentar postear.<br />Detalle: " + e.Message;
        //////////////        return View("ErrorPartial", (object)strError);
        //////////////    }

        //////////////    //preparo un Post para enviar a la vista y devolver el partial mostrando el mismo
        //////////////    Post post = new Post();
        //////////////    post.id = postid;
        //////////////    post.user_from = (User)Session["user"];
        //////////////    post.mensaje = mensaje;
        //////////////    post.created_time = Convert.ToDateTime(DateTime.Now);
        //////////////    post.comentarios = new List<Comentario>();

        //////////////    return View("PostPartial", post);

        //////////////}

    //    //Petición AJAX para publicar un comentario de un post
    //    [AuthFilter]
    //    public ActionResult Comentar(string mensaje, string postid)
    //    {
    //        string comentarioid = "";

    //        try
    //        {
    //            //utilizo la API de facebook para publicar este comentario
    //            comentarioid = FB.PublicarComentario(postid, mensaje);
    //        }
    //        catch (Exception e)
    //        {
    //            //envio un mensaje de error a la vista
    //            string strError = "Error al publicar comentario.<br />Detalle: " + e.Message;
    //            return View("ErrorPartial", (object)strError);
    //        }

    //        //preparo un Comentario para enviar a la vista y devolver el partial mostrando el mismo
    //        Comentario comentario = new Comentario();
    //        comentario.id = comentarioid;
    //        comentario.user_from = (User)Session["user"];
    //        comentario.mensaje = mensaje;
    //        comentario.created_time = Convert.ToDateTime(DateTime.Now);

    //        return View("ComentarioPartial", comentario);


    //    }

    //    //Petición AJAX para traer todo el listado de amigos
    //    [AuthFilter]
    //    public ActionResult ListaAmigos()
    //    {
    //        List<User> amigos;
    //        try
    //        {
    //            //utilizo la API de facebook para traer todos los amigos
    //            amigos = FB.GetAmigos("me", true);
    //        }
    //        catch (Exception e)
    //        {
    //            //envio un mensaje de error a la vista
    //            string strError = "Error al traer los amigos.<br />Detalle: " + e.Message;
    //            return View("ErrorPartial", (object)strError);
    //        }

    //        return View("ListaAmigosPartial", amigos);
    //    }

    //    //Petición AJAX para traer todos los comentarios de un post
    //    [AuthFilter]
    //    public ActionResult GetAllComentarios(string postid)
    //    {
    //        List<Comentario> comentarios;
    //        try
    //        {
    //            //utilizo la API de facebook para traer todos los amigos
    //            comentarios = FB.GetAllComentarios(postid);
    //        }
    //        catch (Exception e)
    //        {
    //            //envio un mensaje de error a la vista
    //            string strError = "Error al traer los comentarios.<br />Detalle: " + e.Message;
    //            return View("ErrorPartial", (object)strError);
    //        }


    //        return View("ListaComentariosPartial", comentarios);
    //    }

    //    //Petición AJAX para borrar un comentario
    //    [AuthFilter]
    //    public ActionResult BorrarComentario(string comentarioid)
    //    {

    //        try
    //        {
    //            //utilizo la API de facebook para traer todos los amigos
    //            FB.BorrarComentario(comentarioid);
    //        }
    //        catch (Exception e)
    //        {
    //            //envio un mensaje de error a la vista
    //            string strError = "Error al borrar comentario.<br />Detalle: " + e.Message;
    //            return View("ErrorPartial", (object)strError);
    //        }

    //        return Content(String.Empty);
    //    }

    //    //Petición AJAX para borrar un post
    //    [AuthFilter]
    //    public ActionResult BorrarPost(string postid)
    //    {

    //        try
    //        {
    //            //utilizo la API de facebook para traer todos los amigos
    //            FB.BorrarPost(postid);
    //        }
    //        catch (Exception e)
    //        {
    //            //envio un mensaje de error a la vista
    //            string strError = "Error al borrar post.<br />Detalle: " + e.Message;
    //            return View("ErrorPartial", (object)strError);
    //        }

    //        return Content(String.Empty);
    //    }


    //    //Petición AJAX para traer todos los comentarios de un post
    //    [AuthFilter]
    //    public ActionResult MasPosts(string offset)
    //    {

    //        /*
    //         TODO: 
             
    //         * agregar el ver mas post
    //         * agregar el ver publicaciones anteriores
    //         * agregar el logout!!!
    //         * agregar el icono de espera en todas las peticiones ajax
    //         * paginar el listado de amigos(con infinite scroll!!)
    //         * agregar favico de fb
    //         * chequear funcionamiento minimo en ie6
    //         * chequear compatibilidad en ie7,8 y firefox
    //         * poner el estilo del celestito claro de arriba
    //         * ver si se puede achicar el ancho en general para que sea mas como facebook
                          
    //         */
    //        List<Post> posts;
    //        try
    //        {
    //            //utilizo la API de facebook para traer todos los amigos
    //            posts = FB.GetPostsMuro("me", offset);
    //        }
    //        catch (Exception e)
    //        {
    //            //envio un mensaje de error a la vista
    //            string strError = "Error al traer los posts.<br />Detalle: " + e.Message;
    //            return View("ErrorPartial", (object)strError);
    //        }



    //        return View("ListaPostsPartial", posts);
    //    }


    }
}
