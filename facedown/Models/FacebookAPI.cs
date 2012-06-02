using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Globalization;
using System.Drawing;

namespace FacebookProject.Models
{
    public class FacebookAPI
    {
        public const int MAX_COMMENTS = 3;

        private oAuthFacebook oAuth = new oAuthFacebook();
        public FacebookAPI(){
            oAuth = new oAuthFacebook();
        }
        public string GetAuthorizationLink()
        {
            return oAuth.AuthorizationLinkGet();
        }
        public void EstablecerConexion(string code)
        {
            oAuth.AccessTokenGet(code);
        }
        
        /*public string GetInfoPerfil(string uid)
        {
            string url = "https://graph.facebook.com/" + uid + "?access_token=" + oAuth.Token;
            return oAuth.WebRequest(oAuthFacebook.Method.GET, url, string.Empty);
        }*/

        //todos: si es true => trae a todos los amigos, sino trae un numero limitado de amigos
        public List<User> GetAmigos(string uid, bool todos) {
            
            string url = "https://graph.facebook.com/" + uid + "/friends?access_token=" + oAuth.Token;

            //si no solicito todos los amigos que solo me traiga los primeros 6
            if (!todos)
            {
                url += "&limit=6";
            }
            string json = oAuth.WebRequest(oAuthFacebook.Method.GET, url, string.Empty);
                        
            //parseo el XML, mientras lo recorro creo objetos User y los agrego a
            //una lista, al final retorno dicha lista
            List<User> amigos = new List<User>();
                        
            JObject o = JObject.Parse(json);
            IList<JToken> amigos_token = o["data"].Children().ToList();

            foreach (var am in amigos_token) {

                User user = new User();
                user.id = (string)am["id"];
                user.nombre_completo = (string)am["name"];
                user.link = "http://www.facebook.com/profile.php?id=" + user.id;
                amigos.Add(user);
            }

            return amigos;

        }
        
        public string GetFotoPerfl(string uid)
        {
            string url = "https://graph.facebook.com/" + uid + "/picture?access_token=" + oAuth.Token;
            return oAuth.WebRequest(oAuthFacebook.Method.GET, url, string.Empty);
        }


        public List<Post> GetPostsMuro(string uid, string offset) {
            string url="";
            //Si la llamo por primera vez traerá los últimos post sino trae los anteriores con una URL que 
            //ya tengo en sesión y además guarda la URL siguiente 
            
            url = "https://graph.facebook.com/me/feed?limit=25&access_token=" + oAuth.Token;

            if (Convert.ToInt32(offset) > 0) {
                
                url = (string)System.Web.HttpContext.Current.Session["url_next_posts"];
            }
            
            string json = oAuth.WebRequest(oAuthFacebook.Method.GET, url, string.Empty);
                        
            List<Post> posts = new List<Post>();

            JObject o = JObject.Parse(json);
            System.Web.HttpContext.Current.Session["url_next_posts"] = (string)o.SelectToken("paging.next");
            int max = o.SelectToken("data").Count();
            for (int i = 0; i < max; i++)
            {
                User user = new User();

                Post post = new Post();
                post.id = (string)o.SelectToken("data[" + i + "].id");
                post.tipo = (string)o.SelectToken("data[" + i + "].type");
                //post.tipo = "dd";       
                switch (post.tipo) { 
                    case "photo":

                        post.picture = (string)o.SelectToken("data[" + i + "].picture");
                        post.link = (string)o.SelectToken("data[" + i + "].link");
                        post.name = (string)o.SelectToken("data[" + i + "].name");
                        post.description = (string)o.SelectToken("data[" + i + "].description");
                        
                        post.mensaje = "Se ha etiquetado a ";
                        user.id = (string)o.SelectToken("data[" + i + "].to.data[0].id");
                        user.nombre_completo = (string)o.SelectToken("data[" + i + "].to.data[0].name");
                        user.link = "http://www.facebook.com/profile.php?id=" + user.id;
                        post.user_from = user;
            
                         break;
                    case "link":
                        post.picture = (string)o.SelectToken("data[" + i + "].picture");
                        post.link = (string)o.SelectToken("data[" + i + "].link");
                        post.name = (string)o.SelectToken("data[" + i + "].name");
                        post.caption = (string)o.SelectToken("data[" + i + "].caption");

                        
                        user.id = (string)o.SelectToken("data[" + i + "].from.id");
                        user.nombre_completo = (string)o.SelectToken("data[" + i + "].from.name");
                        user.link = "http://www.facebook.com/profile.php?id=" + user.id;
                        post.user_from = user;
                        break;
                    case "status":
                    default:
                        
                        post.mensaje = (string)o.SelectToken("data[" + i + "].message");
                        user.id = (string)o.SelectToken("data[" + i + "].from.id");
                        user.nombre_completo = (string)o.SelectToken("data[" + i + "].from.name");
                        user.link = "http://www.facebook.com/profile.php?id=" + user.id;
                        post.user_from = user;
                        break;
                
                }
                
                post.created_time = Convert.ToDateTime((string)o.SelectToken("data[" + i + "].created_time"));
             
                

                //cantidad de comentarios para este post
                int count = 0;
                //Pongo esto porque puede darse la posibilidad que no tenga comentarios y directamente no aparece esta sección
                if(o.SelectToken("data[" + i + "].comments.count")!=null)
                    post.comentarios_count = (int)o.SelectToken("data[" + i + "].comments.count");

                List<Comentario> comentarios = new List<Comentario>();
                if (post.comentarios_count > 0)
                {
                    
                    IList<JToken> comentarios_token = o["data"][i]["comments"]["data"].Children().ToList();
                    for (int j = 0; j < comentarios_token.Count; j++)
                    {
                        Comentario c = new Comentario();
                        c.id = (string)o.SelectToken("data[" + i + "].comments.data[" + j + "].id");
                        c.mensaje = (string)o.SelectToken("data[" + i + "].comments.data[" + j + "].message");
                        c.created_time = Convert.ToDateTime((string)o.SelectToken("data[" + i + "].comments.data[" + j + "].created_time"));

                        User u = new User();
                        u.id = (string)o.SelectToken("data[" + i + "].comments.data[" + j + "].from.id");
                        u.nombre_completo = (string)o.SelectToken("data[" + i + "].comments.data[" + j + "].from.name");
                        u.link = "http://www.facebook.com/profile.php?id=" + u.id;

                        c.user_from = u;
                        comentarios.Add(c);

                    }
                }
                post.comentarios = comentarios;
                
                posts.Add(post);    
            }

            return posts;

        }
        

