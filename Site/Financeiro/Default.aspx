<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Site.Financeiro.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-lg-4"></div>
            <div class="col-lg-4">
                <div class="text-center" id="alert-area" style="margin-left:-14px; width:398px;"></div>
            </div>
            <div class="col-lg-4"></div>
        </div>
    </div>
    <div class="container">
        <div class="row">
             <div class="col-lg-4"></div>
             <a href="Fechamento.aspx?fechamento=dia">
                <div id="divPedidos" runat="server" class="col-lg-2 color-swatch text-white text-center bg-money">
                     <div class="col-lg-12 iconMain">
                         
                         <span class="fa fa-arrow-circle-down fa-5x" aria-hidden="true"></span>

                     </div>
                    <div class="col-lg-12 iconMain"><span class="text-center "> DIA</span></div>
                     
                     
                   
                </div>
            </a>
                <a href="Fechamento.aspx?fechamento=periodo">
                <div id="divClientes" runat="server"  class="col-lg-2 color-swatch text-white text-center bg-money">
                    <div class="col-lg-12 iconMain">
                           <span class="fa fa-calendar fa-5x" aria-hidden="true"></span>
                    </div>
                    <div class="col-lg-12 iconMain"><span class="text-center"> PERÍODO</span></div>

                </div>
                </a>
             <div class="col-lg-4"></div>
        </div>

    </div>
</asp:Content>
