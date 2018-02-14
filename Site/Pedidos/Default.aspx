<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Site.Pedidos.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    

    <script src="/Scripts/jquery-1.9.1.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
    <link href="/Content/bootstrap.css" rel="stylesheet" />
    <link href="/Content/pedidos.css" rel="stylesheet" />
    <script type="text/javascript">
        window.alert = function () { };
        var defaultCSS = document.getElementById('bootstrap-css');
        function changeCSS(css) {
            if (css) $('head > link').filter(':first').replaceWith('<link rel="stylesheet" href="' + css + '" type="text/css" />');
            else $('head > link').filter(':first').replaceWith(defaultCSS);
        }
        $(document).ready(function () {
            var iframe_height = parseInt($('html').height());
            //window.parent.postMessage( iframe_height, 'http://bootsnipp.com');
        });

        function newAlert(type, message) {
            $("#alert-area").append($("<div class='alert-message " + type + " fade in' data-alert><p> " + message + " </p></div>"));
            $(".alert-message").delay(6000).fadeOut("slow", function () { $(this).remove(); });
        }
        //newAlert('success', 'Oh yeah!');
        function tbDrugName_OnChange() {
            var txtVal = document.getElementById('<%= txtBusca.ClientID %>').value;
            //document.getElementById('< %= lblList.ClientID %>').innerText = txtVal;
        }

        $('#myModal').on('show.bs.modal', function (e) {
            var loadurl = e.relatedTarget.data('load-url');
            $(this).find('.modal-body').load(loadurl);
           // alert('teste');
           

        });

        
        

        /*var loadingModal = $("#myModal");

        $("#divNovoCliente").click(function () {
            loadingModal.modal("show");
            $(this).find('.modal-body').load(loadurl);
        });*/

        function OnContactSelected(source, eventArgs) {

            var hdnValueID = "<%= hdnValue.ClientID %>";

            document.getElementById(hdnValueID).value = eventArgs.get_value();
            __doPostBack(hdnValueID, "");
        }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   
    <div class="row voffset6">
         <div class="col-md-1">
           <a href="Main.aspx">
               <i class="fa fa-arrow-circle-left fa-3x voltar" aria-hidden="true"></i>
           </a>
         </div>

        <div class="col-md-2"></div>
        <div class="col-md-6">
            <h2 style="text-align: center;">Pesquisar Clientes</h2>

            <div id="alert-area" style="width: 490px;"></div>
            <div class="col-md-12 text-right">
                <asp:TextBox ID="txtBusca" runat="server" EnableTheming="True" MaxLength="50" CssClass="form-control input-lg espaco" placeholder="Buscar" required class="search-box">
                </asp:TextBox>
            </div>
           
            <asp:HiddenField ID="hdnValue" OnValueChanged="carregaDadosCliente_OnClientItemSelected" runat="server" />

            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                ServiceMethod="AutoCompleteAjaxRequest"
                ServicePath="AutoComplete.asmx"
                MinimumPrefixLength="2"
                CompletionInterval="100"
                EnableCaching="false"
                CompletionSetCount="10"
                TargetControlID="txtBusca"
                FirstRowSelected="false"
                CompletionListCssClass="completionList"
                CompletionListHighlightedItemCssClass="itemHighlighted"
                CompletionListItemCssClass="listItem"
                OnClientItemSelected="OnContactSelected">
            </asp:AutoCompleteExtender>
            <div class="col-md-12">
                <div class="col-md-4">
                    <div class="btn btn-lg btn-danger btn-block botaoVoltar semUnderline" data-toggle="modal" data-load-url="" data-target="#myModal" >Novo Cliente</div>
                </div>
                <div class="col-md-4"></div>

                <div class="col-md-4">
                    <asp:Button ID="cmdSalvar" runat="server" Text="Fazer Pedido" CssClass="btn btn-lg btn-primary btn-block botaoSalvar" OnClick="cmdSalvar_Click" />
                </div>

            </div>
        </div>
         <div class="col-md-1 text-left">
                <asp:ImageButton ID="imgLimpar" runat="server" OnClick="imgLimpar_Click" ImageUrl="~/Images/empty.png" Width="32" Height="32" ImageAlign="Left" CssClass="imgLimpar" AlternateText="Limpar" ToolTip="Limpar" />
        </div>
        <div class="col-md-2"></div>
        <%--<a href="#" data-toggle="modal" data-load-url="" data-target="#myModal">Click me</a>--%>
        <%--<a href="#" >Click me</a>--%>
    </div>
    <div id="myModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Novo Cliente</h4>
                </div>
                <div class="modal-body">
                    <iframe src="../Clientes/Novo.aspx" style="zoom: 0.60" frameborder="0" height="810" width="99.6%"></iframe>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
                    <%--<button type="submit" class="btn btn-primary">Save changes</button>--%>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">&nbsp;</div>
    </div>
    <div id="divDetalhaCliente" runat="server" visible="false">
        <div class="row voffset4">
            <div class="col-md-4">&nbsp;</div>
            <div class="col-md-4 text-center">
                <h2>Detalhes do cliente</h2>
            </div>
            <div class="col-md-4">&nbsp;</div>
        </div>
        <div class="col-md-3"></div>
        <div class="col-md-6">
            <div class="col-md-12">
                <table id="tbDetalhaCliente" runat="server">
                    <thead>
                        <tr>
                            <th>Nome</th>
                            <th>Endereço</th>
                            <th>Telefone</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lblNome" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEnd" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTel" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="col-md-3"></div>
    </div>
</asp:Content>
