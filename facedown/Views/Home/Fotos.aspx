<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<facedown.Controllers.FotosViewModel>" %>

<ul>
<% foreach (var foto in Model.Fotos) { %>
    <li><!-- name="albums[<%= foto.albumid %>][<%= foto.id %>-<%= foto.name %>]" --> 
        <img src="<%= foto.source %>" class="<%= foto.height%> <%= foto.width%>" alt="<%= foto.name %>" />
        <div><input class="foto-checkbox" value="<%= foto.source %>" name="<%= foto.name %>-<%= foto.id %>" type="checkbox" /></div>
    </li>
<% } %>
</ul>