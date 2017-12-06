using BLL;
using MODEL.Funcionario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Site
{
    public partial class Main : BaseWebFormAutenticado
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!IsPostBack)
                CarregaFormulario();
        }

        private void CarregaFormulario()
        {
            var permissao = Request["permissao"];

            Funcionario_VO usuario = AutenticacaoBLL.obterUsuarioAutenticado();
            bool obterAcesso = AutenticacaoBLL.obterAcessoAdministrador(usuario);
            //Monto menu funcionario
            if (!obterAcesso)
                montaMenuFuncionario();
            if (permissao == "N")
                mensagem();

        }

        private void montaMenuFuncionario()
        {
            divFuncionarios.Visible = false;
            divProdutos.Visible = false;
            divFinanceiro.Visible = false;
        }

        private void mensagem()
        {
            ClientScript.RegisterStartupScript(GetType(), "message", "newAlert('alert alert-danger', 'Você não possui permissão!');", true);

        }
    }
}