<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Tipos.aspx.cs" Inherits="Site.Produtos.Tipos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

         <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <div class="container">
         <div class="row">
            <div class="col-md-12">
                <h3>
                    <a href="EditarProdutoTipos.aspx">Adicionar novo tipo</a>
                </h3>
               <center>
                        <Wizard:WizardGridView ID="gvListaTipoProdutos" runat="server" DataSourceID="SourceData" DataKeyNames="id" OnRowCommand="gvListaTipoProdutos_RowCommand" 
                        EmptyDataText="Não há tipo de produtos cadastrados até o momento." OnRowDataBound="gvListaTipoProdutos_RowDataBound" AutoGenerateColumns="true"  
                            CssClass="table table-hover"   ClientIDMode="Static" AutoGenerateEditButton="true" PageSize="20" AllowPaging="true" Width="40%" >
                        </Wizard:WizardGridView>
                </center>
            </div>
        </div>
    </div>
    <asp:ObjectDataSource ID="SourceData" TypeName="BLL.Produto.ProdutoTipoBLL" SelectMethod="obterDataSet" runat="server"></asp:ObjectDataSource>

</asp:Content>
