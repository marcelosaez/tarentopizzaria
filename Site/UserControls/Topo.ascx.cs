using BLL;
using MODEL;
using MODEL.Funcionario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.UserControls
{
    public partial class Topo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Funcionario_VO usuario = AutenticacaoBLL.obterUsuarioAutenticado();
            if (usuario != null)
            {
                this.lblUsuario.Text = usuario.nome;
            }
        }
    }
}