﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Globalization;
using System.Drawing;
using Spring.Context;

namespace facedown.Models
{
    public class FacebookAPI
    {
        private oAuthFacebook oAuth;

        public FacebookAPI(oAuthFacebook _oAuth){
            oAuth = _oAuth;
        }

        public string GetAuthorizationLink()
        {
            return oAuth.AuthorizationLinkGet();
        }

        public void EstablecerConexion(string code)
        {
            oAuth.AccessTokenGet(code);
        }

        public string GetFotoPerfl(string uid)
        {
            string url = "https://graph.facebook.com/" + uid + "/picture?access_token=" + oAuth.Token;
            return oAuth.WebRequest(oAuthFacebook.Method.GET, url, string.Empty);
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

        public List<Album> GetAlbums(string offset)
        {
            string url = "";
            //Si la llamo por primera vez traerá los últimos post sino trae los anteriores con una URL que 
            //ya tengo en sesión y además guarda la URL siguiente 

            url = "https://graph.facebook.com/me/albums?access_token=" + oAuth.Token;

 /*           if (Convert.ToInt32(offset) > 0)
            {

                url = (string)System.Web.HttpContext.Current.Session["url_next_posts"];
            }
 */
            string json = oAuth.WebRequest(oAuthFacebook.Method.GET, url, string.Empty);

            List<Album> albumes = new List<Album>();

            JObject o = JObject.Parse(json);

            int max = o.SelectToken("data").Count();
            for (int i = 0; i < max; i++)
            {
                Album album = new Album();
                album.id = (string)o.SelectToken("data[" + i + "].id");
                album.name = (string)o.SelectToken("data[" + i + "].name");
                album.link = (string)o.SelectToken("data[" + i + "].link");
                album.cover = getLinkCover((string)o.SelectToken("data[" + i + "].cover_photo"));
                album.cantFotos = (int)o.SelectToken("data[" + i + "].count");
                
                albumes.Add(album);
            }

            return albumes;

        }

        public List<Foto> GetFotos(string albumid, string offset)
        {
            string url = "";
            url = "https://graph.facebook.com/"+albumid+"/photos?limit=30&access_token=" + oAuth.Token;

            if (albumid == "mas_fotos")
            {
                url = (string)System.Web.HttpContext.Current.Session["url_next_posts"];
            }

            if (url == null)
            {
                return new List<Foto>();
            }
            string json = oAuth.WebRequest(oAuthFacebook.Method.GET, url, string.Empty);
            List<Foto> fotos = new List<Foto>();

            JObject o = JObject.Parse(json);
            System.Web.HttpContext.Current.Session["url_next_posts"] = (string)o.SelectToken("paging.next");
            
            int max = o.SelectToken("data").Count();
            for (int i = 0; i < max; i++)
            {
                Foto foto = new Foto();
                foto.albumid = albumid;
                foto.id = (string)o.SelectToken("data[" + i + "].id");
                foto.name = (string)o.SelectToken("data[" + i + "].name");
                foto.picture = (string)o.SelectToken("data[" + i + "].picture");
                foto.source = (string)o.SelectToken("data[" + i + "].source");
                foto.height = (int)o.SelectToken("data[" + i + "].height");
                foto.width = (int)o.SelectToken("data[" + i + "].width");
                foto.link = (string)o.SelectToken("data[" + i + "].link");

                fotos.Add(foto);
            }
            return fotos;
        }

        private string getLinkCover(string p)
        {
          string  url = "https://graph.facebook.com/" + p + "?access_token=" + oAuth.Token;
          string json = oAuth.WebRequest(oAuthFacebook.Method.GET, url, string.Empty);
          JObject jO = JObject.Parse(json);
          return (string)jO.SelectToken("picture");
        }
    }
    
}