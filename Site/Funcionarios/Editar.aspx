<%@ Page Title="Tarento" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Editar.aspx.cs" Inherits="Site.Funcionarios.Editar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/bootstrap.css" rel="stylesheet" />
    <link href="/Content/funcionarios.css" rel="stylesheet" />

    <script src="/Scripts/jquery-1.9.0.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
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

        function newAlert(type, message) {
            $("#alert-area").append($("<div class='alert-message " + type + " fade in' data-alert><p> " + message + " </p></div>"));
            $(".alert-message").delay(3000).fadeOut("slow", function () { $(this).remove(); });
        }
        //newAlert('success', 'Oh yeah!');

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



<div class="container">
  <div class="row" id="container">
    <div class="col-md-4"></div>
     <div class="col-md-4 text-center">
         <div id="alert-area"></div>
     <div class="formulario">   
         <h1>Funcionário</h1>
         <section class="login-form">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Visible="false" />
                <asp:Panel ID="panelFormulario" runat="server" DefaultButton="cmdSalvar">
                
        
                <asp:TextBox ID="txtNome" runat="server" EnableTheming="True" MaxLength="50" Width="350px" CssClass="form-control input-lg espaco" placeholder="Nome" ></asp:TextBox>

                <asp:RequiredFieldValidator ID="reqNome" runat="server" ControlToValidate="txtNome" ErrorMessage="Informe o nome" EnableClientScript="True" Display="Dynamic">* Nome Obrigatório</asp:RequiredFieldValidator>
            
                <asp:TextBox ID="txtLogin" runat="server" EnableTheming="True" MaxLength="50" Width="350px" CssClass="form-control input-lg espaco" placeholder="Login" ></asp:TextBox>

                <asp:RequiredFieldValidator ID="reqLogin" runat="server" ControlToValidate="txtLogin" ErrorMessage="Informe o login" EnableClientScript="True" Display="Dynamic">* Login Obrigatório</asp:RequiredFieldValidator>
            
                <asp:TextBox ID="txtEmail" runat="server" EnableTheming="True" MaxLength="50" Width="350px" CssClass="form-control input-lg espaco" placeholder="Email" ></asp:TextBox>

                <%--<asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Informe o email" EnableClientScript="True" Display="Dynamic">*</asp:RequiredFieldValidator>--%>
            
                <asp:TextBox ID="txtSenha" runat="server" EnableTheming="True" MaxLength="50" Width="350px" CssClass="form-control input-lg espaco" placeholder="Senha" TextMode="Password" ></asp:TextBox>

                <asp:RequiredFieldValidator ID="reqSenha" runat="server" ControlToValidate="txtSenha" ErrorMessage="Informe a senha" EnableClientScript="True" Display="Dynamic">*Senha Obrigatória</asp:RequiredFieldValidator>
            
                <asp:DropDownList ID="ddlTipos" runat="server" CssClass="form-control espaco" Width="350px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqTipo" runat="server" ErrorMessage="Selecione o tipo" 
                    Display="Dynamic" ControlToValidate="ddlTipos">* Selecione o Tipo</asp:RequiredFieldValidator>
            
                <div class="col-md-12 text-left cbxFuncionario">
                    <asp:CheckBox ID="cbxAtivo" runat="server" Visible="false" EnableTheming="True"  Checked="false" Text="Ativo?" >
                  
                    </asp:CheckBox>
                </div>  


                    <div class="col-md-12">
                        <div class="col-md-6">
                            <asp:Button ID="cmdVoltar" runat="server" Text="Voltar" OnClick="cmdVoltar_Click" CssClass="btn btn-lg btn-danger btn-block botaoVoltar" CausesValidation="false" UseSubmitBehavior="false" />&nbsp;
                         </div>
                        <div class="col-md-6">
                            <asp:Button ID="cmdSalvar" runat="server" Text="Salvar" OnClick="cmdSalvar_Click" CssClass="btn btn-lg btn-primary btn-block botaoSalvar" />
                        </div>
                    </div>
                    <div class="row">

                    </div>
        
    </asp:Panel>
              
        </section>
     </div>
    </div>
    <div class="col-md-4"></div>
  </div>
</div>


</asp:Content>
