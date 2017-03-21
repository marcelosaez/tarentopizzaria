using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using MODEL;
using MODEL.Funcionario;

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
            Funcionario_VO funcionario = new Funcionario_VO();
            var login = txtLogin.Value;
            var senha = txtSenha.Value;

            if (this.IsValid)
            {
                try
                {
                    funcionario = AutenticacaoBLL.autenticar(login, senha);
                    if (funcionario!=null)
                        Response.Redirect("Main.aspx");
                    else
                    {
                        divMSG.InnerText = "Login ou Senha invalido";
                        divMSG.Attributes.CssStyle.Add("padding","3px");
                    }
                }
                catch (Framework.Erros.Excecao ex)
                {
                    this.Page.Validators.Add(new Framework.Validacao.CustomError(ex.Message));
                }
            }
        }

    }
}