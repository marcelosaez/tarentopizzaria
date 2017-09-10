using MODEL.Pedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Pedidos
{
    public partial class All : BaseWebFormAutenticado
    {
        protected  override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!IsPostBack)
            {
                carregaDados();
            }
        }

        private void carregaDados()
        {

        }

        protected void SourceData_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            DateTime dataIni = DateTime.Now;
            DateTime dataFim = DateTime.Now;

            e.InputParameters["dataIni"] = dataIni;
            e.InputParameters["dataFim"] = dataFim;
        }

        protected void gvListaPedidos_RowCommand(Object src, GridViewCommandEventArgs e)
        {
            int index = 0;
            Int32.TryParse(e.CommandArgument.ToString(), out index);

            if (e.CommandName == "Edit")
            {
                int idPedido = 0;
                Int32.TryParse(gvListaPedidos.DataKeys[index].Value.ToString(), out idPedido);
                Pedido_VO Pedido = new Pedido_VO();
                List<Pedido_VO> lst = new List<Pedido_VO>();
                if (idPedido > 0)
                {
                    Pedido.idPedido = idPedido;
                    Pedido.TemPedido = true;
                    lst.Add(Pedido);
                    Session["NovoPedido"] = lst;
                    Session["Pedido"] = idPedido;
                    Response.Redirect("Pedidos2.aspx");
                }
            }

        }
    }
}