using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Cliente
{
    public class Cliente_VO
    {
        
        public int idCliente { get; set; }
        public string nome { get; set; }
        public string endereco { get; set; }
        public int numero { get; set; }
        public string email { get; set; }
        public bool ativo { get; set; }
        public bool temCadastro { get; set; }
        public int dddres { get; set; }
        public int telres { get; set; }
        public int dddcel { get; set; }
        public int cel { get; set; }
        public List<Cliente_VO> lstCliente { get; set; }
    }
}
