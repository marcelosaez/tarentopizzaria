using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class Cliente_VO
    {
        public int idCliente { get; set; }
        public string NomeCliente { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public List<Cliente_VO> lstCliente { get; set; }
    }
}
