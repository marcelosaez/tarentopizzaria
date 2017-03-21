using MODEL.Funcionario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    [Serializable]
    public class Erro_VO
    {
        #region "Construtores"
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public Erro_VO() { }
        /// <summary>
        /// Construtor setando o ID
        /// </summary>
        /// <param name="codigo"></param>
        public Erro_VO(int codigo)
        {
            this.Codigo = codigo;
        }

        #endregion

        #region "Propriedades"

        public int Codigo { get; set; }
        public Funcionario_VO Usuario { get; set; }
        public DateTime DataCadastro { get; set; }
        public String IP { get; set; }
        public String Pagina { get; set; }
        public String Mensagem { get; set; }
        public String StackTrace { get; set; }
        public Boolean Resolvido { get; set; }

        #endregion
    }
}
