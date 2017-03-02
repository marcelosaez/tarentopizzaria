using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                validarForm();
            }
            

        }

        private void validarForm()
        {
            var login = txtLogin.Value;
            var senha = txtSenha.Value;
        }


        protected void cmdLogin_click(object sender, EventArgs e)
        {

            var login = txtLogin.Value;
            var senha = txtSenha.Value;

            if (this.IsValid)
            {
                try
                {
                    //AutenticacaoBLL.autenticar(txtLogin.Text.Trim(), txtSenha.Text.Trim());

                    Response.Redirect("Main.aspx");
                }
                catch (Framework.Erros.Excecao ex)
                {
                    this.Page.Validators.Add(new Framework.Validacao.CustomError(ex.Message));
                }
            }
        }

    }
}