<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<facedown.Controllers.HomePageViewModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Albums de <%= Model.User.nombre_completo %></title>
    <script type="text/javascript" src="/Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/Scripts/ajax_loco.js"></script>
</head>
<body>
    <div id="content">
                 
                      <img id="img_perfil" src="http://graph.facebook.com/<%= Model.User.id %>/picture?type=large" alt="Foto de perfil de <%= Model.User.nombre_completo %>" />
                      <br />
                      <a href="<%= Model.User.link %>" title="Ver perfil completo de <%= Model.User.nombre_completo %>"><%= Model.User.nombre_completo %></a>
 
                
                        <div id="listado_album">
                            <% foreach (var album in Model.Albums) { %>
                                <% Html.RenderPartial("Album", album);%>
                            <% } %>
                        </div>
                
    </div>

</body>
</html>