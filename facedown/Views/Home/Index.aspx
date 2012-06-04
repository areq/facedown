<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FacebookProject.Controllers.HomePageViewModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Albums de <%= Model.User.nombre_completo %></title>
<link href="/Content/muro.css" rel="stylesheet" />
<script type="text/javascript" src="/Scripts/jquery-1.4.1.min.js"></script>
<script type="text/javascript" src="/Scripts/jquery.watermark.min.js"></script>
<script type="text/javascript" src="/Scripts/jquery.elastic.js"></script>
<script type="text/javascript" src="/Scripts/jquery.livequery.js"></script>
<script type="text/javascript" src="/Scripts/jquery.infinitescroll.min.js"></script>
<link href="/facebox/facebox.css" media="screen" rel="stylesheet" type="text/css"/>
<script src="/facebox/facebox.js" type="text/javascript"></script> 
<script type="text/javascript" src="/Scripts/facebook.js"></script>
</head>
<body>

    <div id="maincontainer">
    
        <div id="header">
            <img alt="" style= "width:200px" src= "../../Logo facebook.jpg" />            
        </div> 
        <br />
        <br />
               
            <div id = "corpus">
                 
                <div id="leftcolumn">
                      <img id="img_perfil" src="http://graph.facebook.com/<%= Model.User.id %>/picture?type=large" alt="Foto de perfil de <%= Model.User.nombre_completo %>" />
                      <br />
                      <a href="<%= Model.User.link %>" title="Ver perfil completo de <%= Model.User.nombre_completo %>"><%= Model.User.nombre_completo %></a>
                </div>
                
                <div id="contentwrapper">
                        <div id="listado_posts">
                            
                            <%= Model.Albums.id %>
                            <!--<div id="ajax-loader" class="ajax-loader" style="display:none;"><img src="/Content/ajax-loader.gif" alt="Loading..." /></div>
                            <div id="mas_posts" class="mas_posts">
                                 <label id="offset-25" class="mas_posts_link" style="font-weight:normal" >Publicaciones más antiguas</label>                                         
                            </div>-->
                        </div>
                </div>
                
            </div>   
                
    <div  id="footer">
        <p>Grupo 4.</p>
    </div>

    </div>
    
</body>
</html>

