using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Validacao
{
    public class CustomError : System.Web.UI.WebControls.BaseValidator
    {
        #region "Construtores"

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="erro">Descrição do erro.</param>
        public CustomError(String erro)
        {
            this.ErrorMessage = erro;
            this.IsValid = false;
        }

        #endregion

        #region "Metodos"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override bool EvaluateIsValid()
        {
            return false;
        }

        #endregion
    }
}
