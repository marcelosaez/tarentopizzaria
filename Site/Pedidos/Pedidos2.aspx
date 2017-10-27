<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Pedidos2.aspx.cs" Inherits="Site.Pedidos.Pedidos2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="/Content/bootstrap.css" rel="stylesheet" />
    <link href="/Content/pedidos.css" rel="stylesheet" />

    <script src="/Scripts/jquery-1.9.0.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
    <script src="/Scripts/jquery.maskMoney.js"></script>
    <script src="/Scripts/bootbox.min.js"></script>

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
            $("#divAlerta").remove();
            //$(".alert-message").delay(500).fadeOut("slow", function () { $(this).remove(); });
            $("#alert-area").append($("<div id='divAlerta' class='alert-message " + type + " fade in' data-alert><p> " + message + " </p></div>"));
            //$(".alert-message").delay(8000).fadeOut("slow", function () { $(this).remove(); });
        }

        function newSucess(message) {
            $("#divValor").remove();
            $("#success-alert").removeClass("invisible");
            $("#success-alert").append($("<div class='text-center' id='divValor'><strong> " + message + "</strong></div>"));
            //$("#success-alert").fadeTo(30000, 500).slideUp(500, function () {
            //    $("#success-alert").alert('close');
            //});

            //$("#alert-area").append($("<div class='alert alert-success fade in' data-alert><p> " + message + " </p></div>"));
            //$(".alert-message").delay(8000).fadeOut("slow", function () { $(this).remove(); });
        }

        function newAlert(message) {
            $("#divAlerta").remove();
            $("#danger-alert").removeClass("invisible");
            $("#danger-alert").append($("<div id='divAlerta' class='text-center'><strong> " + message + "</strong></div>"));
            $("#danger-alert").fadeTo(10000, 500).slideUp(500, function () {
                //$("#danger-alert").alert('close');
                $("#danger-alert").addClass("invisible");
            });

            //$("#alert-area").append($("<div class='alert alert-success fade in' data-alert><p> " + message + " </p></div>"));
            //$(".alert-message").delay(8000).fadeOut("slow", function () { $(this).remove(); });
        }

        //newAlert('success', 'Oh yeah!');

        $(function () {
            $("#ctl00_ContentPlaceHolder1_txtValor").maskMoney({ showSymbol: true, symbol: "R$", decimal: ",", thousands: "." });
        })


        function confirmDelete(sender) {
            if ($(sender).attr("confirmed") == "true") { return true; }

            bootbox.confirm("Deseja mesmo apagar este registro?", function (confirmed) {
                if (confirmed) {
                    $(sender).attr("confirmed", confirmed).trigger("click");
                }
            });

            return false;
        }

        function confirmEdit(sender) {

            if ($(sender).attr("confirmed") == "true") { return true; }
            bootbox.confirm("Deseja realmente editar este registro*?", function (confirmed) {
                if (confirmed) {
                    $(sender).attr("confirmed", confirmed).trigger("click");
                    //__doPostBack('__EVENTTARGET', '__EVENTARGUMENT');
                }
            });
            return false;
        }

        function teste() {
            alert('ok');
            return false;
        }

        function openModal() {
            $('#myModal').modal('show');
        }




        /**
 * Module for displaying "Waiting for..." dialog using Bootstrap
 *
 * @author Eugene Maslovich <ehpc@em42.ru>
 */

        var waitingDialog = waitingDialog || (function ($) {
            'use strict';

            // Creating modal dialog's DOM
            var $dialog = $(
                '<div class="modal fade" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-hidden="true" style="padding-top:15%; overflow-y:visible;">' +
                '<div class="modal-dialog modal-m">' +
                '<div class="modal-content">' +
                '<div class="modal-header"><h3 style="margin:0;"></h3></div>' +
                '<div class="modal-body">' +
                '<div class="progress progress-striped active" style="margin-bottom:0;"><div class="progress-bar" style="width: 100%"></div></div>' +
                '</div>' +
                '</div></div></div>');

            return {
                /**
                 * Opens our dialog
                 * @param message Custom message
                 * @param options Custom options:
                 * 				  options.dialogSize - bootstrap postfix for dialog size, e.g. "sm", "m";
                 * 				  options.progressType - bootstrap postfix for progress bar type, e.g. "success", "warning".
                 */
                show: function (message, options) {
                    // Assigning defaults
                    if (typeof options === 'undefined') {
                        options = {};
                    }
                    if (typeof message === 'undefined') {
                        message = 'Loading';
                    }
                    var settings = $.extend({
                        dialogSize: 'm',
                        progressType: '',
                        onHide: null // This callback runs after the dialog was hidden
                    }, options);

                    // Configuring dialog
                    $dialog.find('.modal-dialog').attr('class', 'modal-dialog').addClass('modal-' + settings.dialogSize);
                    $dialog.find('.progress-bar').attr('class', 'progress-bar');
                    if (settings.progressType) {
                        $dialog.find('.progress-bar').addClass('progress-bar-' + settings.progressType);
                    }
                    $dialog.find('h3').text(message);
                    // Adding callbacks
                    if (typeof settings.onHide === 'function') {
                        $dialog.off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
                            settings.onHide.call($dialog);
                        });
                    }
                    // Opening dialog
                    $dialog.modal();
                },
                /**
                 * Closes dialog
                 */
                hide: function () {
                    $dialog.modal('hide');
                }
            };

        })(jQuery);



    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <div class="container">


        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <Wizard:WizardGridView ID="gvListaPedidos" runat="server" DataSourceID="SourceData" DataKeyNames="idDetPed" OnRowCommand="gvListaPedidos_RowCommand"
                            ShowHeaderWhenEmpty="false" OnRowDataBound="gvListaPedidos_RowDataBound" AutoGenerateColumns="false" CssClass="table table-hover" ClientIDMode="Static" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" PageSize="20" AllowPaging="true" ShowFooter="true" FooterStyle-BackColor="#006DCC" FooterStyle-CssClass="rodape" OnRowEditing="gvListaPedidos_RowEditing">
                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("idDetPed") %>' CommandName="Delete" CausesValidation="false">Apagar </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEditar" runat="server" CommandArgument='<%#Eval("idDetPed") %>' CommandName="Edit" CausesValidation="false">Editar </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="idDet">
                                    <ItemTemplate>
                                        <asp:Label ID="lblidPedDet" Width="30px" runat="server" Text='<%#Eval("idDetPed") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="idPedido">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdPedido" Width="30px" runat="server" Text='<%#Eval("idPedido") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipoPedido" Width="30px" runat="server" Text='<%#Eval("Tipo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opção">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOpcao" Width="80px" runat="server" Text='<%#Eval("Opcao") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sabor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSabor" Width="250px" runat="server" Text='<%#Eval("sabor") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField  HeaderText="Adicionais" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblAdicionais" Width="250px" runat="server" Text='<%# DataBinder.Eval( Container.DataItem, "opcionais[0].TxtAdicionais") %>' CssClass="tooltipAdicionais " ToolTip='<%# DataBinder.Eval( Container.DataItem, "opcionais[0].lstAdicionais") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qtd">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQtd" Width="30px" runat="server" Text='<%#Eval("qtd") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valor" FooterText="Total" FooterStyle-CssClass="rodape">
                                    <ItemTemplate>
                                        <asp:Label ID="lblValor" Width="30px" runat="server" Text='<%#Eval("valor") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Label1" runat="server" Text="Total" Font-Bold="true"></asp:Label>
                                        <asp:Label ID="lbltotal" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatusPedido" Width="30px" runat="server" Text='<%#Eval("statusPedido") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#006DCC" Font-Bold="True" ForeColor="White" CssClass="rodape" />

                        </Wizard:WizardGridView>
                    </div>
                </div>
            </ContentTemplate>


        </asp:UpdatePanel>

    </div>
    <asp:ObjectDataSource ID="SourceData" TypeName="BLL.Pedidos.PedidosBLL" SelectMethod="obterDataSet" runat="server"></asp:ObjectDataSource>

    <div class="container">
        <div class="row" id="container">
            <div class="col-md-4"></div>
            <div class="col-md-4 text-center">
                <div id="alert-area" style="width: 420px;"></div>


                <div class="formulario">
                    <h1>Pedido</h1>
                    <section class="login-form">
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" Visible="false" />
                        <asp:Panel ID="panelFormulario" runat="server" DefaultButton="cmdSalvar">

                            <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-12">
                                        <asp:DropDownList ID="ddlTipoProdutos" runat="server" CssClass="form-control espaco" Width="340px" OnSelectedIndexChanged="ddlTipoProdutos_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-12">
                                        <asp:DropDownList ID="ddlOpcao" runat="server" CssClass="form-control espaco" Width="340px" OnSelectedIndexChanged="ddlOpcao_SelectedIndexChanged" Visible="false" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:DropDownList ID="ddlSabor1" runat="server" CssClass="form-control espaco" Width="340px" Visible="false" OnSelectedIndexChanged="ddlSabor_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:DropDownList ID="ddlSabor2" runat="server" CssClass="form-control espaco" Width="340px" Visible="false" OnSelectedIndexChanged="ddlSabor_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:DropDownList ID="ddlSabor3" runat="server" CssClass="form-control espaco" Width="340px" Visible="false" OnSelectedIndexChanged="ddlSabor_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:DropDownList ID="ddlQtd" runat="server" CssClass="form-control espaco" Width="340px" OnSelectedIndexChanged="ddlQtd_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="[ form-group ]" id="divBorda" runat="server" visible="false">
                                            <%--<input type="checkbox" runat="server" name="fancyCheckBox" onclick="MostraBorda_Click" id="fancyCheckBox" autocomplete="off" runat="server"    />--%>
                                            <asp:CheckBox runat="server" OnCheckedChanged="MostraBorda_Click" ID="fancyCheckBox" AutoPostBack="true" ClientIDMode="Static" />
                                            <div class="[ btn-group ]">
                                                <label for="fancyCheckBox" class="[ btn btn-default ]">
                                                    <span class="[ glyphicon glyphicon-ok ]"></span>
                                                    <span></span>
                                                </label>
                                                <label for="fancyCheckBox" class="[ btn btn-default  ] tamanhoCheckBox">
                                                    Borda Recheada
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:DropDownList ID="ddlBorda" runat="server" CssClass="form-control espaco" Width="340px" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlBorda_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>



                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtObs" runat="server" CssClass="form-control espaco" Width="340px" placeholder="Obs:" TextMode="MultiLine" MaxLength="80"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div id="divOpcionais" runat="server" class="col-md-12 " style="margin-bottom: 30px;" visible="false">
                                        <div class="btn btn-lg btn-success btn-block botaoAdicional" data-toggle="modal" data-load-url="" data-target="#myModal">Opcionais</div>

                                    </div>

                                    <div class="col-md-12">

                                        <div class="col-md-6">
                                            <asp:Button ID="cmdVoltar" runat="server" Text="Voltar" OnClick="cmdVoltar_Click" CssClass="btn btn-lg btn-danger btn-block botaoVoltar2" CausesValidation="false" UseSubmitBehavior="false" />&nbsp;
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Button ID="cmdSalvar" runat="server" Text="Adicionar" OnClick="cmdSalvar_Click" CssClass="btn btn-lg btn-primary btn-block botaoSalvar2" />
                                        </div>

                                    </div>
                                    <div class="col-md-12">

                                        <asp:Button ID="cmdAtualizar" runat="server" Text="Atualizar" OnClick="cmdAtualizar_Click" CssClass="btn btn-lg btn-primary btn-block botaoAtualizar" Visible="false" />

                                        <asp:Button ID="cmdFinalizar" runat="server" Text="Finalizar" OnClick="cmdFinalizar_Click" CssClass="btn btn-lg btn-success btn-block botaoFinalizar" Visible="false" />

                                    </div>

                                    <div class="row">
                                    </div>

                                    <div id="myModal" class="modal fade" data-backdrop="static">
                                        <div class="modal-dialog modal-lg">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                    <h4 class="modal-title">Adicionar Opcionais</h4>
                                                </div>
                                                    <asp:CheckBoxList ID="cblOpcionais" runat="server" RepeatDirection="Horizontal" RepeatColumns="4" CssClass="cblEspaco" ></asp:CheckBoxList>
                                                <%--OnSelectedIndexChanged="cblOpcionais_SelectedIndexChanged" AutoPostBack="true"--%>
                                                <div class="modal-footer">
                                                    <p class="text-left"><strong>* Não será tarifado valores para pastel montado</strong></p>
                                                    <%--<button type="button" class="btn btn-default" data-dismiss="modal" runat="server">Fechar</button>--%>
                                                    <%--<button type="submit" class="btn btn-primary">Save changes</button>--%>
                                                    <asp:Button ID="btnFechar" runat="server" OnClick="btnFechar_Click"  UseSubmitBehavior="false"  class="btn btn-default" data-dismiss="modal" Text="Fechar" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                   


                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </asp:Panel>

                        <asp:UpdatePanel ID="updPainel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                     

                                </ContentTemplate>
                        </asp:UpdatePanel>

                    </section>
                </div>


            </div>
            <div class="col-md-2"></div>

            <div class="col-md-2">
                <div class="alert alert-success invisible" id="success-alert">
                    <%--<button type="button" class="close" data-dismiss="alert">x</button>--%>
                </div>
                <div class="alert alert-danger invisible" id="danger-alert">
                    <%--<button type="button" class="close" data-dismiss="alert">x</button>--%>
                </div>

            </div>
        </div>
    </div>




</asp:Content>
