using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MODEL;
using ClienteBLL;



namespace Site
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarMetodos();
            }
            
        }

        private void CarregarMetodos()
        {
            listarTipoCliente();
        }

        private void listarTipoCliente()
        {
            List<TipoCliente_VO> lstTipoCliente = new Cliente.TipoClienteBLL().ListarClienteTipo();
            this.ddlTipo.Items.Clear();
            this.ddlTipo.Items.Add(new ListItem("", ""));
            this.ddlTipo.Items.Add(new ListItem("Selecionar Todos", "0"));
            if (lstTipoCliente != null)
            {
                foreach (TipoCliente_VO tipo in lstTipoCliente)
                {
                    this.ddlTipo.Items.Add(new ListItem(tipo.descricao, tipo.idTipo.ToString()));
                }
            }
        }

        protected void carregaCliente_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            gvwCliente.Visible = false;
            List<Cliente_VO> lstTipoCliente = new List<Cliente_VO>();

            if (ddlTipo.SelectedValue != "")
            {
                if (ddlTipo.SelectedIndex == 1)
                    lstTipoCliente = new Cliente.TipoClienteBLL().ListarCliente();
                else
                    lstTipoCliente = new Cliente.TipoClienteBLL().ListarTipoCliente(ddlTipo.SelectedValue);


                gvwCliente.DataSource = lstTipoCliente;
                gvwCliente.DataBind();
                gvwCliente.Visible = true;
            }
        }

        protected void gvwCliente_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Ativo")))
                    e.Row.CssClass = "ativo";
                else
                    e.Row.CssClass = "inativo";
            }
        }
    }

}