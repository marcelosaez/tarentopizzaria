<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Novo.aspx.cs" Inherits="Site.Clientes.Novo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="/Content/bootstrap.css" rel="stylesheet" />
    <link href="/Content/clientes.css" rel="stylesheet" />

    <script src="/Scripts/jquery-1.9.1.min.js"></script>
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
            $(".alert-message").delay(8000).fadeOut("slow", function () { $(this).remove(); });
        }
        //newAlert('success', 'Oh yeah!');

    </script>
</head>
 <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ScriptManager>
<body>
    <div class="container">
  <div class="row" id="container">
    <div class="col-md-4"></div>
     <div class="col-md-4 text-center">
         <div id="alert-area" style="width:420px;"></div>
     <div class="formulario formulario-novo">   
         <h3>Cliente</h3>
         <section class="login-form-novo">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Visible="false" />
                <asp:Panel ID="panelFormulario" runat="server" DefaultButton="cmdSalvar">
                
                <div class="col-md-12">
                     <asp:TextBox ID="txtNome" runat="server" EnableTheming="True" MaxLength="50"  CssClass="form-control input-lg espaco" placeholder="Nome" ></asp:TextBox>
                     <asp:RequiredFieldValidator ID="reqNome" runat="server" ControlToValidate="txtNome" ErrorMessage="Informe o nome" EnableClientScript="True" Display="Dynamic">* Nome Obrigatório</asp:RequiredFieldValidator>
                </div>
                <div class="col-md-12">
                    <asp:TextBox ID="txtEndereco" runat="server" EnableTheming="True" MaxLength="50" CssClass="form-control input-lg espaco" placeholder="Endereço" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqEndereco" runat="server" ControlToValidate="txtEndereco" ErrorMessage="Informe o endereço" EnableClientScript="True" Display="Dynamic">* Endereço Obrigatório</asp:RequiredFieldValidator>
                </div>
                <div class="col-md-12">
                    <asp:TextBox ID="txtNumero" runat="server" EnableTheming="True" MaxLength="50"  CssClass="form-control input-lg espaco" placeholder="Número" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqNumero" runat="server" ControlToValidate="txtNumero" ErrorMessage="Informe o número" EnableClientScript="True" Display="Dynamic">*</asp:RequiredFieldValidator>
                </div>
                <div class="col-md-12">
                    <asp:TextBox ID="txtEmail" runat="server" EnableTheming="True" MaxLength="50"  CssClass="form-control input-lg espaco" placeholder="E-mail"  ></asp:TextBox>
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtDDDRES" runat="server" EnableTheming="True" MaxLength="2"  CssClass="form-control input-lg espaco" placeholder="11" Text="11"  ></asp:TextBox>
               </div>
                <div class="col-xs-9">
                    <asp:TextBox ID="txtTELRES" runat="server" EnableTheming="True" MaxLength="9"  CssClass="form-control input-md espaco" placeholder="Telefone Residêncial" Text=""  ></asp:TextBox>
                </div> 
                <div class="col-xs-3">        
                <asp:TextBox ID="txtDDDCEL" runat="server" EnableTheming="True" MaxLength="2" CssClass="form-control input-lg espaco" placeholder="11" Text="11"  ></asp:TextBox>
               </div>
               <div class="col-xs-9">
                <asp:TextBox ID="txtCEL" runat="server" EnableTheming="True" MaxLength="9"  CssClass="form-control  espaco" placeholder="Telefone Celular" Text=""  ></asp:TextBox>
               </div> 

                <%--<asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Informe o e-mail" EnableClientScript="True" Display="Dynamic"></asp:RequiredFieldValidator>--%>
            
                
                    <asp:CheckBox ID="cbxAtivo" runat="server" Visible="false" EnableTheming="True"  Checked="false" Text="Ativo?" >
                  
                    </asp:CheckBox>
                

                    <div class="col-xs-6">
                            <asp:Button ID="cmdLimpar" runat="server" Text="Limpar" OnClick="cmdLimpar_Click" CssClass="btn btn-lg btn-danger btn-block " CausesValidation="false" UseSubmitBehavior="false"    />&nbsp;
                         </div>

                    <div class="col-xs-6">
                        
                            <asp:Button ID="cmdSalvar" runat="server" Text="Salvar" OnClick="cmdSalvar_Click" CssClass="btn btn-lg btn-primary btn-block botaoSalvar-novo" />
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
</body>
</form>
</html>
