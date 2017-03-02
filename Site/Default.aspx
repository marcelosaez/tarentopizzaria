<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Site.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/css.css" rel="stylesheet" />

</head>
<body>
    <script src="Scripts/jquery-1.9.0.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>


    <form id="form1" runat="server">
    <div class="container">
        <div class="row text-center">
            <H1>Escolha o tipo</H1>
        </div>

        <div class="row">
            <div class="col-lg-12 text-center">
                <asp:DropDownList ID="ddlTipo" runat="server" OnSelectedIndexChanged="carregaCliente_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>   
            </div>
        </div>
        <div class="row">
            <asp:GridView ID="gvwCliente" OnRowDataBound="gvwCliente_RowDataBound" runat="server" Visible="false"></asp:GridView>


        </div>
    </div>
    </form>
</body>
</html>
