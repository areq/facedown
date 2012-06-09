<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FacebookProject.Models.Album>" %>



 <img class = "album-tapa" src="<%= Model.cover %>" alt="Tapa del album" />      
     <div class = "perfilMensaje">
        <a href="fotos/<%= Model.id %>" id="<%= Model.id %>" title="Ver las fotos del album <%= Model.name %>">Ver las fotos del album <%= Model.name%></a>         
		<label><%=Model.cantFotos%></label>
     </div>     
     <br /> 