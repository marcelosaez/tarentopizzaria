<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Site.Clientes.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <div class="container">
         <div class="row">
            <div class="col-md-12">
                <h3>
                    <a href="Editar.aspx">Adicionar novo Cliente</a>
                </h3>

              <Wizard:WizardGridView ID="gvListaClientes" runat="server" DataSourceID="SourceData" DataKeyNames="id" OnRowCommand="gvListaClientes_RowCommand" 
        EmptyDataText="Não há clientes cadastrados até o momento." OnRowDataBound="gvListaClientes_RowDataBound" AutoGenerateColumns="true"  CssClass="table table-hover"   ClientIDMode="Static" AutoGenerateEditButton="true" PageSize="20" AllowPaging="true"   >
        <%--<Columns>
            <Wizard:WizardBoundField HeaderText="Nome" DataField="Nome">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="16%" HorizontalAlign="Left" />
            </Wizard:WizardBoundField>
        </Columns>--%>
    </Wizard:WizardGridView>
            </div>
        </div>
    </div>
    <asp:ObjectDataSource ID="SourceData" TypeName="BLL.Cliente.ClienteBLL" SelectMethod="obterDataSet" runat="server"></asp:ObjectDataSource>


</asp:Content>
