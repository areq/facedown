<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<facedown.Controllers.FotosViewModel>" %>

<ul>
<% foreach (var foto in Model.Fotos) { %>
    <li>
        <img src="<%= foto.source %>" class="<%= foto.height%> <%= foto.width%>" alt="<%= foto.name %>" />
    </li>
<% } %>
</ul>