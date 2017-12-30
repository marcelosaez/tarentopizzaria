<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Site.Clientes.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-8">
                <h3>
                    <a href="Editar.aspx">Adicionar novo Cliente</a>
                </h3>
            </div>
            <div class="col-md-3">

                <asp:Panel ID="panelBuscar" runat="server" DefaultButton="cmdBusca">
                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:TextBox ID="txtBusca" runat="server" MaxLength="20" CssClass="form-control espaco" placeholder="Buscar cliente"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <div class="btn btn-default text-left">
                                <asp:LinkButton ValidationGroup="panelBuscar" ID="cmdBusca" runat="server" Text="<i class='fa fa-search fa-fw fa-lg' aria-hidden='true'></i>" CssClass="botao" OnClick="cmdBusca_OnClick" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class="col-md-1"></div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <Wizard:WizardGridView ID="gvListaClientes" runat="server" DataSourceID="SourceData" DataKeyNames="id" OnRowCommand="gvListaClientes_RowCommand"
                    EmptyDataText="Não há clientes cadastrados até o momento." OnRowDataBound="gvListaClientes_RowDataBound" AutoGenerateColumns="true"
                    CssClass="table table-hover" ClientIDMode="Static" AutoGenerateEditButton="true" PageSize="10"
                    WizardCustomPager="True" AllowPaging="true" PagingVerticalAlignment="Bottom">
                </Wizard:WizardGridView>
            </div>
        </div>
    </div>
    <asp:ObjectDataSource ID="SourceData" TypeName="BLL.Cliente.ClienteBLL" SelectMethod="obterDataSet" runat="server"></asp:ObjectDataSource>
</asp:Content>
