using BLL;
using MODEL.Funcionario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Funcionarios
{
    public partial class Default : BaseWebFormAutenticado
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

        protected void gvListaFuncionarios_RowCommand(Object src, GridViewCommandEventArgs e)
        {
            int index = 0;
            Int32.TryParse(e.CommandArgument.ToString(), out index);

            if (e.CommandName == "Edit")
            {
                int id_ = 0;
                Int32.TryParse(gvListaFuncionarios.DataKeys[index].Value.ToString(), out id_);

                Response.Redirect("Editar.aspx?idFuncionario=" + id_.ToString());
            }

        }
        protected void gvListaFuncionarios_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
        }
    }
}