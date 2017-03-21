using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Produto
{
    public class TipoProduto_VO
    {
        public bool temCadastro;

        public int idTipoProduto { get; set; }
        public string tipo { get; set; }
        public bool ativo { get; set; }
    }
}
