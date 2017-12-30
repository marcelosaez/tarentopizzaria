using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Produtos
{
    public partial class Produtos : BaseWebFormAutenticado

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

        protected void cmdBusca_OnClick(object sender, EventArgs e)
        {
            this.setDataSource();
        }

        protected void setDataSource()
        {
            this.SourceData.SelectParameters.Clear();
            this.SourceData.SelectParameters.Add(new Parameter("busca", System.Data.DbType.String, txtBusca.Text.Trim()));
        }

        protected void gvListaProdutos_RowCommand(Object src, GridViewCommandEventArgs e)
        {
            int index = 0;
            Int32.TryParse(e.CommandArgument.ToString(), out index);

            if (e.CommandName == "Edit")
            {
                int id_ = 0;
                Int32.TryParse(gvListaProdutos.DataKeys[index].Value.ToString(), out id_);

                Response.Redirect("EditarProdutos.aspx?idProduto=" + id_.ToString());
            }

        }
        protected void gvListaProdutos_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
        }
    }
}