<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditarProdutoTipos.aspx.cs" Inherits="Site.Produtos.EditarProdutoTipos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/bootstrap.css" rel="stylesheet" />
    <link href="/Content/clientes.css" rel="stylesheet" />

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
            $(".alert-message").delay(8000).fadeOut("slow", function () { $(this).remove(); });
        }
        //newAlert('success', 'Oh yeah!');

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
  <div class="row" id="container">
    <div class="col-md-4"></div>
     <div class="col-md-4 text-center">
         <div id="alert-area" style="width:420px;"></div>
     <div class="formulario">   
         <h1>Cliente</h1>
         <section class="login-form">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Visible="false" />
                <asp:Panel ID="panelFormulario" runat="server" DefaultButton="cmdSalvar">
                
                <div class="col-md-12">
                     <asp:TextBox ID="txtTipo" runat="server" EnableTheming="True" MaxLength="50"  CssClass="form-control input-lg espaco" placeholder="Tipo" ></asp:TextBox>
                     <asp:RequiredFieldValidator ID="reqTipo" runat="server" ControlToValidate="txtTipo" ErrorMessage="Informe o tipo" EnableClientScript="True" Display="Dynamic">* Tipo Obrigatório</asp:RequiredFieldValidator>
                </div>
                
                <div class="col-md-12 text-left cbxCliente">
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
