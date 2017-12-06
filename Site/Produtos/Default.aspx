﻿<%@ Page Title="Produtos" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Site.Produtos.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            <div class="col-lg-4"></div>
            <a href="Tipos.aspx">
                <div id="divPedidos" runat="server" class="col-lg-2 color-swatch bg-warning text-white text-center">
                    <div class="col-lg-12 iconMain">

                        <span class="fa fa-bars fa-5x" aria-hidden="true"></span>

                    </div>
                    <div class="col-lg-12 iconMain"><span class="text-center ">TIPOS</span></div>
                </div>
            </a>
            <a href="Produtos.aspx">
                <div id="divClientes" runat="server" class="col-lg-2 color-swatch bg-warning text-white text-center">
                    <div class="col-lg-12 iconMain">
                        <span class="glyphicon glyphicon-cutlery fa-5x" aria-hidden="true"></span>
                    </div>
                    <div class="col-lg-12 iconMain"><span class="text-center">PRODUTOS</span></div>

                </div>
            </a>
            <div class="col-lg-4"></div>
        </div>

        <div class="row">
            <div class="col-lg-4"></div>
            <a href="opcionaisMain.aspx">
                <div id="div1" runat="server" class="col-lg-2 color-swatch bg-warning text-white text-center">
                    <div class="col-lg-12 iconMain">

                        <span class="fa fa-plus fa-5x" aria-hidden="true"></span>

                    </div>
                    <div class="col-lg-12 iconMain"><span class="text-center ">OPCIONAIS</span></div>



                </div>
            </a>
            <div id="div2" runat="server" class="col-lg-2">
                <!-- ANOTHER ITEM-->

            </div>
            <div class="col-lg-4"></div>
        </div>

    </div>
</asp:Content>
