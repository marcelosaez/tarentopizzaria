using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Produtos
{
    public partial class OpcionaisMain : BaseWebFormAutenticado
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!IsPostBack)
                carregaInformacoes();
        }
        private void carregaInformacoes()
        {
            AutenticacaoBLL.checaPermissao();

        }

        protected void gvListaOpcionais_RowCommand(Object src, GridViewCommandEventArgs e)
        {
            int index = 0;
            Int32.TryParse(e.CommandArgument.ToString(), out index);

            if (e.CommandName == "Edit")
            {
                int id_ = 0;
                Int32.TryParse(gvListaOpcionais.DataKeys[index].Value.ToString(), out id_);

                Response.Redirect("opcionais.aspx?idOpcional=" + id_.ToString());
            }

        }
        protected void gvListaOpcionais_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
        }
    }
}