        public User GetUser(string uid)
        {
            string url = "https://graph.facebook.com/" + uid + "?access_token=" + oAuth.Token;


            string json = oAuth.WebRequest(oAuthFacebook.Method.GET, url, string.Empty);
            
            
            JObject o = JObject.Parse(json);
                        
            //parseo el request y devuelvo el user
            User user = new User();
            user.id = (string)o["id"];
            user.nombre_completo = (string)o["name"];
            user.apellido = (string)o["last_name"];
            user.nombre = (string)o["first_name"];
            user.sexo = (string)o["gender"];
            user.locale = (string)o["locale"];
            user.link = (string)o["link"];
            //user.timezone = (string)o["timezone"];


            return user;
            

        }

        //Publicar un post
        public string PublicarPost(string uid, string mensaje) {

            string url = "https://graph.facebook.com/" + uid + "/feed/?message="+ mensaje.Replace(" ","%20") +"&access_token=" + oAuth.Token;
            string json = oAuth.WebRequest(oAuthFacebook.Method.POST, url, string.Empty);
            JObject o = JObject.Parse(json);
            return (string)o["id"];
            
        }

        //Publicar un comentario de un post
        public string PublicarComentario(string postid, string mensaje) {

            string url = "https://graph.facebook.com/" + postid + "/comments/?message=" + mensaje.Replace(" ", "%20") + "&access_token=" + oAuth.Token;
            string json = oAuth.WebRequest(oAuthFacebook.Method.POST, url, string.Empty);
            JObject o = JObject.Parse(json);
            return (string)o["id"];

        }


        //Traer todos los comentarios de un post
        public List<Comentario> GetAllComentarios(string postid) {
                        
            string url = "https://graph.facebook.com/" + postid + "?access_token=" + oAuth.Token;
            string json = oAuth.WebRequest(oAuthFacebook.Method.GET, url, string.Empty);
            JObject o = JObject.Parse(json);

            List<Comentario> comentarios = new List<Comentario>();
            IList<JToken> comentarios_token = o["comments"]["data"].Children().ToList();
            foreach(var com in comentarios_token) {
            
                Comentario c = new Comentario();
                c.id = (string)com["id"];
                c.mensaje = (string)com["message"];
                c.created_time = Convert.ToDateTime((string)com["created_time"]);

                User u = new User();
                u.id = (string)com["from"]["id"]; 
                u.nombre_completo = (string)com["from"]["name"];
                u.link = "http://www.facebook.com/profile.php?id=" + u.id;

                c.user_from = u;
                comentarios.Add(c);

            }
            return comentarios;

        }

        public void BorrarComentario(string comentarioid)
        {
            string url = "https://graph.facebook.com/" + comentarioid + "?method=delete&access_token=" + oAuth.Token;
            string json = oAuth.WebRequest(oAuthFacebook.Method.POST, url, string.Empty);
        }

        internal void BorrarPost(string postid)
        {
            string url = "https://graph.facebook.com/" + postid + "?method=delete&access_token=" + oAuth.Token;
            string json = oAuth.WebRequest(oAuthFacebook.Method.POST, url, string.Empty);
        }
    }
    
}
