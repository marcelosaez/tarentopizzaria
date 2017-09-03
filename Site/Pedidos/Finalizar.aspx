<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Finalizar.aspx.cs" Inherits="Site.Pedidos.Finalizar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/bootstrap.css" rel="stylesheet" />
    <link href="/Content/finalizar.css" rel="stylesheet" />

    <script src="/Scripts/jquery-1.9.0.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
    <script src="/Scripts/jquery.maskMoney.js"></script>
    <script src="/Scripts/bootbox.min.js"></script>
    <script src="/Scripts/jquery-1.9.0.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">

        function newAlert(type, message) {
            $("#alert-area").append($("<div class='alert-message " + type + " fade in' data-alert><p> " + message + " </p></div>"));
            $(".alert-message").delay(6000).fadeOut("slow", function () { $(this).remove(); });
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row" id="container">
            <div class="col-md-4"></div>
            <div class="col-md-4 text-center">
                <div class="formulario">
                    <h2>Finalizar</h2>
                </div>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-3">
                <div id="alert-area"></div>

            </div>

        </div>
    </div>

    <asp:Repeater ID="rptItems" runat="server">
        <HeaderTemplate>
            <table>
                <thead>
                    <tr>
                        <th>Qtd</th>
                        <th>Tipo</th>
                        <th>Opção</th>
                        <th>Sabor</th>
                        <th>Obs</th>
                        <th>Valor</th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("qtd")%></td>
                <td><%# Eval("tipo") %></td>
                <td><%# Eval("opcao")%></td>
                <td><%# Eval("sabor")%></td>
                <td><%# Eval("obs") %></td>
                <td><%# Eval("valor")%></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody>
     <tfoot>
         <tr style="border-top: solid 2px #006DCC; background-color: #006DCC;">
             <td colspan="5" style="text-align: right; font-weight: bold; color: #fff">Total</td>
             <td style="text-align: right; font-weight: bold; color: #fff"><strong>R$
                 <asp:Label ID="lblTotal" runat="server"></asp:Label>
             </strong></td>
         </tr>
     </tfoot>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <div class="row">
        <div class="col-md-4"></div>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlPagamento" runat="server" CssClass="form-control espaco">
            </asp:DropDownList>
        </div>
        <div class="col-md-4"></div>
    </div>
    <div class="row">
        <div class="col-md-4">&nbsp;</div>
        <div class="col-md-4">&nbsp;</div>
        <div class="col-md-4">&nbsp;</div>
    </div>

    <div class="row">
        <div class="col-md-4"></div>
        <div class="col-md-4">

            <div class="col-md-6">
                <asp:Button ID="cmdVoltar" runat="server" Text="Voltar" OnClick="cmdVoltar_Click" CssClass="btn btn-lg btn-danger btn-block botaoVoltar" CausesValidation="false" UseSubmitBehavior="false" />&nbsp;
            </div>
            <div class="col-md-6">
                <asp:Button ID="cmdFinalizar" runat="server" Text="Finalizar" OnClick="cmdFinalizar_Click" CssClass="btn btn-lg btn-primary btn-block botaoSalvar" />
            </div>
        <div class="col-md-3"></div>

        </div>
        <div class="col-md-4"></div>

    </div>


</asp:Content>
