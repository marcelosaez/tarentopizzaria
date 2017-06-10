using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Produto
{
    public class Produto_VO
    {
        public bool temCadastro;

        public int idProduto { get; set; }
        public int idTipoProduto { get; set; }
        public string nome { get; set; }
        public string ingredientes { get; set; }
        public decimal valor { get; set; }
        public bool ativo { get; set; }
        public string tipo { get; set; }
    }
}
