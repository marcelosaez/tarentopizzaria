using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MODEL;
using BLL;
using MODEL.Funcionario;
using System.IO;

namespace Site

{
    /// <summary>
    /// Classe base para as páginas.
    /// </summary>
    public class BaseWebForm : System.Web.UI.Page
    {
        #region "Eventos"

        /// <summary>
        /// Ao carregar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = ConfigurationManager.AppSettings["TITLE_SITE"].ToString();
        }

        /// <summary>
        /// Ao ocorrer um erro durante a execução de uma página.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Page_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            //tenta fazer o log do erro no banco de dados
            try
            {

                Erro_VO erro = new Erro_VO();
                erro.Usuario = new Funcionario_VO();
                erro.Mensagem = ex.Message.ToString();
                erro.StackTrace = ex.StackTrace.ToString();
                erro.Pagina = Request.Url.ToString();
                if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Length > 0)
                    erro.IP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                else
                    erro.IP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                //new ErroBLL().salvar(erro);
                using (StreamWriter writer = new StreamWriter("C:\\inetpub\\wwwroot\\tarento\\erro" + DateTime.Now.ToString() + ".txt", true))

                {
                    writer.WriteLine("sistema: Impressora");
                    writer.WriteLine("erro: " + erro.Mensagem);
                    writer.WriteLine("StackTrace: " + erro.StackTrace);

                }


            }
            catch (Exception)
            {
            }

            Server.Transfer(ResolveUrl("~/500.aspx"), false);
        }

        #endregion
    }
}
