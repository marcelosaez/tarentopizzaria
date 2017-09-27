<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="OpcionaisMain.aspx.cs" Inherits="Site.Produtos.OpcionaisMain" %>
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
                        <a href="opcionais.aspx">Adicionar novo opcional</a>
                     </div>
                        <Wizard:WizardGridView ID="gvListaOpcionais" runat="server" DataSourceID="SourceData" DataKeyNames="id" OnRowCommand="gvListaOpcionais_RowCommand" 
                        EmptyDataText="Não há opcionais cadastrados até o momento." OnRowDataBound="gvListaOpcionais_RowDataBound" AutoGenerateColumns="true"  
                            CssClass="table table-hover"   ClientIDMode="Static" AutoGenerateEditButton="true" PageSize="20" AllowPaging="true" Width="60%">
                        </Wizard:WizardGridView>
                </center>
            </div>
        </div>
    </div>
    <asp:ObjectDataSource ID="SourceData" TypeName="BLL.Produto.ProdutoBLL" SelectMethod="obterDataSetOpcionais" runat="server"></asp:ObjectDataSource>

</asp:Content>
