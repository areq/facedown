<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<facedown.Controllers.HomePageViewModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Albums de <%= Model.User.nombre_completo %></title>
    <script type="text/javascript" src="/Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/Scripts/ajax_loco.js"></script>
    <link href="../../Content/Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="content">
                 <div id="user-datos">
                      <img id="img_perfil" src="http://graph.facebook.com/<%= Model.User.id %>/picture?type=large" alt="Foto de perfil de <%= Model.User.nombre_completo %>" />
                      <span><%= Model.User.nombre_completo %></span>
                 </div>       
                        <div id="fotos-list">
                            <h2>Eleg� las fotos y descargalas en tu computadora en un solo paso.</h2>
                        </div>                
    </div>
    <div id="list-album">
        <ul>
            <% foreach (var album in Model.Albums) { %>
                <% Html.RenderPartial("Album", album);%>
            <% } %>
        </ul>
    </div>
</body>
</html>