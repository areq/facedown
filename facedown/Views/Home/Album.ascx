<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<facedown.Models.Album>" %>



     <img class="album-tapa" src="<%= Model.cover %>" alt="Tapa del album" />      
     <div class="album-fotos">
        <div id="<%= Model.id %>" class="verfotos">Si clickeas aca vas a ver las fotos purrete!</div><br /><br /><br />
        <a href="fotos/<%= Model.id %>" title="Ver las fotos del album <%= Model.name %>">Ver las fotos del album <%= Model.name%></a>         
		<label><%=Model.cantFotos%></label>
     </div>     
     <br />