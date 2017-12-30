<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Fechamento.aspx.cs" Inherits="Site.Financeiro.Fechamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <link rel="stylesheet" type="text/css" href="../Content/DataTables/datatables.css" />
    <script type="text/javascript" src="https://cdn.datatables.net/v/bs4/dt-1.10.16/b-1.4.2/b-html5-1.4.2/r-2.2.0/datatables.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.4.2/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" src="//cdn.datatables.net/buttons/1.4.2/js/buttons.html5.min.js"></script>
    <script src="/Scripts/bootstrap-datepicker.js"></script>
    <script src="/Scripts/bootstrap-datepicker-globalize.js"></script>
    <script src="/Scripts/locales/bootstrap-datepicker.pt-BR.min.js"></script>
    <link href="/Content/bootstrap.css" rel="stylesheet" />
    <link href="/Content/bootstrap-datepicker.css" rel="stylesheet" />
    <link href="/Content/bootstrap-datepicker3.css" rel="stylesheet" />

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            var d = new Date();
            var curr_date = d.getDate();
            var curr_month = d.getMonth() + 1;
            var curr_year = d.getFullYear();
            var curr_hour = d.getHours();
            var curr_min = d.getMinutes();
            var curr_sec = d.getSeconds();

            var dia = curr_date.toString().length == 1 ? "0" + curr_date : curr_date;
            var mes = curr_month.toString().length == 1 ? "0" + curr_month : curr_month;
            var data = dia + "" + mes + "" + "" + curr_year + "";


            $("#btnExport").click(function () {
                tableToExcel('tbl', 'tblPagto', data);
                //tableToExcel(, data);

            });

            var tableToExcel = (function () {
                var uri = 'data:application/vnd.ms-excel;base64,'
                    , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><meta http-equiv="content-type" content="application/vnd.ms-excel; charset=UTF-8"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
                    , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                    , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
                return function (table, table2, name) {
                    if (!table.nodeType) table = document.getElementById(table)
                    if (!table2.nodeType) table2 = document.getElementById(table2)
                    //var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                    var ctx = { worksheet: "Fechamento_" + data, table: table.innerHTML + table2.innerHTML }
                    //window.location.href = uri + base64(format(template, ctx))
                    var link = document.createElement("a");
                    link.download = "Fechamento_" + data + ".xls";
                    link.href = uri + base64(format(template, ctx));
                    link.click();
                }
            })()


            

        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#datetimepicker1').datepicker({
                format: 'dd/mm/yyyy',
                language: 'pt-BR'
            });
            $('#datetimepicker2').datepicker({
                format: 'dd/mm/yyyy',
                language: 'pt-BR'
            });
        });
    </script>
    <style>
        .num {
            mso-number-format: General;
        }
    </style>
    <body>
        <div class="row voffset6">
         <div class="col-md-1">
           <a href="/Financeiro/Default.aspx">
               <i class="fa fa-arrow-circle-left fa-3x voltar" aria-hidden="true"></i>
           </a>
         </div>
         <div class="col-md-11"></div>
        </div>
        <div id="divPeriodo" runat="server" visible="false">
            <div class="container">
                <div class="row">
                    <div class='col-sm-3'></div>
                    <div class='col-sm-3 text-right'>
                        <div class="form-group">
                            <div class='input-group date' id='datetimepicker1'>
                                <input type='text' class="form-control input-lg espaco" placeholder="Período Inicial" id="txtDtInicial" runat="server" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class='col-sm-3 text-left'>
                        <div class="form-group">
                            <div class='input-group date' id='datetimepicker2'>
                                <input type='text' class="form-control input-lg espaco" placeholder="Período Final" id="txtDtFinal" runat="server" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-1">
                        <asp:Button ID="cmdBuscar" runat="server" Text="OK" CssClass="btn btn-lg btn-primary btn-block botaoSalvar" OnClick="cmdSalvar_cmdBuscar" />
                    </div>
                    <div class='col-sm-2'></div>
                </div>
            </div>
        </div>
        <div id="divResumo" runat="server" visible="false">
            <div class="row">
                <div class="col-lg-4"></div>
                <div class="col-lg-4">
                    <div class="row">
                        <div class="col-lg-11">
                            <h3>
                        <asp:Label ID="lblData" runat="server"></asp:Label>
                            </h3>
                        </div>
                        <div class="col-lg-1 text-right" style="vertical-align: middle">
                            <a href="#;" id="btnExport" title="Baixar Excel">
                                <i class="fa fa-file-excel-o fa-2x" aria-hidden="true" style="padding-left: 15px; padding-top: 20px; text-align: left;"></i>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4"></div>
            </div>
            <div class="row">
                <div class="col-lg-4"></div>
                <div class="col-lg-4">
                    <div id="dvData" class="text-center" style="width: 450px; margin-top: 20px;">
                        <asp:Repeater ID="repeater" runat="server" OnItemDataBound="repeater_ItemDataBound">
                            <HeaderTemplate>
                                <table id="tbl" cellpadding="1" cellspacing="0"
                                    border="0" class="display table table-hover">
                                    <thead class="text-center">
                                        <tr class="text-center">
                                            <th class="text-center">Produto</th>
                                            <th class="text-center">Qtd</th>
                                            <th class="text-center">Total</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Tipo") %></td>
                                    <td><%# Eval("qtd") %></td>
                                    <td class="num"><%# Eval("Total") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tfoot>
                                    <tr class="rodape">
                                        <th class="text-center rodapeTotal">
                                            <asp:Label ID="lblTotal" runat="server" Text="Total" Font-Bold="true"></asp:Label>
                                        </th>
                                        <th class="text-center rodapeTotal">
                                            <asp:Label ID="lblQtd" runat="server" Text="" Font-Bold="true" CssClass="text-center"></asp:Label>
                                        </th>
                                        <th class="text-center rodapeTotal">
                                            <asp:Label ID="lblValor" runat="server" Text="" Font-Bold="true" CssClass="text-center"></asp:Label>
                                        </th>
                                    </tr>
                                    <tr class="rodape">
                                        <th class="text-center rodapeTotal">
                                            <asp:Label ID="lblTotalEntregas" runat="server" Text="Total Entregas" Font-Bold="true"></asp:Label>
                                        </th>
                                        <th class="text-center rodapeTotal">
                                            <asp:Label ID="lblQtdEntregas" runat="server" Text="" Font-Bold="true" CssClass="text-center"></asp:Label>
                                        </th>
                                        <th class="text-center rodapeTotal num">
                                            <asp:Label ID="lblValorEntregas" runat="server" Text="" Font-Bold="true" CssClass="text-center"></asp:Label>
                                        </th>
                                    </tr>
                                    <tr class="rodape">
                                        <th class="text-center rodapeTotalGeral">
                                            <asp:Label ID="lblTotalGeral" runat="server" Text="Total Geral" Font-Bold="true"></asp:Label>
                                        </th>
                                        <th class="text-center rodapeTotalGeral">
                                            <asp:Label ID="lblQtdTotalGeral" runat="server" Text="" Font-Bold="true" CssClass="text-center"></asp:Label>
                                        </th>
                                        <th class="text-center rodapeTotalGeral">
                                            <asp:Label ID="lblValorTotalGeral" runat="server" Text="" Font-Bold="true" CssClass="text-center"></asp:Label>
                                        </th>
                                    </tr>
                                </tfoot>
                                </tbody>
                </table>
                            </FooterTemplate>
                        </asp:Repeater>

                        <asp:Repeater ID="repeaterPagto" runat="server">
                            <HeaderTemplate>
                                <table id="tblPagto" cellpadding="1" cellspacing="0"
                                    border="0" class="display table table-hover">
                                    <thead class="text-center">
                                        <tr class="text-center">
                                            <th class="text-center">Tipo Pagamento</th>
                                            <th class="text-center">Qtd</th>
                                            <th class="text-center">Total</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="text-center">
                                    <td class="text-center"><%# Eval("TipoPagamento") %></td>
                                    <td class="text-center"><%# Eval("qtd") %></td>
                                    <td class="text-center" class="num"><%# Eval("Total") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                </table>
                            </FooterTemplate>
                        </asp:Repeater>

                    </div>
                </div>
                <div class="col-lg-4"></div>
            </div>
        </div>

    </body>
</asp:Content>
