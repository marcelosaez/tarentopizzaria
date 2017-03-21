<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Site.FeriadosM.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/calendario.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row">
                 <div class="col-md-12 text-center"> 
                     <h2 class="titulo">Feriados - <asp:Label ID="lblAno" runat="server"></asp:Label> </h2>
                </div>
        </div>
       
         <asp:Repeater ID="rptFeriados" runat="server">
                <HeaderTemplate>
                    <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-2 text-center cabecalho">Dia </div>
                    <div class="col-md-3 text-center cabecalho">Feriado</div>
                    <div class="col-md-2 text-left cabecalho">Dia da Semana</div>
                    <div class="col-md-2"></div>
                </div> 
                </HeaderTemplate>
            <ItemTemplate>
              <div class="row">
                 <div class="col-md-3"></div>
                 <div class="col-md-2 text-center espaco margimDireita"><%# DataBinder.Eval(Container.DataItem,"Dt", "{0:dd/MM/yyyy }") %> </div>
                 <div class="col-md-3 text-center espaco"><%# DataBinder.Eval(Container.DataItem,"Desc" ) %></div>
                 <div class="col-md-2 text-center espaco"><%# DataBinder.Eval(Container.DataItem,"DiaSemana" ) %> </div>
                 <div class="col-md-2"></div>
             </div>         
        </ItemTemplate>
        </asp:Repeater>
        </div>

        

</asp:Content>
