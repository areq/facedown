using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using facedown.Models;

namespace facedown
{
    public class AuthFilter : ActionFilterAttribute
    {
        FacebookAPI FB = (FacebookAPI)ServiceFactory.GetImpl("FB");
        public override void  OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Verifico que la variable de sesión isLogued exista sino la "creo"
            if (System.Web.HttpContext.Current.Session["isLogued"] == null)
                System.Web.HttpContext.Current.Session["isLogued"] = false;
            //Si no estoy logueado y no tengo el parametro código, entonces redirecciono para
            //loguarse en facebook
            if (filterContext.HttpContext.Request["code"] == null && (bool)System.Web.HttpContext.Current.Session["isLogued"] == false)
            {
               //Lo mando a la página de login de facebook
               filterContext.Result = new RedirectResult(FB.GetAuthorizationLink());
            }
            else
            {
                //Si tengo el parametro código entonces tendré que traer el Token enviandole a facebook el código
                //Y de está forma ya tengo el Token para trabajar.
                if ((bool)System.Web.HttpContext.Current.Session["isLogued"] == false)
                {
                    System.Web.HttpContext.Current.Session["isLogued"] = true;
                    FB.EstablecerConexion(filterContext.HttpContext.Request["code"]);
                }
            }
        }
    }
}