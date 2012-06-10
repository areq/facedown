<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<facedown.Models.Album>" %>

      
<li>
    <div id="<%= Model.id %>" class="verfotos">
        <img class="album-tapa" src="<%= Model.cover %>" alt="Tapa del album" />     
        <label><%=Model.cantFotos%></label>
    </div>
</li>     