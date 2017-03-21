using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.configurarFormulario();
            }
            catch (Exception ex)
            {
                //tenta fazer o log do erro no banco de dados
                try
                {
                    Erro_VO erro = new Erro_VO();
                    erro.Mensagem = ex.Message.ToString();
                    erro.StackTrace = ex.StackTrace.ToString();
                    erro.Pagina = Request.Url.ToString();
                    erro.IP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                    //TO DO
                    //new Erro_BLL().salvar(erro);
                }
                catch (Exception)
                {
                }

                Server.Transfer(ResolveUrl("~/500.aspx"), false);
            }
        }

        private void configurarFormulario()
        {
            this.addScripts();
        }

        private void addScripts()
        {
            int cont = 0;
            this.Page.Header.Controls.AddAt(cont, new LiteralControl("<script type=\"text/javascript\" src=\"" + ResolveUrl("~/Scripts/jquery-1.9.0.min.js") + "\"></script>"));
            cont++;



        }
    }
}