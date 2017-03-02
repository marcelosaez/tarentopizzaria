using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Erros
{
    public class Excecao : Exception
    {
        #region "Construtores"

        /// <summary>
        /// Construtor com a mensagem.
        /// </summary>
        /// <param name="mensagem_"></param>
        public Excecao(String mensagem_)
        {
            this.mensagem = mensagem_;
        }

        #endregion

        #region "Atributos"

        private String mensagem;

        #endregion

        #region "Metodos"

        /// <summary>
        /// Retorna mensagem de erro.
        /// </summary>
        public override string Message
        {
            get { return this.mensagem; }
        }

        #endregion
    }
}
