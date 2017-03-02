using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class TipoCliente_VO
    {
        public int idTipo { get; set; }
        public string descricao { get; set; }
        public List<TipoCliente_VO> lstTipoCliente { get; set; }


    }
}
