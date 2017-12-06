<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Site.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function newAlert(type, message) {
            $("#alert-area").append($("<div class='alert-message " + type + " fade in' data-alert><p> " + message + " </p></div>"));
            $(".alert-message").delay(4000).fadeOut("slow", function () { $(this).remove(); });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="row">
            <div class="col-lg-4"></div>
            <div class="col-lg-4">
                <div class="text-center" id="alert-area" style="margin-left: -14px; width: 398px;"></div>
            </div>
            <div class="col-lg-4"></div>
        </div>
    </div>

    <div class="content">
        <div class="row">
            <div class="col-lg-3"></div>
            <a href="Pedidos/Main.aspx">
                <div id="divPedidos" runat="server" class="col-lg-2 color-swatch bg-primary text-white text-center">
                    <div class="col-lg-12 iconMain"><i class="fa fa-cart-plus fa-5x" aria-hidden="true"></i></div>
                    <div class="col-lg-12 iconMain"><span class="text-center">PEDIDOS</span></div>
                </div>
            </a>
            <a href="Clientes/Default.aspx">
                <div id="divClientes" runat="server" class="col-lg-2 color-swatch bg-success text-white text-center">
                    <div class="col-lg-12 iconMain"><i class="fa fa-user fa-5x" aria-hidden="true"></i></div>
                    <div class="col-lg-12 iconMain"><span class="text-center">CLIENTES</span></div>
                </div>
            </a>
            <a href="Financeiro/Default.aspx">
                <div id="divFinanceiro" runat="server" class="col-lg-2 color-swatch text-white text-center bg-money">
                    <div class="col-lg-12 iconMain"><i class="fa fa-dollar fa-5x" aria-hidden="true"></i></div>
                    <div class="col-lg-12 iconMain"><span class="text-center">CAIXA</span></div>

                </div>
            </a>

            <div class="col-lg-3"></div>
        </div>
        <div class="row ">
            <div class="col-lg-3"></div>
            <a href="Produtos/Default.aspx">
                <div id="divProdutos" runat="server" class="col-lg-2 color-swatch bg-warning text-white text-center">
                    <div class="col-lg-12 iconMain"><i class="fa fa-cutlery fa-5x" aria-hidden="true"></i></div>
                    <div class="col-lg-12 iconMain"><span class="text-center">PRODUTOS</span></div>

                </div>
            </a>
            <a href="Funcionarios/Default.aspx">
                <div id="divFuncionarios" runat="server" class="col-lg-2 color-swatch bg-danger text-white text-center">
                    <div class="col-lg-12 iconMain"><i class="fa fa-users fa-5x" aria-hidden="true"></i></div>
                    <div class="col-lg-12 iconMain"><span class="text-center">FUNCIONARIOS</span></div>
                </div>
            </a>
            
            <div class="col-lg-3"></div>
        </div>
    </div>
</asp:Content>
