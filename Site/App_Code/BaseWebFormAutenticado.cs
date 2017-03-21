using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using BLL;
using MODEL;

namespace Site 
{
    /// <summary>
    /// Classe base para as páginas.
    /// </summary>
    public class BaseWebFormAutenticado : BaseWebForm
    {
        #region "Eventos"

        /// <summary>
        /// Ao carregar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!AutenticacaoBLL.estaAutenticado())
            {
                AutenticacaoBLL.finalizarSessao();
                Response.Redirect(ResolveUrl("~/Login.aspx?err=expirou"));
            }

            this.Page.Title = ConfigurationManager.AppSettings["TITLE_SITE"].ToString();
        }

        #endregion
    }
}
