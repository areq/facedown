<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<facedown.Models.Album>" %>

      
<li>
    <div id="<%= Model.id %>" nombre="<%= Model.name %>" class="verfotos">
        <img class="album-tapa" id="<%= Model.name %>" src="<%= Model.cover %>" alt="Tapa del album" />     
        <label><%=Model.cantFotos%></label>
    </div>
</li>     