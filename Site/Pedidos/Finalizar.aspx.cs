﻿using BLL.Pedidos;
using BLL.Produto;
using Framework.Impressao;
using MODEL.Pagamento;
using MODEL.Pedido;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Pedidos
{
    public partial class Finalizar : BaseWebFormAutenticado
    {

        int idEmp = Convert.ToInt32(ConfigurationManager.AppSettings["idEmpresa"]);
        decimal valorEntrega = Convert.ToDecimal(ConfigurationManager.AppSettings["valorEntrega"]);
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!IsPostBack)
                carregarDados();
        }
        List<Pedido_VO> lst = new List<Pedido_VO>();
        private void carregarDados()
        {
            Pedido_VO Pedido = new Pedido_VO();
            List<Pedido_VO> lst = new List<Pedido_VO>();

            if (Session["idPedido"] != null)
            {
                int idPedido = 0;
                int.TryParse(Session["idPedido"].ToString(), out idPedido);

                if (idPedido > 0)
                {
                    this.obterPedido(idPedido);
                    Pedido.idPedido = idPedido;
                    Pedido.TemPedido = true;
                    lst.Add(Pedido);
                    Session["NovoPedido"] = lst;
                    Session["Pedido"] = idPedido;
                }

            }

            
            carregarPagamento();
            carregarEntrega();

        }

        private void carregarPagamento()
        {
            List<Pagamento_VO> lstPagamento = new PedidosBLL().obterPagamento();

            this.ddlPagamento.Items.Clear();
            this.ddlPagamento.Items.Add(new ListItem("Selecione o pagamento", ""));
            if (lstPagamento != null)
            {
                foreach (Pagamento_VO pagamento in lstPagamento)
                {
                    this.ddlPagamento.Items.Add(new ListItem(pagamento.TipoPagamento.ToString(), pagamento.idTipoPagamento.ToString()));
                }
            }
        }
        
        private void carregarEntrega()
        {

            this.ddlEntrega.Items.Clear();
            this.ddlEntrega.Items.Add(new ListItem("Entrega", "1"));
            this.ddlEntrega.Items.Add(new ListItem("Retirada", "2"));
            this.ddlEntrega.SelectedIndex = 0;

        }



        private void obterPedido(int idDetPed)
        {
            Pedido_VO Pedido = new Pedido_VO();
            
            if (idDetPed > 0)
            {
                this.lst = new PedidosBLL().obterDadosPedidos(idDetPed);
                this.rptItems.DataSource = lst;
                this.rptItems.DataBind();

                Label lbl = (Label)rptItems.FindControl("lblTotal");

                //Finding the FooterTemplate and access its controls
                Control FooterTemplate = rptItems.Controls[rptItems.Controls.Count - 1].Controls[0];
                Label lblFooter = FooterTemplate.FindControl("lblTotal") as Label;
                

                //if (this.ddlEntrega.SelectedIndex == -1)
                    lblFooter.Text = (new PedidosBLL().totalPedido(idDetPed)+ valorEntrega).ToString();
                //else
                //    lblFooter.Text = new PedidosBLL().totalPedido(idDetPed).ToString();

            }

        }

        protected void cmdVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pedidos2.aspx");
        }

        protected void cmdFinalizar_Click(object sender, EventArgs e)
        {
            PedidosBLL pedido = new PedidosBLL();
            int idPedido = (int)Session["Pedido"];
            Pagamento_VO pagamento = new Pagamento_VO();


            if (ddlPagamento.SelectedIndex == 0)
                ClientScript.RegisterStartupScript(GetType(), "message", "newAlert('alert alert-danger', 'Por favor escolha a forma de pagamento!');", true);
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('#myModal').modal('show');</script>", false);

                pagamento.idTipoPagamento = Convert.ToInt32(ddlPagamento.SelectedValue);
                pagamento.idStatusPedido = (int)StatusPagamento.Finalizado;
                pagamento.idPedido = idPedido;
                pagamento.idEntrega = Convert.ToInt32(ddlEntrega.SelectedValue);
                pagamento.obs = txtObs.Text;
                pedido.atualizaPagamento(pagamento);
                imprimirCupom(idPedido);
                //imprime 2 via
                imprimirCupom(idPedido);
            }
        }

        private void imprimirCupom(int idPedido)
        {
            
            //TO DO
            new PrintCupom().ImprimeVendaVista(this.lst, idEmp, idPedido, valorEntrega);
        }

        protected void ddlPagamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idPedido = 0;
            int.TryParse(Session["idPedido"].ToString(), out idPedido);

            Label lbl = (Label)rptItems.FindControl("lblTotal");

            //Finding the FooterTemplate and access its controls
            Control FooterTemplate = rptItems.Controls[rptItems.Controls.Count - 1].Controls[0];
            Label lblFooter = FooterTemplate.FindControl("lblTotal") as Label;

            if (this.ddlEntrega.SelectedIndex == 0)
                lblFooter.Text = (new PedidosBLL().totalPedido(idPedido) + valorEntrega).ToString();
            else
                lblFooter.Text = (new PedidosBLL().totalPedido(idPedido)).ToString();
        }
    }
}