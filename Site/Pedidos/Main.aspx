<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Site.Pedidos.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content">
        <div class="row voffset6">
            <div class="col-md-4"></div>
            <a href="/Pedidos/Default.aspx">
                <div id="divPedidos" runat="server" class="col-md-2 color-swatch bg-primary text-white text-center">
                    <div class="col-lg-12 iconMain"><i class="fa fa-cart-plus fa-5x" aria-hidden="true"></i></div>
                    <div class="col-lg-12 iconMain"><span class="text-center">NOVO PEDIDO</span></div>

                </div>
            </a>
            <a href="/Pedidos/All.aspx">
                <div id="divClientes" runat="server" class="col-md-2 color-swatch bg-success text-white text-center">
                    <div class="col-lg-12 iconMain"><i class="fa fa-list fa-5x" aria-hidden="true"></i></div>
                    <div class="col-lg-12 iconMain"><span class="text-center">TODOS PEDIDOS</span></div>
                </div>
            </a>
            <div class="col-md-4"></div>
        </div>
    </div>

</asp:Content>
