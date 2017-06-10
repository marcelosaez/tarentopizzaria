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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            var pedido = "";
            string[] arrPedido = txtBusca.Text.Split('-');
            pedido = arrPedido[0].Trim();
            clientePedido.idCliente = Convert.ToInt32(pedido);
            Session["pedido"] = clientePedido;
            Response.Redirect("Pedidos2.aspx");

        }

    }
}