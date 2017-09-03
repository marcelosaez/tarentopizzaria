using BLL.Cliente;
using BLL.Pedidos;
using MODEL.Cliente;
using MODEL.Pedido;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Pedidos
{
    public partial class Default : BaseWebForm
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            this.txtBusca.Attributes.Add("OnChange", "tbDrugName_OnChange()");
        }

        protected void cmdSalvar_Click(object sender, EventArgs e)
        {
            if (validarFormulario())
            {
                this.fazerPedido();
            }
        }
        private bool validarFormulario()
        {
            if (txtBusca.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "message", "newAlert('alert alert-danger', 'Por favor digite ao menos um telefone!');", true);
                return false;
            }
            return true;
        }

        private void fazerPedido()
        {
            Pedido_VO clientePedido = new Pedido_VO();
            var idCliente = "";
            string[] arrPedido = txtBusca.Text.Split('-');
            idCliente = arrPedido[0].Trim();

            if (validaCliente(idCliente))
            {
                clientePedido.idCliente = Convert.ToInt32(idCliente);
                Session["pedido"] = clientePedido;
                Session["NovoPedido"] = null;
                Response.Redirect("Pedidos2.aspx");
            }
            

        }

        protected void carregaDadosCliente_OnClientItemSelected(object sender, EventArgs e)
        {
            Pedido_VO clientePedido = new Pedido_VO();
            var idCliente = "";
            string[] arrPedido = txtBusca.Text.Split('-');
            idCliente = arrPedido[0].Trim();

            if (validaCliente(idCliente))
            {
                //clientePedido.idCliente = Convert.ToInt32(idCliente);
                //Session["pedido"] = clientePedido;
                //Session["NovoPedido"] = null;
                //Response.Redirect("Pedidos2.aspx");
            }

        }

        private bool validaCliente(string idCliente)
        {
            bool retorno = false;
            Cliente_VO cliente = new Cliente_VO();

           int idCli = 0;
            int.TryParse(idCliente, out idCli);

            if (idCli > 0)
            {
                divDetalhaCliente.Visible = false;
                cliente = new ClienteBLL().obterRegistro(idCli);
                if (cliente == null)
                {
                    ClientScript.RegisterStartupScript(GetType(), "message", "newAlert('alert alert-danger', 'Cliente nao encontrado!');", true);

                }
                else
                {
                    retorno = true;
                    exibeDetalhas(cliente);
                }
            }
            else
                ClientScript.RegisterStartupScript(GetType(), "message", "newAlert('alert alert-danger', 'Por favor digite um telefone valido!');", true);

            return retorno;
        }

        private void exibeDetalhas(Cliente_VO cliente)
        {
            divDetalhaCliente.Visible = true;
            lblNome.Text = cliente.nome;
            lblEnd.Text = "" +cliente.endereco + " - apto: " + cliente.numero;
            if(cliente.dddcel!=0)
                 lblTel.Text = cliente.dddcel + " " + cliente.cel;
            if (cliente.dddres != 0)
            {
                if (cliente.dddcel != 0)
                    lblTel.Text += " | ";
                lblTel.Text += cliente.dddres + " " + cliente.telres;

            }
        }

        protected void imgLimpar_Click(object sender, ImageClickEventArgs e)
        {
            txtBusca.Text = "";
            divDetalhaCliente.Visible = false;
        }
    }
}