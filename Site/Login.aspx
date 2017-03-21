<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Site.Login" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="robots" content="noindex">

    <title>Tarento Pizzaria</title>
        <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/login.css" rel="stylesheet" />

    <script src="Scripts/jquery-1.9.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">
        window.alert = function(){};
        var defaultCSS = document.getElementById('bootstrap-css');
        function changeCSS(css){
            if(css) $('head > link').filter(':first').replaceWith('<link rel="stylesheet" href="'+ css +'" type="text/css" />'); 
            else $('head > link').filter(':first').replaceWith(defaultCSS); 
        }
        $( document ).ready(function() {
          var iframe_height = parseInt($('html').height()); 
          //window.parent.postMessage( iframe_height, 'http://bootsnipp.com');
        });
    </script>
</head>
<body>
<div class="container">
  
  <div class="row" id="pwd-container">
    <div class="col-md-4"></div>
    
    <div class="col-md-4">
      <section class="login-form">
         <form id="form1" runat="server" action="#" role="login">
        
          <img src="Images/logo.jpg" class="img-responsive" alt="" />
          <input runat="server" id="txtLogin" type="text" name="txtLogin" placeholder="login" required class="form-control input-lg" value="" />
          
          <input runat="server" id="txtSenha" type="password" class="form-control input-lg" placeholder="senha" required="" />
          
          
          <%--<div class="pwstrength_viewport_progress invisible"></div>--%>
          
          <div runat="server" id="divMSG" class="alert-danger"></div>
          
          <asp:Button ID="cmdLogin" runat="server" Text="Entrar" OnClick="cmdLogin_click" class="btn btn-lg btn-primary btn-block" />
          <%--<button runat="server" id="btnLogar" type="submit" name="go" class="btn btn-lg btn-primary btn-block">Entrar</button>
          
             <div>
            <a href="#">Create account</a> or <a href="#">reset password</a>
          </div>--%>
          
        </form>
        
        <div class="form-links invisible">
          <a href="#">www.tarentopizzaria.com.br</a>
        </div>
      </section>  
      </div>
      
      <div class="col-md-4"></div>
      

  </div>
  
  
  
</div>
    <script src="Scripts/login.js"></script>
</body>
</html>
