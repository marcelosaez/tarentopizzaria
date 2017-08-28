using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Pagamento
{
    public enum StatusPagamento
    {
        Aberto=1,
        AguardandoPagamento=2,
        Cancelado=3,
        Entregue=4,
        Finalizado=5,
        Devolvido=6,
        AguardandoEntrega=7
    }
}
