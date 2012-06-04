<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FacebookProject.Models.Post>" %>



 <img class = "album-tapa" src="http://www.facebook.com/<%= Model.album.cover %>" alt="Tapa del album" />      
     <div class = "perfilMensaje">
        <a href="<%= Model.album.id %>" id="<%= Model.album.id %>" title="Ver las fotos del album <%= Model.album.name %>">Ver las fotos del album <%= Model.album.name %></a>         
        <div class="post_caption">
			<a href="<%=Model.album.link %>"><%= Model.album.name%></a>
		</div>
		<label><%=Model.album.cantFotos%></label>
     </div>     
     <br /> 