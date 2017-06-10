using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Produto
{
    public class BordaProduto_VO
    {
        public int idBordaProduto { get; set; }
        public string borda { get; set; }
        public decimal valor { get; set; }
        public bool ativo { get; set; }
    }
}
