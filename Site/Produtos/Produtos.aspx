<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Produtos.aspx.cs" Inherits="Site.Produtos.Produtos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <div class="container">
         <div class="row">

            <div class="col-md-1">
                <a href="/Produtos/Default.aspx"><i class="fa fa-arrow-left fa-2x" aria-hidden="true"></i></a>
            </div>
            <div class="col-md-11">

               <center>
                     <div class="text-nowrap h3">
                        <a href="EditarProdutos.aspx">Adicionar novo produto</a>
                     </div>
                        <Wizard:WizardGridView ID="gvListaProdutos" runat="server" DataSourceID="SourceData" DataKeyNames="id" OnRowCommand="gvListaProdutos_RowCommand" 
                        EmptyDataText="Não há produtos cadastrados até o momento." OnRowDataBound="gvListaProdutos_RowDataBound" AutoGenerateColumns="true"  
                            CssClass="table table-hover"   ClientIDMode="Static" AutoGenerateEditButton="true" PageSize="20" AllowPaging="true" Width="60%">
                        </Wizard:WizardGridView>
                </center>
            </div>
        </div>
    </div>
    <asp:ObjectDataSource ID="SourceData" TypeName="BLL.Produto.ProdutoBLL" SelectMethod="obterDataSet" runat="server"></asp:ObjectDataSource>


</asp:Content>
