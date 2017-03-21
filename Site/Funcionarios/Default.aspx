<%@ Page Title="Funcionarios" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Site.Funcionarios.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <div class="container">
         <div class="row">
            <div class="col-md-12">
                <h3>
                    <a href="Editar.aspx">Adicionar novo Funcionário</a>
                </h3>

              <Wizard:WizardGridView ID="gvListaFuncionarios" runat="server" DataSourceID="SourceData" DataKeyNames="idFuncionario" OnRowCommand="gvListaFuncionarios_RowCommand" 
        EmptyDataText="Não há funcionários cadastrados até o momento." OnRowDataBound="gvListaFuncionarios_RowDataBound" AutoGenerateColumns="true"  CssClass="table table-hover"   ClientIDMode="Static" AutoGenerateEditButton="true" PageSize="20" AllowPaging="true"  >
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
    <asp:ObjectDataSource ID="SourceData" TypeName="BLL.Funcionario.FuncionarioBLL" SelectMethod="obterDataSet" runat="server"></asp:ObjectDataSource>

</asp:Content>
