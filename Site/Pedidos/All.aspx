<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="All.aspx.cs" Inherits="Site.Pedidos.All" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-4 text-center">
                <div class="formulario">
                    <h2>Pedidos do dia</h2>
                </div>
            </div>
            <div class="col-md-4"></div>
            <div class="col-md-12"></div>
        </div>
        <div class="row voffset6">
            <div class="col-md-12">
                <Wizard:WizardGridView ID="gvListaPedidos" runat="server" DataSourceID="SourceData" DataKeyNames="idPedido" 
                    EmptyDataText="Não há pedidos cadastrados até o momento." AutoGenerateColumns="true" CssClass="table table-hover" 
                    ClientIDMode="Static" AutoGenerateEditButton="true" PageSize="20" AllowPaging="true"
                    OnRowCommand="gvListaPedidos_RowCommand" 
                    >
                </Wizard:WizardGridView>
            </div>
        </div>
    <asp:ObjectDataSource ID="SourceData" TypeName="BLL.Pedidos.PedidosBLL" SelectMethod="obterDataSetData" runat="server" OnSelecting="SourceData_Selecting" >
        <SelectParameters>
        <asp:Parameter Name="dataIni" Type="DateTime" />
        <asp:Parameter Name="dataFim" Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
