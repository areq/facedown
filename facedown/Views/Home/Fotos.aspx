<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<facedown.Controllers.FotosViewModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Albums de <%= Model.User.nombre_completo %></title>

<script type="text/javascript" src="/Scripts/jquery-1.4.1.min.js"></script>

</head>
<body>

    <div id="maincontainer">
                   
            <div id = "corpus">
                 
                <div id="leftcolumn">
                      <img id="img_perfil" src="http://graph.facebook.com/<%= Model.User.id %>/picture?type=large" alt="Foto de perfil de <%= Model.User.nombre_completo %>" />
                      <br />
                </div>
                
                <div id="contentwrapper">
                
                    <div id="contentcolumn">

                        <div id="listado_fotos">
                            <% foreach (var foto in Model.Fotos) { %>
                                <div>
                                    <img src="<%= foto.source %>" height="<%= foto.height%>" width="<%= foto.width%>" alt="<%= foto.name %>" />
                                 </div>
                            <% } %>
                            <div id="ajax-loader" class="ajax-loader" style="display:none;"><img src="/Content/ajax-loader.gif" alt="Loading..." /></div>
                            <div id="mas_posts" class="mas_posts">
                                 <label id="offset-25" class="mas_posts_link" style="font-weight:normal" >Publicaciones más antiguas</label>
                                                                     
                            </div>

                        </div>
                    </div>
                </div>
            </div>   
                
        <div  id="footer">
            <p>Grupo 4.</p>
        </div>

    </div>
    
</body>
</html>

