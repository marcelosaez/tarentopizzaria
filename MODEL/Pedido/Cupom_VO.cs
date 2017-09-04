using MODEL.Cliente;
using MODEL.Funcionario;
using MODEL.Pagamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Pedido
{
    public class Cupom_VO
    {
       public Pedido_VO pedido = new Pedido_VO();
       public Funcionario_VO funcionario = new Funcionario_VO();
       public Cliente_VO cliente = new Cliente_VO();
       public Pagamento_VO pagamento = new Pagamento_VO();
    }
}
