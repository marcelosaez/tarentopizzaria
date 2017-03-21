<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Topo.ascx.cs" Inherits="Site.UserControls.Topo" %>
<header class="topo">
<div id="topo">
    <div class="container">
        <div class="row gray">
            <div class="col-lg-1">
                <div class="logo">
                    <a href="<%= ResolveUrl("~/Main.aspx") %>">
                         <asp:Image ID="imgLogo" ImageUrl="~/Images/logo.jpg" runat="server"  Width="81px" Height="108px"  ToolTip="Menu" />
                    </a>
                        </div>
            </div>
            <div class="col-lg-11 login">
                <div class="pull-right">
                    <i class="fa fa-user fa-2x fa-fw" aria-hidden="true"></i>
                    <asp:Label ID="lblUsuario" runat="server" Text=""></asp:Label>
                </div>
            </div>
             <div class="col-lg-11 loginSair">
                <div class="pull-right">
                    <a href="<%= ResolveUrl("~/Sair.aspx") %>">
                     <i class="fa fa-sign-out fa-2x fa-fw" aria-hidden="true"></i>
                    <asp:Label ID="lblSair" runat="server" Text="Sair do sistema"></asp:Label></a>
                 </div>
             </div>
        </div>
        
    </div>  
    
</div>
</header>
