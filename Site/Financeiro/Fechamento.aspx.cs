using MODEL.Financeiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Financeiro
{
    public partial class Fechamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //carregaDados();
                mostraPeriodo();
                carregarDatas();
            }
        }

        private void carregarDatas()
        {
            DateTime Data = DateTime.Now;
            DateTime PrimeiroDiadoMes = DateTime.Parse("01" +Data.ToString("/ MM / yyyy"));

            txtDtInicial.Value = PrimeiroDiadoMes.ToString("dd/MM/yyyy");
            txtDtFinal.Value = Data.ToString("dd/MM/yyyy");
        }

        private void mostraPeriodo()
        {
            var fechamento = Request.QueryString["fechamento"];
            if (fechamento == "dia")
            {
                DateTime dateIni = DateTime.Now;//.AddDays(-2);
                DateTime dateFim = DateTime.Now;//.AddDays(-2);
                carregaDados(dateIni, dateFim, fechamento);
            }
            else
            {
                divPeriodo.Visible = true;
            }
        }

        
        protected void cmdSalvar_cmdBuscar(object sender, EventArgs e)
        {
            DateTime dateIni = Convert.ToDateTime(txtDtInicial.Value.ToString());
            DateTime dateFim = Convert.ToDateTime(txtDtFinal.Value.ToString());
            carregaDados(dateIni, dateFim, "periodo");
        }

        private void carregaDados(DateTime dateIni, DateTime dateFim, string fechamento)
        {
            

            //Financeiro_VO resultados = new BLL.Pedidos.PedidosBLL().obterDadosFechamentoResumoTotal(dateIni, dateFim.AddDays(1));
            repeater.DataSource = new BLL.Pedidos.PedidosBLL().obterDadosFechamentoResumo(dateIni, dateFim, fechamento);
            repeater.DataBind();

            repeaterPagto.DataSource = new BLL.Pedidos.PedidosBLL().obterDadosFechamentoPagtoResumo(dateIni, dateFim, fechamento);
            repeaterPagto.DataBind();
            divResumo.Visible = true;
            //lblData.Text = dateIni.ToShortDateString();

        }

        protected void repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DateTime dateIni = DateTime.Now;
            DateTime dateFim = DateTime.Now;
            var fechamento = Request.QueryString["fechamento"];
            string title = "Resumo Diário " + dateIni.ToShortDateString();
            if (fechamento != "dia")
            {
                dateIni = Convert.ToDateTime(txtDtInicial.Value.ToString());
                dateFim = Convert.ToDateTime(txtDtFinal.Value.ToString());
                title = "De " +dateIni.ToShortDateString().ToString() + " a " + dateFim.ToShortDateString().ToString();
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                //DateTime dateFim = DateTime.Now;//;.AddDays(-2);
                //DateTime dateIni = DateTime.Now;//;.AddDays(-2);
                Financeiro_VO resultados = new BLL.Pedidos.PedidosBLL().obterDadosFechamentoResumoTotal(dateIni, dateFim.AddDays(1),fechamento);
                Financeiro_VO resultadosEntregas = new BLL.Pedidos.PedidosBLL().obterDadosFechamentoResumoTotalEntregas(dateIni, dateFim.AddDays(1),fechamento);
                Label lblQtd = (Label)e.Item.FindControl("lblQtd");
                Label lblValor = (Label)e.Item.FindControl("lblValor");
                Label lblQtdEntregas = (Label)e.Item.FindControl("lblQtdEntregas");
                Label lblValorEntregas = (Label)e.Item.FindControl("lblValorEntregas");
                Label lblQtdTotalGeral = (Label)e.Item.FindControl("lblQtdTotalGeral");
                Label lblValorTotalGeral = (Label)e.Item.FindControl("lblValorTotalGeral");

                lblQtd.Text = resultados.qtdTotal.ToString();
                lblValor.Text = resultados.valorTotal.ToString();
                lblData.Text = title;

                lblQtdEntregas.Text = resultadosEntregas.qtdTotal.ToString();
                lblValorEntregas.Text = Convert.ToString(resultadosEntregas.qtdTotal * 2) + ",00";

                //lblQtdTotalGeral.Text = Convert.ToString((resultados.qtdTotal) + (resultadosEntregas.qtdTotal));
                lblValorTotalGeral.Text = Convert.ToString((resultados.valorTotal) + (resultadosEntregas.qtdTotal * 2));

            }
        }
    }
}