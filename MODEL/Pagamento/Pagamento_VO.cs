using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Pagamento
{
    public class Pagamento_VO
    {
        public int idEntrega { get; set; }
        public string obs { get; set; }

        public int idStatusPedido { get; set; }
        public int idPedido { get; set; }

        public int idTipoPagamento { get; set; }
        public string TipoPagamento { get; set; }

    }
}
