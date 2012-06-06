<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FacebookProject.Models.Album>" %>



 <img class = "album-tapa" src="<%= Model.cover %>" alt="Tapa del album" />      
     <div class = "perfilMensaje">
        <a href="<%= Model.id %>" id="<%= Model.id %>" title="Ver las fotos del album <%= Model.name %>">Ver las fotos del album <%= Model.name%></a>         
        <div class="post_caption">
			<a href="<%=Model.link %>"><%= Model.name%></a>
		</div>
		<label><%=Model.cantFotos%></label>
     </div>     
     <br /> 